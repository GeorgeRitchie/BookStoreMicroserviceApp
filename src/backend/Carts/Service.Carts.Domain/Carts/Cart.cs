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

using Service.Carts.Domain.CartItems;

namespace Service.Carts.Domain.Carts
{
	/// <summary>
	/// Represents the Cart entity.
	/// </summary>
	public sealed class Cart : Entity<CartId>, IAuditable
	{
		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the customer identifier this cart belongs to.
		/// </summary>
		public CustomerId CustomerId { get; private set; }

		/// <summary>
		/// Gets the cart items.
		/// </summary>
		public List<CartItem> Items { get; private set; } = [];

		/// <summary>
		/// Initializes a new instance of the <see cref="Cart"/> class.
		/// </summary>
		/// <param name="id">The cart identifier.</param>
		/// <param name="isDeleted">The cart deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private Cart(CartId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Cart"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Cart()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="Cart"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="customerId">Customer identifier for whom creating new cart.</param>
		/// <returns>The new <see cref="Cart"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Cart> Create(CustomerId customerId)
			=> Result.Success()
				.Ensure(() => customerId is not null, CartErrors.CustomerIdIsRequired())
				.Map(() => new Cart(new CartId(Guid.NewGuid()), false)
				{
					CustomerId = customerId,
				});
	}
}
