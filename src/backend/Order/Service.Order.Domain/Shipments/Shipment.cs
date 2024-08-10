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

using Service.Shipments.IntegrationEvents;

namespace Service.Order.Domain.Shipments
{
	/// <summary>
	/// Represents the Shipment entity.
	/// </summary>
	public sealed class Shipment : Entity<ShipmentId>, IAuditable
	{
		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the shipment status.
		/// </summary>
		public ShipmentStatus Status { get; private set; }

		/// <summary>
		/// Gets the shipment address if available.
		/// </summary>
		public Address? Address { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Shipment"/> class.
		/// </summary>
		/// <param name="id">The shipment identifier.</param>
		/// <param name="isDeleted">The shipment deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private Shipment(ShipmentId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Shipment"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Shipment()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="Shipment"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="shipmentStatus">Shipment status.</param>
		/// <returns>The new <see cref="Shipment"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Shipment> Create(
			ShipmentStatus shipmentStatus,
			Address? address = null)
			=> Result.Success(new Shipment(new ShipmentId(Guid.NewGuid()), false)
			{
				Status = shipmentStatus,
				Address = address
			});

		/// <summary>
		/// Changes the shipment information.
		/// </summary>
		/// <param name="shipmentStatus">Shipment status.</param>
		/// <returns>The updated shipment.</returns>
		public Result<Shipment> Update(ShipmentStatus shipmentStatus)
			=> Result.Success(this)
						.Tap(s => s.Status = shipmentStatus);
	}
}
