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

using Service.Catalog.Domain.Authors;

namespace Service.Catalog.Application.Authors.Queries.GetAuthorById
{
	/// <summary>
	/// Represents the <see cref="GetAuthorByIdQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetAuthorByIdQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The author repository.</param>
	/// <param name="mapper">The auto mapper.</param>
	internal sealed class GetAuthorByIdQueryHandler(IRepository<Author, AuthorId> repository, IMapper mapper)
		: IQueryHandler<GetAuthorByIdQuery, AuthorDto>
	{
		public async Task<Result<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken) =>
			Result.Create(
					await repository.GetAll()
									.Include(c => c.Images)
									.FirstOrDefaultAsync(i => i.Id == request.AuthorId, cancellationToken))
				.Map(mapper.Map<AuthorDto>)
				.MapFailure(() => AuthorErrors.NotFound(request.AuthorId));
	}
}
