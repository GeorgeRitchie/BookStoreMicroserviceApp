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

namespace Service.CatalogWrite.Application.Books.Commands.DeleteBook
{
	/// <summary>
	/// Represents the <see cref="DeleteBookCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="DeleteBookCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The book repository.</param>
	internal sealed class DeleteBookCommandHandler(
		ICatalogDb db,
		IBookRepository repository)
		: ICommandHandler<DeleteBookCommand>
	{
		public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken) =>
			await Result.Create(
					await repository.GetAll().FirstOrDefaultAsync(i => i.Id == request.BookId, cancellationToken))
				.MapFailure(() => BookErrors.NotFound(request.BookId))
				.Tap<Book>(repository.Delete)
				.Tap(() => db.SaveChangesAsync(cancellationToken));
	}
}
