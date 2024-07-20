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
using Service.CatalogWrite.Domain.Authors;

namespace Service.CatalogWrite.Application.Authors.Queries.GetAuthors
{
	/// <summary>
	/// Represents the <see cref="GetAuthorsQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetAuthorsQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The author repository.</param>
	/// <param name="mapper">The auto mapper.</param>
	internal sealed class GetAuthorsQueryHandler(IRepository<Author, AuthorId> repository, IMapper mapper)
		: IQueryHandler<GetAuthorsQuery, IEnumerable<AuthorDto>>
	{
		public async Task<Result<IEnumerable<AuthorDto>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
		{
			var query = request.IncludeDeleted ?
							repository.GetAllIgnoringQueryFiltersAsNoTracking() :
							repository.GetAllAsNoTracking();

			return await query.ProjectTo<AuthorDto>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
		}
	}
}
