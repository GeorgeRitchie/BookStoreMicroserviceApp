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
using Persistence.Converters;
using Service.Order.Domain.Shipments;
using Service.Order.Persistence.Contracts;
using Service.Shipments.IntegrationEvents;
using Shared.Extensions;

namespace Service.Order.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="Shipment"/> entity configuration.
	/// </summary>
	internal sealed class ShipmentConfigurations : IEntityTypeConfiguration<Shipment>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Shipment> builder) =>
			 builder
				.Tap(ConfigureDataStructure);

		private static void ConfigureDataStructure(EntityTypeBuilder<Shipment> builder)
		{
			builder.ToTable(TableNames.Shipments);

			builder.HasKey(shipment => shipment.Id);

			builder.Property(shipment => shipment.Id).ValueGeneratedNever()
							.HasConversion(shipmentId => shipmentId.Value, value => new ShipmentId(value));

			builder.Property(shipment => shipment.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.HasQueryFilter(shipment => shipment.IsDeleted == false);

			builder.OwnsOne(shipment => shipment.Address, error =>
			{
				error.Property(e => e.Country).IsRequired(false);
				error.Property(e => e.Region).IsRequired(false);
				error.Property(e => e.District).IsRequired(false);
				error.Property(e => e.City).IsRequired(false);
				error.Property(e => e.Street).IsRequired(false);
				error.Property(e => e.Home).IsRequired(false);
			});

			builder.Property(bs => bs.Status)
					.HasConversion<EnumerationConverter<ShipmentStatus, int>>()
					.IsRequired();

			builder.Property(shipment => shipment.CreatedOnUtc).IsRequired();

			builder.Property(shipment => shipment.ModifiedOnUtc).IsRequired(false);
		}
	}
}
