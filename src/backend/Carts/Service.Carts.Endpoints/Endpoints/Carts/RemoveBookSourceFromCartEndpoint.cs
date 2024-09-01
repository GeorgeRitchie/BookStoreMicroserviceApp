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

using Service.Carts.Application.Carts.RemoveBookSourceFromCart;
using Service.Carts.Domain.BookSources;
using Service.Carts.Domain.Carts;
using Service.Carts.Endpoints.Contracts.Carts;
using Service.Carts.Endpoints.Routes;

namespace Service.Carts.Endpoints.Endpoints.Carts
{
	/// <summary>
	/// Represents remove book source from cart endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="RemoveBookSourceFromCartEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class RemoveBookSourceFromCartEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<RemoveBookSourceRequest>
		.WithActionResult
	{
		[Authorize]
		[HttpPut(CartRoutes.RemoveBookFromCart)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Removes book source from cart.",
			Description = "Removes book source from user cart or decreases quantity.",
			Tags = [CartRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] RemoveBookSourceRequest request,
																CancellationToken cancellationToken = default)
			=> await sender.Send(new RemoveBookSourceFromCartCommand
			{
				CustomerId = new CustomerId(Guid.Parse(HttpContext.User.GetIdentityProviderId())),
				BookSourceId = new BookSourceId(request.BookSourceId),
				QuantityToRemove = request.QuantityToRemove,
			},
				cancellationToken)
				.Match(NoContent, this.HandleFailure);
	}
}
