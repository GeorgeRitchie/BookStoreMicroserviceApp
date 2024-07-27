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

using Service.CatalogWrite.Application.Common.Services;
using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.Books;
using Service.CatalogWrite.Domain.ImageSources;

namespace Service.CatalogWrite.Application.Books.Commands.RemoveBookImage
{
	/// <summary>
	/// Represents the <see cref="RemoveBookImageCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RemoveBookImageCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="bookRepository">The book repository.</param>
	/// <param name="imgRepository">The image repository.</param>
	/// <param name="fileManager">The file manager.</param>
	internal sealed class RemoveBookImageCommandHandler(
		ICatalogDb db,
		IBookRepository bookRepository,
		IRepository<ImageSource<BookImageType>, ImageSourceId> imgRepository,
		IFileManager fileManager)
		: ICommandHandler<RemoveBookImageCommand>
	{
		public async Task<Result> Handle(RemoveBookImageCommand request, CancellationToken cancellationToken)
		{
			var book = await bookRepository.GetAll()
											.Include(a => a.Images)
											.FirstOrDefaultAsync(c => c.Id == request.BookId, cancellationToken);

			if (book == null)
				return Result.Failure(BookErrors.NotFound(request.BookId));

			if (request.ImageIds?.Count > 0)
			{
				List<Result<ImageSource<BookImageType>>> result = [];

				request.ImageIds.ForEach(i =>
				{
					var imageToDelete = book.Images.FirstOrDefault(o => o.Id == i);
					if (imageToDelete is not null)
						result.Add(Result.Success(imageToDelete));
					else
						result.Add(Result.Failure<ImageSource<BookImageType>>(
																BookErrors.BookImageNotFound(book.Id, i)));
				});

				return await Result.Combine(result.ToArray())
					.Tap(async () =>
					{
						var images = result.Select(i => i.Value);
						await RemoveFromDb(images!, cancellationToken);
						await images.Tap(o => o.ForEachElement(i => RemoveFiles(i!.Source, cancellationToken)));
					});
			}

			return Result.Success();
		}

		private async Task RemoveFromDb(IEnumerable<ImageSource<BookImageType>> images, CancellationToken cancellationToken)
			=> await images.Tap(imgRepository.DeleteRange)
							.Tap(() => db.SaveChangesAsync(cancellationToken));

		private async Task RemoveFiles(string? source, CancellationToken cancellationToken = default)
		{
			if (source is not null)
				await fileManager.DeleteAsync(source, cancellationToken);
		}
	}
}
