﻿/*
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
using Persistence.Converters;
using Service.Orders.Domain.Orders;
using Service.Orders.Domain.Payments;
using Service.Orders.Domain.Shipments;
using Service.Orders.IntegrationEvents;
using Service.Orders.Persistence.Contracts;
using Shared.Extensions;

namespace Service.Orders.Persistence.Configurations
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

			builder.Property(bs => bs.Status)
					.HasConversion<EnumerationConverter<OrderStatus, int>>()
					.IsRequired();

			builder.Ignore(order => order.TotalPrice);

			builder.Property(order => order.CreatedOnUtc).IsRequired();

			builder.Property(order => order.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureRelationships(EntityTypeBuilder<Order> builder)
		{
			builder.HasOne(order => order.Payment)
				.WithOne()
				.HasForeignKey<Payment>()
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired(false);

			builder.HasOne(order => order.Shipment)
				.WithOne()
				.HasForeignKey<Shipment>()
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired(false);

			builder.HasMany(order => order.Items)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}

		private static void ConfigureIndexes(EntityTypeBuilder<Order> builder)
			=> builder.HasIndex(order => order.CustomerId);
	}
}