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

using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.Books;
using Service.CatalogWrite.Domain.Categories;

namespace Service.CatalogWrite.Application.Books.Commands.RemoveCategory
{
	/// <summary>
	/// Represents the <see cref="RemoveCategoryCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RemoveCategoryCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="bookRepository">The book repository.</param>
	internal sealed class RemoveCategoryCommandHandler(
		ICatalogDb db,
		IBookRepository bookRepository)
		: ICommandHandler<RemoveCategoryCommand>
	{
		public async Task<Result> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
		{
			var bookId = new BookId(request.BookId);
			var book = await bookRepository.GetAll()
									.Include(i => i.Categories)
									.FirstOrDefaultAsync(i => i.Id == bookId, cancellationToken);

			if (book is null)
				return Result.Failure(BookErrors.NotFound(bookId));

			return await book.RemoveCategory(new CategoryId(request.CategoryId))
							.Tap<Book>(bookRepository.Update)
							.Tap(() => db.SaveChangesAsync(cancellationToken));
		}
	}
}
