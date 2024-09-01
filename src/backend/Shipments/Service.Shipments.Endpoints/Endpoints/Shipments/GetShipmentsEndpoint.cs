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

using Endpoints.Authorization;
using Service.Shipments.Application.Shipments.GetShipments;
using Service.Shipments.Endpoints.Routes;

namespace Service.Shipments.Endpoints.Endpoints.Shipments
{
	/// <summary>
	/// Represents get shipments endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetShipmentsEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetShipmentsEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<GetShipmentsQuery>
		.WithActionResult<ShipmentDto>
	{
		[Authorize]
		[HasPermission(ShipmentPermissions.ReadShipment)]
		[HttpGet(ShipmentRoutes.GetAll)]
		[ProducesResponseType(typeof(ShipmentDto), StatusCodes.Status200OK)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets shipments.",
			Description = "Gets shipments.",
			Tags = [ShipmentRoutes.Tag])]
		public override async Task<ActionResult<ShipmentDto>> HandleAsync([FromQuery] GetShipmentsQuery query, CancellationToken cancellationToken = default)
			=> await sender.Send(query, cancellationToken).Match(Ok, this.HandleFailure);
	}
}
