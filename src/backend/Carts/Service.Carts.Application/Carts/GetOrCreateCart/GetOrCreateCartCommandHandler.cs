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

using Service.Carts.Domain;
using Service.Carts.Domain.Carts;

namespace Service.Carts.Application.Carts.GetOrCreateCart
{
	/// <summary>
	/// Represents the <see cref="GetOrCreateCartCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="GetOrCreateCartCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The cart repository.</param>
	/// <param name="mapper">The mapper.</param>
	internal sealed class GetOrCreateCartCommandHandler(
		ICartDb db,
		ICartRepository repository,
		IMapper mapper)
		: ICommandHandler<GetOrCreateCartCommand, CartDto>
	{
		/// <inheritdoc/>
		public async Task<Result<CartDto>> Handle(GetOrCreateCartCommand request, CancellationToken cancellationToken)
		{
			Cart? cart = await repository.GetCartByCustomerId(request.CustomerId, cancellationToken);

			if (cart is not null)
				return mapper.Map<CartDto>(cart);

			return await Cart.Create(request.CustomerId)
				.Tap<Cart>(cart => repository.Create(cart))
				.Tap(() => db.SaveChangesAsync(cancellationToken))
				.Map(mapper.Map<CartDto>);
		}
	}
}
