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

using Service.Shipments.Domain.BookSources;
using Service.Shipments.Domain.Shipments;

namespace Service.Shipments.Domain.ShipmentItems
{
	/// <summary>
	/// Represents the Shipment Item entity.
	/// </summary>
	public sealed class ShipmentItem : Entity<ShipmentItemId>, IAuditable
	{
		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets or sets Book Source identifier.
		/// </summary>
		public BookSourceId BookSourceId { get; set; }

		/// <summary>
		/// Gets or sets Book Source.
		/// </summary>
		public BookSource BookSource { get; set; }

		/// <summary>
		/// Gets or sets shipment identifier this item belongs to.
		/// </summary>
		public ShipmentId ShipmentId { get; set; }

		/// <summary>
		/// Gets or sets shipment this item belongs to.
		/// </summary>
		public Shipment Shipment { get; set; }

		/// <summary>
		/// Gets or sets shipment item quantity.
		/// </summary>
		public uint Quantity { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ShipmentItem"/> class.
		/// </summary>
		/// <param name="id">The shipment item identifier.</param>
		/// <param name="isDeleted">The shipment item deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		public ShipmentItem(ShipmentItemId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShipmentItem"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private ShipmentItem()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}
	}
}
