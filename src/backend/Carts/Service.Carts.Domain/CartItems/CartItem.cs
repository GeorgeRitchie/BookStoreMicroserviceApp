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

using Service.Carts.Domain.BookSources;
using Service.Carts.Domain.Carts;

namespace Service.Carts.Domain.CartItems
{
	/// <summary>
	/// Represents the Cart Item entity.
	/// </summary>
	public sealed class CartItem : Entity<CartItemId>, IAuditable
	{
		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the book source identifier customer added to cart.
		/// </summary>
		public BookSourceId BookSourceId { get; private set; }

		/// <summary>
		/// Gets the book source customer added to cart.
		/// </summary>
		public BookSource BookSource { get; private set; }

		/// <summary>
		/// Gets the cart identifier this item belongs to.
		/// </summary>
		public CartId CartId { get; private set; }

		/// <summary>
		/// Gets the cart this item belongs to.
		/// </summary>
		public Cart Cart { get; private set; }

		/// <summary>
		/// Gets the book quantity customer is purchasing.
		/// </summary>
		public uint Quantity { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CartItem"/> class.
		/// </summary>
		/// <param name="id">The cart item identifier.</param>
		/// <param name="isDeleted">The cart item deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private CartItem(CartItemId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CartItem"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private CartItem()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="CartItem"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="bookSource">The book source to purchase.</param>
		/// <param name="cart">The cart this item belongs to.</param>
		/// <param name="quantity">The book quantity to purchase.</param>
		/// <returns>The new <see cref="CartItem"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<CartItem> Create(BookSource bookSource, Cart cart, uint quantity)
			=> Result.Success()
				.Ensure(() => bookSource is not null, CartItemErrors.NullBookSource())
				.Ensure(() => cart is not null, CartItemErrors.NullCart())
				.Map(() => new CartItem(new CartItemId(Guid.NewGuid()), false)
				{
					BookSource = bookSource,
					BookSourceId = bookSource.Id,
					CartId = cart.Id,
					Cart = cart,
					Quantity = quantity,
				});
	}
}
