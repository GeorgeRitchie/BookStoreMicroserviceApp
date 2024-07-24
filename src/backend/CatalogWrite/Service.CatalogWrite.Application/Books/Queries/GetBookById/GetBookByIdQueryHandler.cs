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

using Service.CatalogWrite.Domain.Books;

namespace Service.CatalogWrite.Application.Books.Queries.GetBookById
{
	/// <summary>
	/// Represents the <see cref="GetBookByIdQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetBookByIdQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The book repository.</param>
	/// <param name="mapper">The auto mapper.</param>
	internal sealed class GetBookByIdQueryHandler(IRepository<Book, BookId> repository, IMapper mapper)
		: IQueryHandler<GetBookByIdQuery, BookDto>
	{
		public async Task<Result<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
			=> Result.Create(
					await repository.GetAll()
									.Include(i => i.Images)
									.Include(i => i.Publisher)
									.Include(i => i.Authors)
									.Include(i => i.Categories)
									.FirstOrDefaultAsync(i => i.Id == request.BookId, cancellationToken))
				.Map(mapper.Map<BookDto>)
				.MapFailure(() => BookErrors.NotFound(request.BookId));
	}
}
