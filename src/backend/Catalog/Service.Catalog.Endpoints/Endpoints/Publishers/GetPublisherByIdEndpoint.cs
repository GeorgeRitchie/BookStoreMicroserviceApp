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

using Service.Catalog.Application.Publishers.Queries.GetPublisherById;
using Service.Catalog.Domain.Publishers;

namespace Service.Catalog.Endpoints.Endpoints.Publishers
{
	/// <summary>
	/// Represents get publisher by identifier endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetPublisherByIdEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetPublisherByIdEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<Guid>
		.WithActionResult<PublisherDto>
	{
		[AllowAnonymous]
		[HttpGet(PublisherRoutes.GetById, Name = nameof(GetPublisherByIdEndpoint))]
		[ProducesResponseType(typeof(PublisherDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets the publisher by id.",
			Description = "Gets the publisher with the specified identifier.",
			Tags = new[] { PublisherRoutes.Tag })]
		public override async Task<ActionResult<PublisherDto>> HandleAsync([FromQuery] Guid publisherId,
																CancellationToken cancellationToken = default) =>
			await sender.Send(new GetPublisherByIdQuery(new PublisherId(publisherId)), cancellationToken)
				.Match(Ok, this.HandleFailure);
	}
}
