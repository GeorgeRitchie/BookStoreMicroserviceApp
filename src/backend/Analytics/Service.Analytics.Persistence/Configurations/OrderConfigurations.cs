/*
	BookStore
	Copyright(c) 2024, Sharifjon Abdulloev.

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
using Service.Analytics.Domain.Orders;
using Service.Analytics.Persistence.Contracts;
using Shared.Extensions;

namespace Service.Analytics.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="Order"/> entity configuration.
	/// </summary>
	internal sealed class OrderConfigurations : IEntityTypeConfiguration<Order>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Order> builder) =>
			 builder
				.Tap(ConfigureDataStructure)
				.Tap(ConfigureRelationships)
				.Tap(ConfigureIndexes);

		private static void ConfigureDataStructure(EntityTypeBuilder<Order> builder)
		{
			builder.ToTable(TableNames.Orders);

			builder.HasKey(order => order.Id);

			builder.Property(order => order.Id).ValueGeneratedNever()
							.HasConversion(orderId => orderId.Value, value => new OrderId(value));

			builder.Property(order => order.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.HasQueryFilter(order => order.IsDeleted == false);

			builder.Property(order => order.CustomerId).IsRequired()
				.HasConversion(order => order.Value, value => new CustomerId(value));

			builder.Property(order => order.OrderedDateTimeUtc).IsRequired();

			builder.OwnsOne(shipment => shipment.Address, address =>
			{
				address.Property(e => e.Country).IsRequired(false);
				address.Property(e => e.Region).IsRequired(false);
				address.Property(e => e.District).IsRequired(false);
				address.Property(e => e.City).IsRequired(false);
				address.Property(e => e.Street).IsRequired(false);
				address.Property(e => e.Home).IsRequired(false);
			});

			builder.Ignore(order => order.TotalPrice);

			builder.Property(order => order.CreatedOnUtc).IsRequired();

			builder.Property(order => order.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureRelationships(EntityTypeBuilder<Order> builder)
		{
			builder.HasMany(order => order.Items)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}

		private static void ConfigureIndexes(EntityTypeBuilder<Order> builder)
			=> builder.HasIndex(order => order.CustomerId);
	}
}