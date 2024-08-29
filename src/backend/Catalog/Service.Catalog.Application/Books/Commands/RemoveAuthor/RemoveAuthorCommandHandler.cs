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

using Service.Catalog.Domain;
using Service.Catalog.Domain.Authors;
using Service.Catalog.Domain.Books;

namespace Service.Catalog.Application.Books.Commands.RemoveAuthor
{
	/// <summary>
	/// Represents the <see cref="RemoveAuthorCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RemoveAuthorCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="bookRepository">The book repository.</param>
	internal sealed class RemoveAuthorCommandHandler(
		ICatalogDb db,
		IBookRepository bookRepository)
		: ICommandHandler<RemoveAuthorCommand>
	{
		public async Task<Result> Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
		{
			var bookId = new BookId(request.BookId);
			var book = await bookRepository.GetAll()
									.Include(i => i.Authors)
									.FirstOrDefaultAsync(i => i.Id == bookId, cancellationToken);

			if (book is null)
				return Result.Failure(BookErrors.NotFound(bookId));

			return await book.RemoveAuthor(new AuthorId(request.AuthorId))
							.Tap<Book>(bookRepository.Update)
							.Tap(() => db.SaveChangesAsync(cancellationToken));
		}
	}
}
