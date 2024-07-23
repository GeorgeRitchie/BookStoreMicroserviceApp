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

using Service.CatalogWrite.Application.Publishers.Queries.GetPublishers;

namespace Service.CatalogWrite.Endpoints.Endpoints.Publishers
{
	/// <summary>
	/// Represents get publishers endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetPublishersEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetPublishersEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<GetPublishersQuery>
		.WithActionResult<IEnumerable<PublisherDto>>
	{
		[AllowAnonymous]
		[HttpGet(PublisherRoutes.GetAll)]
		[ProducesResponseType(typeof(IEnumerable<PublisherDto>), StatusCodes.Status200OK)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets publishers.",
			Description = "Gets publishers.",
			Tags = new[] { PublisherRoutes.Tag })]
		public override async Task<ActionResult<IEnumerable<PublisherDto>>> HandleAsync(
			[FromQuery] GetPublishersQuery request,
			CancellationToken cancellationToken = default) =>
			await sender.Send(request, cancellationToken).Match(Ok, this.HandleFailure);
	}
}
