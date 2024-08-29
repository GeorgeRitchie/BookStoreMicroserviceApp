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

using Service.Shipments.Domain.Shipments;

namespace Service.Shipments.Application.Shipments
{
	/// <summary>
	/// Contains the shipment errors.
	/// </summary>
	internal static class ShipmentErrors
	{
		/// <summary>
		/// Gets shipment not found error.
		/// </summary>
		/// <param name="shipmentId">The not found shipment identifier.</param>
		/// <returns>The error.</returns>
		internal static NotFoundError NotFound(ShipmentId shipmentId)
			=> new("Shipment.NotFound", $"Shipment with the identifier {shipmentId.Value} was not found.");

		/// <summary>
		/// Gets invalid shipment status name error.
		/// </summary>
		/// <param name="statusName">The invalid shipment status name.</param>
		/// <returns>The error.</returns>
		internal static Error InvalidShipmentStatusName(string statusName)
			=> new("Shipment.InvalidShipmentStatusName", $"Shipment does not have status with name {statusName}");

		/// <summary>
		/// Gets book source not found error.
		/// </summary>
		/// <returns>The error.</returns>
		internal static NotFoundError BookSourceNotFound()
			=> new("Shipment.BookSourceNotFound", "Requested book source not found.");

		/// <summary>
		/// Gets not e-book error.
		/// </summary>
		/// <returns>The error.</returns>
		internal static Error NotEBookError()
			=> new("Shipment.NotEBook", "Requested book source is not e-book.");
	}
}
