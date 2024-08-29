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

using Service.Orders.Application.Orders.Queries.GetOrderById;
using Service.Orders.Domain.Orders;
using Service.Orders.Endpoints.Routes;

namespace Service.Orders.Endpoints.Endpoints.Orders
{
	/// <summary>
	/// Represents get order by identifier endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetOrderByIdEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetOrderByIdEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<Guid>
		.WithActionResult<OrderDto>
	{
		// TODO [Authorize]
		[HttpGet(OrderRoutes.GetById, Name = nameof(GetOrderByIdEndpoint))]
		[ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets the order by id.",
			Description = "Gets the order with the specified identifier.",
			Tags = [OrderRoutes.Tag])]
		public override async Task<ActionResult<OrderDto>> HandleAsync([FromQuery] Guid orderId,
															CancellationToken cancellationToken = default)
			=> await sender.Send(new GetOrderByIdQuery(
											new OrderId(orderId),
											// TODO when authorization is done, get customer id from jwt token
											new CustomerId(Guid.Parse("866DFFC0-C7F6-4477-912C-76586BC0485B"))),
								cancellationToken)
							.Match(Ok, this.HandleFailure);
	}
}
