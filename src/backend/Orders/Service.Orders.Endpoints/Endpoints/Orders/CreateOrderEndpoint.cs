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

using Service.Orders.Application.Orders.Commands.CreateOrder;
using Service.Orders.Endpoints.Contracts.Orders;
using Service.Orders.Endpoints.Routes;

namespace Service.Orders.Endpoints.Endpoints.Orders
{
	/// <summary>
	/// Represents create order endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="CreateOrderEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class CreateOrderEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<CreateOrderRequest>
		.WithActionResult<Guid>
	{
		// TODO [Authorize]
		[HttpPost(OrderRoutes.Create)]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Creates a new order.",
			Description = "Creates a new order for current user based on the specified request.",
			Tags = [OrderRoutes.Tag])]
		public override async Task<ActionResult<Guid>> HandleAsync([FromBody] CreateOrderRequest request,
																CancellationToken cancellationToken = default)
			=> await Result.Create(request)
					.Map(r => new CreateOrderCommand
					{
						// TODO when authorization is done, get customer id from jwt token
						CustomerId = Guid.Parse("866DFFC0-C7F6-4477-912C-76586BC0485B"),
						Items = request.Items,
						Address = request.Address,
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(orderId => CreatedAtRoute(nameof(GetOrderByIdEndpoint), new { orderId }, orderId),
										 this.HandleFailure);
	}
}
