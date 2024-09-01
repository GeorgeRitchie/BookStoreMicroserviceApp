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

using Service.Shipments.Application.Shipments.GetBookSourceUrl;
using Service.Shipments.Domain.Shipments;
using Service.Shipments.Domain.BookSources;
using Service.Shipments.Endpoints.Contracts.Shipments;
using Service.Shipments.Endpoints.Routes;

namespace Service.Shipments.Endpoints.Endpoints.Shipments
{
	/// <summary>
	/// Represents get e-book source url endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetBookSourceUrlEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetBookSourceUrlEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<GetBookSourceUrlRequests>
		.WithActionResult<string>
	{
		[Authorize]
		[HttpGet(ShipmentRoutes.GetBookSourceUrl)]
		[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets the e-book source url.",
			Description = "Gets the e-book source url.",
			Tags = [ShipmentRoutes.Tag])]
		public override async Task<ActionResult<string>> HandleAsync([FromQuery] GetBookSourceUrlRequests requests,
															CancellationToken cancellationToken = default)
			=> await sender.Send(new GetBookSourceUrlQuery(
											new CustomerId(Guid.Parse(HttpContext.User.GetIdentityProviderId())),
											new OrderId(requests.OrderId),
											new BookSourceId(requests.BookSourceId)),
								cancellationToken)
							.Match(Ok, this.HandleFailure);
	}
}
