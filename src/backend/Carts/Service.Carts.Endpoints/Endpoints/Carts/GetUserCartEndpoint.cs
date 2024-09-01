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

using Service.Carts.Application.Carts.GetOrCreateCart;
using Service.Carts.Domain.Carts;
using Service.Carts.Endpoints.Routes;

namespace Service.Carts.Endpoints.Endpoints.Carts
{
	/// <summary>
	/// Represents get or create user cart endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetUserCartEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetUserCartEndpoint(ISender sender) : EndpointBaseAsync
		.WithoutRequest
		.WithActionResult<CartDto>
	{
		[Authorize]
		[HttpGet(CartRoutes.Get)]
		[ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets the user's cart.",
			Description = "Gets the cart of current user.",
			Tags = [CartRoutes.Tag])]
		public override async Task<ActionResult<CartDto>> HandleAsync(CancellationToken cancellationToken = default)
			=> await sender.Send(new GetOrCreateCartCommand
			{
				CustomerId = new CustomerId(Guid.Parse(HttpContext.User.GetIdentityProviderId())),
			},
				cancellationToken)
				.Match(Ok, this.HandleFailure);
	}
}
