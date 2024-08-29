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

using AutoMapper.QueryableExtensions;
using Service.Catalog.Domain.Books;

namespace Service.Catalog.Application.Books.Queries.GetBooks
{
	/// <summary>
	/// Represents the <see cref="GetBooksQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetBooksQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The book repository.</param>
	/// <param name="mapper">The auto mapper.</param>
	internal sealed class GetBooksQueryHandler(IBookRepository repository, IMapper mapper)
		: IQueryHandler<GetBooksQuery, IEnumerable<BookDto>>
	{
		public async Task<Result<IEnumerable<BookDto>>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
		{
			var query = request.IncludeDeleted ?
							repository.GetAllIgnoringQueryFiltersAsNoTracking() :
							repository.GetAllAsNoTracking();

			return await query.ProjectTo<BookDto>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
		}
	}
}
