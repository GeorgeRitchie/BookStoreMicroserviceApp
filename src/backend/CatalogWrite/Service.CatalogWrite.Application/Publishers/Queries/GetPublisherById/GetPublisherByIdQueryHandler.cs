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

using Service.Catalog.Domain.Publishers;

namespace Service.Catalog.Application.Publishers.Queries.GetPublisherById
{
	/// <summary>
	/// Represents the <see cref="GetPublisherByIdQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetPublisherByIdQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The publisher repository.</param>
	/// <param name="mapper">The auto mapper.</param>
	internal sealed class GetPublisherByIdQueryHandler(
		IRepository<Publisher, PublisherId> repository,
		IMapper mapper)
		: IQueryHandler<GetPublisherByIdQuery, PublisherDto>
	{
		public async Task<Result<PublisherDto>> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken) =>
			Result.Create(
					await repository.GetAll()
									.Include(c => c.Images)
									.FirstOrDefaultAsync(i => i.Id == request.PublisherId, cancellationToken))
				.Map(mapper.Map<PublisherDto>)
				.MapFailure(() => PublisherErrors.NotFound(request.PublisherId));
	}
}
