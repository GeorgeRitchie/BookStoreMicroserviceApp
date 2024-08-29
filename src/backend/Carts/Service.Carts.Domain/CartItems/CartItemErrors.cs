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

namespace Service.Carts.Domain.CartItems
{
	/// <summary>
	/// Contains the Cart Item errors.
	/// </summary>
	public static class CartItemErrors
	{
		/// <summary>
		/// Gets <see langword="null"> book source error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error NullBookSource()
			=> new("CartItem.NullBookSource", "Book source is required for cart item.");

		/// <summary>
		/// Gets <see langword="null"> cart error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error NullCart()
			=> new("CartItem.NullCart", "Cart is required for cart item.");
	}
}
