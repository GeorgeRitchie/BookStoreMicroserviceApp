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

using Service.Payments.Application.Payments.PaymentSuccess;
using Service.Payments.Endpoints.Routes;

namespace Service.Payments.Endpoints.Endpoints.Payments
{
	/// <summary>
	/// Represents success payment endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="SuccessPaymentEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class SuccessPaymentEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<Guid>
		.WithActionResult
	{
		// TODO [Authorize]
		[HttpPost(PaymentRoutes.Success)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Successfully completes the payment.",
			Description = "Successfully completes the payment by its identifier passed in pid.",
			Tags = [PaymentRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromQuery] Guid pid,
																CancellationToken cancellationToken = default)
			=> await sender.Send(new PaymentSuccessCommand(pid), cancellationToken)
					.Match(NoContent, this.HandleFailure);
	}
}
