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

using Service.CatalogWrite.Domain.BookSources;

namespace Service.CatalogWrite.Application.BooSources.Queries.GetBookSourceById
{
	/// <summary>
	/// Represents the <see cref="GetBookSourceByIdQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetBookSourceByIdQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The book source repository.</param>
	/// <param name="mapper">The auto mapper.</param>
	internal sealed class GetBookSourceByIdQueryHandler(IBookSourceRepository repository, IMapper mapper)
		: IQueryHandler<GetBookSourceByIdQuery, BookSourceDto>
	{
		public async Task<Result<BookSourceDto>> Handle(GetBookSourceByIdQuery request, CancellationToken cancellationToken)
			=> Result.Create(
					await repository.GetAll()
									.FirstOrDefaultAsync(i => i.Id == request.BooSourceId, cancellationToken))
				.Map(mapper.Map<BookSourceDto>)
				.Tap(dto =>
				{
					if (request.IncludeUrl == false)
						dto.Url = null;
				})
				.MapFailure(() => BookSourceErrors.NotFound(request.BooSourceId));
	}
}
