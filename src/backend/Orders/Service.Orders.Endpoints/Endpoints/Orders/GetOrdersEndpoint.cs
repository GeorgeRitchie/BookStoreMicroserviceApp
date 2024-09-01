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

using Service.Orders.Application.Orders.Queries.GetOrders;
using Service.Orders.Domain.Orders;
using Service.Orders.Endpoints.Routes;

namespace Service.Orders.Endpoints.Endpoints.Orders
{
	/// <summary>
	/// Represents get orders endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetOrdersEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetOrdersEndpoint(ISender sender) : EndpointBaseAsync
		.WithoutRequest
		.WithActionResult<OrderDto>
	{
		[Authorize]
		[HttpGet(OrderRoutes.GetAll)]
		[ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets orders.",
			Description = "Gets orders of current user.",
			Tags = [OrderRoutes.Tag])]
		public override async Task<ActionResult<OrderDto>> HandleAsync(CancellationToken cancellationToken = default)
			=> await sender.Send(new GetOrdersQuery
			{
				CustomerId = new CustomerId(Guid.Parse(HttpContext.User.GetIdentityProviderId())),
			}, cancellationToken).Match(Ok, this.HandleFailure);
	}
}
