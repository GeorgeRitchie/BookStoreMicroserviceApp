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

using Service.Catalog.Application.BooSources.Queries.GetBookSourceById;
using Service.Catalog.Domain.BookSources;

namespace Service.Catalog.Endpoints.Endpoints.BookSources
{
	/// <summary>
	/// Represents get book source by identifier endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetBookSourceByIdEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetBookSourceByIdEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<Guid>
		.WithActionResult<BookSourceDto>
	{
		// TODO [Authorize]
		[HttpGet(BookSourceRoutes.GetById, Name = nameof(GetBookSourceByIdEndpoint))]
		[ProducesResponseType(typeof(BookSourceDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets the book source by id.",
			Description = "Gets the book source with the specified identifier.",
			Tags = new[] { BookSourceRoutes.Tag })]
		public override async Task<ActionResult<BookSourceDto>> HandleAsync([FromQuery] Guid bookSourceId,
															CancellationToken cancellationToken = default) =>
			await sender.Send(new GetBookSourceByIdQuery(
									new BookSourceId(bookSourceId),
									false),// TODO when identity server is done, make this value default false, but for users with specific permission (owner of site) set true
								cancellationToken)
				.Match(Ok, this.HandleFailure);
	}
}
