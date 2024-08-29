﻿/* 
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

namespace Service.Carts.Endpoints.Contracts.Carts
{
	/// <summary>
	/// Represents the request to remove book source from cart.
	/// </summary>
	public sealed class RemoveBookSourceRequest
	{
		/// <summary>
		/// Book source to remove from cart.
		/// </summary>
		public Guid BookSourceId { get; set; }

		/// <summary>
		/// Quantity to remove from book source in cart item.
		/// </summary>
		public uint QuantityToRemove { get; set; }
	}
}
