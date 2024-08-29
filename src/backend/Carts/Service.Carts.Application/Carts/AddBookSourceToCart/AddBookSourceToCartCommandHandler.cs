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
using Service.Carts.Domain.BookSources;
using Service.Carts.Domain.CartItems;
using Service.Carts.Domain.Carts;

namespace Service.Carts.Application.Carts.AddBookSourceToCart
{
	/// <summary>
	/// Represents the <see cref="AddBookSourceToCartCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="AddBookSourceToCartCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The cart repository.</param>
	/// <param name="bookSourceRepository">The book source repository.</param>
	internal sealed class AddBookSourceToCartCommandHandler(
		ICartDb db,
		ICartRepository repository,
		IBookSourceRepository bookSourceRepository)
		: ICommandHandler<AddBookSourceToCartCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(AddBookSourceToCartCommand request, CancellationToken cancellationToken)
		{
			Cart? cart = await repository.GetCartByCustomerId(request.CustomerId, cancellationToken);

			if (cart is null)
				return Result.Failure(CartErrors.UserDoesNotHaveCart());

			var cartItem = cart.Items.FirstOrDefault(i => i.BookSourceId == request.BookSourceId);
			if (cartItem is not null)
			{
				cartItem.Quantity += request.QuantityToAdd;
				repository.Update(cart);
				await db.SaveChangesAsync(cancellationToken);
				return Result.Success();
			}

			return await Result.Create(await bookSourceRepository.GetAll()
								.FirstOrDefaultAsync(i => i.Id == request.BookSourceId, cancellationToken))
						.MapFailure(() => CartErrors.BookSourceNotFound(request.BookSourceId))
						.Bind(bookSource => CartItem.Create(bookSource, cart, request.QuantityToAdd))
						.Tap<CartItem>(cart.Items.Add)
						.Tap(() => repository.Update(cart))
						.Tap(() => db.SaveChangesAsync(cancellationToken));
		}
	}
}
