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
using Service.Shipments.Domain.Shipments;
using Service.Shipments.IntegrationEvents;
using Service.Shipments.Persistence.Contracts;
using Shared.Extensions;

namespace Service.Shipments.Persistence.Configurations
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

			builder.HasKey(bs => bs.Id);

			builder.Property(bs => bs.Id).ValueGeneratedNever()
							.HasConversion(bsId => bsId.Value, value => new ShipmentId(value));

			builder.Property(book => book.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.Property(bs => bs.OrderId).IsRequired()
				.HasConversion(bsId => bsId.Value, value => new OrderId(value));

			builder.Property(bs => bs.CustomerId).IsRequired()
				.HasConversion(bs => bs.Value, value => new CustomerId(value));

			builder.Property(order => order.OrderedDateTimeUtc).IsRequired();

			builder.Property(bs => bs.Status)
					.IsRequired()
					.HasConversion<EnumerationConverter<ShipmentStatus, int>>();

			builder.OwnsOne(bs => bs.Address, address =>
			{
				address.Property(e => e.Country).IsRequired(false);
				address.Property(e => e.Region).IsRequired(false);
				address.Property(e => e.District).IsRequired(false);
				address.Property(e => e.City).IsRequired(false);
				address.Property(e => e.Street).IsRequired(false);
				address.Property(e => e.Home).IsRequired(false);
			});

			builder.Property(bs => bs.CreatedOnUtc).IsRequired();

			builder.Property(bs => bs.ModifiedOnUtc).IsRequired(false);
		}
	}
}
