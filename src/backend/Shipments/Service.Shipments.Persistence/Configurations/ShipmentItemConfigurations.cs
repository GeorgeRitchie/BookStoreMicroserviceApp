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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Shipments.Domain.ShipmentItems;
using Service.Shipments.Persistence.Contracts;
using Shared.Extensions;

namespace Service.Shipments.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="ShipmentItem"/> entity configuration.
	/// </summary>
	internal sealed class ShipmentItemConfigurations : IEntityTypeConfiguration<ShipmentItem>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<ShipmentItem> builder) =>
			 builder
				.Tap(ConfigureDataStructure)
				.Tap(ConfigureRelationships);

		private static void ConfigureDataStructure(EntityTypeBuilder<ShipmentItem> builder)
		{
			builder.ToTable(TableNames.ShipmentItems);

			builder.HasKey(shipment => shipment.Id);

			builder.Property(shipment => shipment.Id).ValueGeneratedNever()
							.HasConversion(shipmentId => shipmentId.Value, value => new ShipmentItemId(value));

			builder.Property(shipment => shipment.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.Property(shipment => shipment.Quantity).IsRequired();

			builder.Property(shipment => shipment.CreatedOnUtc).IsRequired();

			builder.Property(shipment => shipment.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureRelationships(EntityTypeBuilder<ShipmentItem> builder)
		{
			builder.HasOne(shipment => shipment.BookSource)
				.WithMany()
				.HasForeignKey(s => s.BookSourceId)
				.OnDelete(DeleteBehavior.SetNull)
				.IsRequired(false);

			builder.HasOne(shipment => shipment.Shipment)
				.WithMany(shipment => shipment.Items)
				.HasForeignKey(s => s.ShipmentId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}
