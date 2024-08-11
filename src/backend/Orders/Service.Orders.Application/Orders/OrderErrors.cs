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

using Service.Orders.Domain.Orders;

namespace Service.Orders.Application.Orders
{
	/// <summary>
	/// Contains the order errors.
	/// </summary>
	internal static class OrderErrors
	{
		/// <summary>
		/// Gets order not found error.
		/// </summary>
		/// <param name="orderId">The not found order identifier.</param>
		/// <returns>The error.</returns>
		internal static NotFoundError NotFound(OrderId orderId)
			=> new("Order.NotFound", $"Order with the identifier {orderId.Value} was not found.");
	}
}
