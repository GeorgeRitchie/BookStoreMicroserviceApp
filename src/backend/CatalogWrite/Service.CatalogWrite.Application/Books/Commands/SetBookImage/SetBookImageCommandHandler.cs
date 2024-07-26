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
using Service.CatalogWrite.Domain.Books;
using Service.CatalogWrite.Domain.ImageSources;

namespace Service.CatalogWrite.Application.Books.Commands.SetBookImage
{
	/// <summary>
	/// Represents the <see cref="SetBookImageCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="SetBookImageCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="bookRepository">The book repository.</param>
	/// <param name="imgRepository">The image repository.</param>
	/// <param name="fileManager">The file manager.</param>
	/// <param name="logger">The logger.</param>
	internal sealed class SetBookImageCommandHandler(
		ICatalogDb db,
		IBookRepository bookRepository,
		IRepository<ImageSource<BookImageType>, ImageSourceId> imgRepository,
		IFileManager fileManager,
		ILogger<SetBookImageCommandHandler> logger)
		: ICommandHandler<SetBookImageCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(SetBookImageCommand request, CancellationToken cancellationToken)
		{
			var book = await bookRepository.GetAll()
											.Include(a => a.Images)
											.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

			if (book == null)
				return Result.Failure(BookErrors.NotFound(request.Id));

			List<string?> newFilesSources = [];

			try
			{
				List<Result<ImageSource<BookImageType>>> actionResults = [Result.Success<ImageSource<BookImageType>>(null)];
				List<string?> oldFilesSources = [];

				if (request.Icon is not null)
				{
					var (oldImage, newImage, result) = await SetBookIcon(request.Icon, book, cancellationToken);

					newFilesSources.Add(newImage);
					oldFilesSources.Add(oldImage);
					actionResults.Add(result);
				}

				if (request.Cover is not null)
				{
					var (oldImage, newImage, result) = await SetBookCover(request.Cover, book, cancellationToken);

					newFilesSources.Add(newImage);
					oldFilesSources.Add(oldImage);
					actionResults.Add(result);
				}

				if (request.Previews?.Any() == true)
				{
					foreach (var preview in request.Previews)
					{
						var (newImage, result) = await SetBookPreviewImage(preview, book, cancellationToken);

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
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to set images to book.", ex);

				foreach (var source in newFilesSources)
				{
					await RemoveFiles(source, cancellationToken);
				}

				return Result.Failure(BookErrors.UpdateOperationFailed);
			}
		}

		private async Task<(string? oldImage, string? newImage, Result<ImageSource<BookImageType>> result)> SetBookIcon(
			IFile file,
			Book book,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				var oldIcon = book.Images.FirstOrDefault(i => i.Type == BookImageType.Icon);
				if (oldIcon is not null)
				{
					imgRepository.Delete(oldIcon);
				}

				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 BookImageType.Icon.Name,
														 cancellationToken);

				var newIcon = ImageSource<BookImageType>.Create(fileSource, BookImageType.Icon)
							.Tap<ImageSource<BookImageType>>(c => imgRepository.Create(c))
							.Tap<ImageSource<BookImageType>>(book.Images.Add);

				return (oldIcon?.Source, fileSource, newIcon);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create icon to book.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return (
					null,
					null,
					Result.Failure<ImageSource<BookImageType>>(BookErrors.SetImageOperationFailed(file.FileName)));
			}
		}

		private async Task<(string? oldImage, string? newImage, Result<ImageSource<BookImageType>> result)> SetBookCover(
			IFile file,
			Book book,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				var oldCover = book.Images.FirstOrDefault(i => i.Type == BookImageType.Cover);
				if (oldCover is not null)
				{
					imgRepository.Delete(oldCover);
				}

				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 BookImageType.Cover.Name,
														 cancellationToken);

				var newCover = ImageSource<BookImageType>.Create(fileSource, BookImageType.Cover)
							.Tap<ImageSource<BookImageType>>(c => imgRepository.Create(c))
							.Tap<ImageSource<BookImageType>>(book.Images.Add);

				return (oldCover?.Source, fileSource, newCover);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create cover image to book.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return (
					null,
					null,
					Result.Failure<ImageSource<BookImageType>>(BookErrors.SetImageOperationFailed(file.FileName)));
			}
		}

		private async Task<(string? newImage, Result<ImageSource<BookImageType>> result)> SetBookPreviewImage(
			IFile file,
			Book book,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 BookImageType.Preview.Name,
														 cancellationToken);

				var newPreview = ImageSource<BookImageType>.Create(fileSource, BookImageType.Preview)
							.Tap<ImageSource<BookImageType>>(c => imgRepository.Create(c))
							.Tap<ImageSource<BookImageType>>(book.Images.Add);

				return (fileSource, newPreview);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create preview image to book.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return (null,
						Result.Failure<ImageSource<BookImageType>>(BookErrors.SetImageOperationFailed(file.FileName)));
			}
		}

		private async Task RemoveFiles(string? source, CancellationToken cancellationToken = default)
		{
			if (source is not null)
				await fileManager.DeleteAsync(source, cancellationToken);
		}
	}
}
