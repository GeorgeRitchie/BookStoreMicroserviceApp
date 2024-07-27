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
using Service.CatalogWrite.Domain.BookSources;

namespace Service.CatalogWrite.Application.BooSources.CreateBookSource
{
	/// <summary>
	/// Represents the <see cref="CreateBookSourceCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="CreateBookSourceCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="bookRepository">The book repository.</param>
	/// <param name="sourceRepository">The book source repository.</param>
	internal sealed class CreateBookSourceCommandHandler(
		ICatalogDb db,
		IBookRepository bookRepository,
		IBookSourceRepository sourceRepository)
		: ICommandHandler<CreateBookSourceCommand, Guid>
	{
		/// <inheritdoc/>
		public async Task<Result<Guid>> Handle(CreateBookSourceCommand request, CancellationToken cancellationToken)
			=> await Result.Create(await bookRepository.GetAll()
													.FirstOrDefaultAsync(i => i.Id == new BookId(request.BookId),
																		cancellationToken))
						.MapFailure(() => BookSourceErrors.BookNotFound(new BookId(request.BookId)))
						.Bind(book => BookSource.Create(book,
														BookFormat.FromName(request.Format)!,
														request.Url,
														request.Quantity,
														request.Price,
														request.PreviewUrl))
						.Tap<BookSource>(bs => sourceRepository.Create(bs))
						.Tap(() => db.SaveChangesAsync(cancellationToken))
						.Map(bs => bs.Id.Value);
	}
}
