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

namespace Service.Shipments.IntegrationEvents
{
	/// <summary>
	/// Represents the Shipment status enumeration.
	/// </summary>
	public sealed class ShipmentStatus : Enumeration<ShipmentStatus>
	{
		public static readonly ShipmentStatus Pending = new("Pending", 0);
		public static readonly ShipmentStatus Processing = new("Processing", 1);
		public static readonly ShipmentStatus Shipped = new("Shipped", 2);

		/// <summary>
		/// Initializes a new instance of the <see cref="ShipmentStatus"/> class.
		/// </summary>
		/// <inheritdoc/>
		private ShipmentStatus(string name, int value) : base(name, value)
		{
		}
	}
}
