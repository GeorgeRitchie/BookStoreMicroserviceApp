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

using Service.CatalogWrite.Application.Common.Helpers;
using Service.CatalogWrite.Application.Common.Services;
using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.Authors;
using Service.CatalogWrite.Domain.ImageSources;

namespace Service.CatalogWrite.Application.Authors.Commands.SetAuthorImage
{
	/// <summary>
	/// Represents the <see cref="SetAuthorImageCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="SetAuthorImageCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="authorRepository">The author repository.</param>
	/// <param name="imgRepository">The image repository.</param>
	/// <param name="fileManager">The file manager.</param>
	/// <param name="logger">The logger.</param>
	internal sealed class SetAuthorImageCommandHandler(
		ICatalogDb db,
		IRepository<Author, AuthorId> authorRepository,
		IRepository<ImageSource<AuthorImageType>, ImageSourceId> imgRepository,
		IFileManager fileManager,
		ILogger<SetAuthorImageCommandHandler> logger)
		: ICommandHandler<SetAuthorImageCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(SetAuthorImageCommand request, CancellationToken cancellationToken)
		{
			var author = await authorRepository.GetAll()
											.Include(a => a.Images)
											.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

			if (author == null)
				return Result.Failure(AuthorErrors.NotFound(request.Id));

			List<string?> newFilesSources = [];

			try
			{
				List<Result<ImageSource<AuthorImageType>>> actionResults = [Result.Success<ImageSource<AuthorImageType>>(null)];
				List<string?> oldFilesSources = [];

				if (request.Icon is not null)
				{
					var (oldImage, newImage, result) = await SetAuthorIcon(request.Icon, author, cancellationToken);

					newFilesSources.Add(newImage);
					oldFilesSources.Add(oldImage);
					actionResults.Add(result);
				}

				if (request.Photo is not null)
				{
					var (oldImage, newImage, result) = await SetAuthorPhoto(request.Photo, author, cancellationToken);

					newFilesSources.Add(newImage);
					oldFilesSources.Add(oldImage);
					actionResults.Add(result);
				}

				if (request.Others?.Any() == true)
				{
					foreach (var other in request.Others)
					{
						var (newImage, result) = await SetAuthorImage(other, author, cancellationToken);

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
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to set images to author.", ex);

				foreach (var source in newFilesSources)
				{
					await RemoveFiles(source, cancellationToken);
				}

				return Result.Failure<Author>(AuthorErrors.UpdateOperationFailed);
			}
		}

		private async Task<(string? oldImage, string? newImage, Result<ImageSource<AuthorImageType>> result)> SetAuthorIcon(
			IFile file,
			Author author,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				var oldAuthorIcon = author.Images.FirstOrDefault(i => i.Type == AuthorImageType.ProfileIcon);
				if (oldAuthorIcon is not null)
				{
					imgRepository.Delete(oldAuthorIcon);
				}

				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 AuthorImageType.ProfileIcon.Name,
														 cancellationToken);

				var newAuthorIcon = ImageSource<AuthorImageType>.Create(fileSource, AuthorImageType.ProfileIcon)
							.Tap<ImageSource<AuthorImageType>>(c => imgRepository.Create(c))
							.Tap<ImageSource<AuthorImageType>>(author.Images.Add);

				return (oldAuthorIcon?.Source, fileSource, newAuthorIcon);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create icon to author.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return (
					null,
					null,
					Result.Failure<ImageSource<AuthorImageType>>(AuthorErrors.SetAuthorImageOperationFailed(file.FileName)));
			}
		}

		private async Task<(string? oldImage, string? newImage, Result<ImageSource<AuthorImageType>> result)> SetAuthorPhoto(
			IFile file,
			Author author,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				var oldAuthorPhoto = author.Images.FirstOrDefault(i => i.Type == AuthorImageType.AuthorPhoto);
				if (oldAuthorPhoto is not null)
				{
					imgRepository.Delete(oldAuthorPhoto);
				}

				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 AuthorImageType.AuthorPhoto.Name,
														 cancellationToken);

				var newAuthorPhoto = ImageSource<AuthorImageType>.Create(fileSource, AuthorImageType.AuthorPhoto)
							.Tap<ImageSource<AuthorImageType>>(c => imgRepository.Create(c))
							.Tap<ImageSource<AuthorImageType>>(author.Images.Add);

				return (oldAuthorPhoto?.Source, fileSource, newAuthorPhoto);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create profile photo to author.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return (
					null,
					null,
					Result.Failure<ImageSource<AuthorImageType>>(AuthorErrors.SetAuthorImageOperationFailed(file.FileName)));
			}
		}

		private async Task<(string? newImage, Result<ImageSource<AuthorImageType>> result)> SetAuthorImage(
			IFile file,
			Author author,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 AuthorImageType.Other.Name,
														 cancellationToken);

				var newOtherAuthorPhoto = ImageSource<AuthorImageType>.Create(fileSource, AuthorImageType.Other)
							.Tap<ImageSource<AuthorImageType>>(c => imgRepository.Create(c))
							.Tap<ImageSource<AuthorImageType>>(author.Images.Add);

				return (fileSource, newOtherAuthorPhoto);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create other photo to author.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return (null,
						Result.Failure<ImageSource<AuthorImageType>>(AuthorErrors.SetAuthorImageOperationFailed(file.FileName)));
			}
		}

		private async Task RemoveFiles(string? source, CancellationToken cancellationToken = default)
		{
			if (source is not null)
				await fileManager.DeleteAsync(source, cancellationToken);
		}
	}
}
