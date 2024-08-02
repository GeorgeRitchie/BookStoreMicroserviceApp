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

namespace Service.Catalog.Application.Books.Commands.AddAuthor
{
	/// <summary>
	/// Represents the <see cref="AddAuthorCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="AddAuthorCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="bookRepository">The book repository.</param>
	/// <param name="authorRepository">The author repository.</param>
	internal sealed class AddAuthorCommandHandler(
		ICatalogDb db,
		IBookRepository bookRepository,
		IRepository<Author, AuthorId> authorRepository)
		: ICommandHandler<AddAuthorCommand>
	{
		public async Task<Result> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
		{
			var bookId = new BookId(request.BookId);
			var book = await bookRepository.GetAll()
									.Include(i => i.Authors)
									.FirstOrDefaultAsync(i => i.Id == bookId, cancellationToken);

			if (book is null)
				return Result.Failure(BookErrors.NotFound(bookId));

			var authorId = new AuthorId(request.AuthorId);
			var author = await authorRepository.GetAll()
									.FirstOrDefaultAsync(i => i.Id == authorId, cancellationToken);

			if (author is null)
				return Result.Failure(Authors.AuthorErrors.NotFound(authorId));

			return await book.AddAuthor(author)
							.Tap<Book>(bookRepository.Update)
							.Tap(() => db.SaveChangesAsync(cancellationToken));
		}
	}
}
