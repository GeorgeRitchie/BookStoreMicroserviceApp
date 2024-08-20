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

namespace Service.Carts.Application.Carts
{
	/// <summary>
	/// Contains the cart errors.
	/// </summary>
	internal static class CartErrors
	{
		/// <summary>
		/// Gets book source not found error.
		/// </summary>
		/// <param name="bookSourceId">The not found book source identifier.</param>
		/// <returns>The error.</returns>
		internal static NotFoundError BookSourceNotFound(BookSourceId bookSourceId)
			=> new("Cart.BookSourceNotFound",
					$"Book Source with the identifier {bookSourceId.Value} was not found.");

		/// <summary>
		/// Gets user does not have cart error.
		/// </summary>
		/// <returns>The error.</returns>
		internal static Error UserDoesNotHaveCart()
			=> new("Cart.UserDoesNotHaveCart",
					"Current user does not have cart.");

		/// <summary>
		/// Gets cart item not found error.
		/// </summary>
		/// <param name="bookSourceId">The book source identifier used to search cart item.</param>
		/// <returns>The error.</returns>
		internal static NotFoundError CartItemNotFound(BookSourceId bookSourceId)
			=> new("Cart.CartItemNotFound",
					$"Cart item with book source identifier {bookSourceId.Value} not found.");
	}
}
