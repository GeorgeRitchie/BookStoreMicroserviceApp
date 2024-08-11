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

using Domain.Primitives;

namespace Service.Orders.IntegrationEvents
{
	/// <summary>
	/// Represents the Order status enumeration.
	/// </summary>
	public sealed class OrderStatus : Enumeration<OrderStatus>
	{
		public static readonly OrderStatus Pending = new("Pending", 0);
		public static readonly OrderStatus Failed = new("Failed", 1);
		public static readonly OrderStatus Completed = new("Completed", 2);
		public static readonly OrderStatus PaymentProcessing = new("PaymentProcessing", 3);
		public static readonly OrderStatus ShippingProcessing = new("ShippingProcessing", 4);

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderStatus"/> class.
		/// </summary>
		/// <inheritdoc/>
		private OrderStatus(string name, int value) : base(name, value)
		{
		}
	}
}
