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

using Service.Carts.Application.Carts.ClearCart;
using Service.Carts.Domain.Carts;
using Service.Carts.Endpoints.Routes;

namespace Service.Carts.Endpoints.Endpoints.Carts
{
	/// <summary>
	/// Represents clear user cart endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="ClearUserCartEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class ClearUserCartEndpoint(ISender sender) : EndpointBaseAsync
		.WithoutRequest
		.WithActionResult
	{
		[Authorize]
		[HttpDelete(CartRoutes.Clear)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Clears the user cart.",
			Description = "Clears the cart of current user.",
			Tags = [CartRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
			=> await sender.Send(new ClearCartCommand
			{
				CustomerId = new CustomerId(Guid.Parse(HttpContext.User.GetIdentityProviderId())),
			},
				cancellationToken)
				.Match(NoContent, this.HandleFailure);
	}
}
