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
using Service.Shipments.Application.Shipments.UpdateShipmentStatus;
using Service.Shipments.Endpoints.Routes;

namespace Service.Shipments.Endpoints.Endpoints.Shipments
{
	/// <summary>
	/// Represents update shipment status endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="UpdateShipmentStatusEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class UpdateShipmentStatusEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<UpdateShipmentStatusCommand>
		.WithActionResult
	{
		[Authorize]
		[HasPermission(ShipmentPermissions.UpdateShipment)]
		[HttpPut(ShipmentRoutes.UpdateStatus)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Updates shipment status.",
			Description = "Updates shipment status.",
			Tags = [ShipmentRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] UpdateShipmentStatusCommand command,
																CancellationToken cancellationToken = default)
			=> await sender.Send(command, cancellationToken)
					.Match(NoContent, this.HandleFailure);
	}
}
