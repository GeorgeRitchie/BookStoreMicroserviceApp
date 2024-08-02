/* 
	BookStore
	Copyright (c) 2024, Sharifjon Abdulloev.

	This program is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License, version 3.0, 
	as published by the Free Software Foundation.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License, version 3.0, for more details.

	You should have received a copy of the GNU General Public License
	along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using Service.Catalog.Application.Common.Helpers;
using Service.Catalog.Application.Common.Services;
using Service.Catalog.Domain;
using Service.Catalog.Domain.ImageSources;
using Service.Catalog.Domain.Publishers;

namespace Service.Catalog.Application.Publishers.Commands.SetPublisherImage
{
	/// <summary>
	/// Represents the <see cref="SetPublisherImageCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="SetPublisherImageCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="publisherRepository">The publisher repository.</param>
	/// <param name="imgRepository">The image repository.</param>
	/// <param name="fileManager">The file manager.</param>
	/// <param name="logger">The logger.</param>
	internal sealed class SetPublisherImageCommandHandler(
		ICatalogDb db,
		IRepository<Publisher, PublisherId> publisherRepository,
		IRepository<ImageSource<PublisherImageType>, ImageSourceId> imgRepository,
		IFileManager fileManager,
		ILogger<SetPublisherImageCommandHandler> logger)
		: ICommandHandler<SetPublisherImageCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(SetPublisherImageCommand request, CancellationToken cancellationToken)
		{
			var publisher = await publisherRepository.GetAll()
											.Include(a => a.Images)
											.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

			if (publisher == null)
				return Result.Failure(PublisherErrors.NotFound(request.Id));

			List<string?> newFilesSources = [];

			try
			{
				List<Result<ImageSource<PublisherImageType>>> actionResults = [Result.Success<ImageSource<PublisherImageType>>(null)];
				List<string?> oldFilesSources = [];

				if (request.Icon is not null)
				{
					var (oldImage, newImage, result) = await SetIcon(request.Icon, publisher, cancellationToken);

					newFilesSources.Add(newImage);
					oldFilesSources.Add(oldImage);
					actionResults.Add(result);
				}

				if (request.Photo is not null)
				{
					var (oldImage, newImage, result) = await SetPhoto(request.Photo, publisher, cancellationToken);

					newFilesSources.Add(newImage);
					oldFilesSources.Add(oldImage);
					actionResults.Add(result);
				}

				if (request.Others?.Any() == true)
				{
					foreach (var other in request.Others)
					{
						var (newImage, result) = await SetImage(other, publisher, cancellationToken);

						newFilesSources.Add(newImage);
						actionResults.Add(result);
					}
				}

				return await Result.Combine(actionResults.ToArray())
							.Tap(() => db.SaveChangesAsync(cancellationToken))
							.Tap(() => oldFilesSources.ForEachElement(s => RemoveFiles(s, cancellationToken)))
							.OnFailure(e => newFilesSources.ForEachElement(s => RemoveFiles(s, cancellationToken)));
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to set images to publisher.", ex);

				foreach (var source in newFilesSources)
				{
					await RemoveFiles(source, cancellationToken);
				}

				return Result.Failure<Publisher>(PublisherErrors.UpdateOperationFailed);
			}
		}

		private async Task<(string? oldImage, string? newImage, Result<ImageSource<PublisherImageType>> result)> SetIcon(
			IFile file,
			Publisher publisher,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				var oldIcon = publisher.Images.FirstOrDefault(i => i.Type == PublisherImageType.Icon);
				if (oldIcon is not null)
				{
					imgRepository.Delete(oldIcon);
				}

				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 PublisherImageType.Icon.Name,
														 cancellationToken);

				var newIcon = ImageSource<PublisherImageType>.Create(fileSource, PublisherImageType.Icon)
							.Tap<ImageSource<PublisherImageType>>(c => imgRepository.Create(c))
							.Tap<ImageSource<PublisherImageType>>(publisher.Images.Add);

				return (oldIcon?.Source, fileSource, newIcon);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create icon to publisher.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return (
					null,
					null,
					Result.Failure<ImageSource<PublisherImageType>>(PublisherErrors.SetImageOperationFailed(file.FileName)));
			}
		}

		private async Task<(string? oldImage, string? newImage, Result<ImageSource<PublisherImageType>> result)> SetPhoto(
			IFile file,
			Publisher publisher,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				var oldPhoto = publisher.Images.FirstOrDefault(i => i.Type == PublisherImageType.Photo);
				if (oldPhoto is not null)
				{
					imgRepository.Delete(oldPhoto);
				}

				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 PublisherImageType.Photo.Name,
														 cancellationToken);

				var newPhoto = ImageSource<PublisherImageType>.Create(fileSource, PublisherImageType.Photo)
							.Tap<ImageSource<PublisherImageType>>(c => imgRepository.Create(c))
							.Tap<ImageSource<PublisherImageType>>(publisher.Images.Add);

				return (oldPhoto?.Source, fileSource, newPhoto);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create profile photo to publisher.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return (
					null,
					null,
					Result.Failure<ImageSource<PublisherImageType>>(PublisherErrors.SetImageOperationFailed(file.FileName)));
			}
		}

		private async Task<(string? newImage, Result<ImageSource<PublisherImageType>> result)> SetImage(
			IFile file,
			Publisher publisher,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 PublisherImageType.Other.Name,
														 cancellationToken);

				var newOtherPhoto = ImageSource<PublisherImageType>.Create(fileSource, PublisherImageType.Other)
							.Tap<ImageSource<PublisherImageType>>(c => imgRepository.Create(c))
							.Tap<ImageSource<PublisherImageType>>(publisher.Images.Add);

				return (fileSource, newOtherPhoto);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create other photo to publisher.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return (null,
						Result.Failure<ImageSource<PublisherImageType>>(PublisherErrors.SetImageOperationFailed(file.FileName)));
			}
		}

		private async Task RemoveFiles(string? source, CancellationToken cancellationToken = default)
		{
			if (source is not null)
				await fileManager.DeleteAsync(source, cancellationToken);
		}
	}
}
