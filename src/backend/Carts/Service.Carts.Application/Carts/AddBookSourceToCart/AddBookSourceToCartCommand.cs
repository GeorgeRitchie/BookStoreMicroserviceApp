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

namespace Service.Carts.Application.Carts.AddBookSourceToCart
{
	/// <summary>
	/// Represents the command to add book source to cart.
	/// </summary>
	public sealed class AddBookSourceToCartCommand : ICommand
	{
		/// <summary>
		/// Customer identifier the cart belongs to.
		/// </summary>
		public CustomerId CustomerId { get; set; }

		/// <summary>
		/// Book source to add to cart.
		/// </summary>
		public BookSourceId BookSourceId { get; set; }

		/// <summary>
		/// Quantity to add to book source in cart item.
		/// </summary>
		public uint QuantityToAdd { get; set; }
	}
}
