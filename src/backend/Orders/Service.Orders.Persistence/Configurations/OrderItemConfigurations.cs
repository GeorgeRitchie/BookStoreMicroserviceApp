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
using Service.Catalog.IntegrationEvents;
using Service.Orders.Domain.OrderItems;
using Service.Orders.Persistence.Contracts;
using Shared.Extensions;

namespace Service.Orders.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="OrderItem"/> entity configuration.
	/// </summary>
	internal sealed class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<OrderItem> builder) =>
			 builder
				.Tap(ConfigureDataStructure)
				.Tap(ConfigureIndexes);

		private static void ConfigureDataStructure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.ToTable(TableNames.OrderItems);

			builder.HasKey(oi => oi.Id);

			builder.Property(oi => oi.Id).ValueGeneratedNever()
							.HasConversion(oiId => oiId.Value, value => new OrderItemId(value));

			builder.Property(oi => oi.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.HasQueryFilter(oi => oi.IsDeleted == false);

			builder.Property(oi => oi.BookId).IsRequired(true)
				.HasConversion(oi => oi.Value, value => new BookId(value));

			builder.Property(book => book.Title).IsRequired(true).HasMaxLength(100);

			builder.Property(book => book.ISBN).IsRequired(false).HasMaxLength(20);

			builder.Property(book => book.Cover).IsRequired(false);

			builder.Property(book => book.Language).IsRequired(true).HasMaxLength(5);

			builder.Property(oi => oi.SourceId).IsRequired(true)
				.HasConversion(oi => oi.Value, value => new BookSourceId(value));

			builder.Property(oi => oi.Quantity).IsRequired(true);

			builder.Property(oi => oi.UnitPrice).IsRequired(true);

			builder.Property(bs => bs.Format)
					.HasConversion<EnumerationConverter<BookFormat, string>>()
					.IsRequired();

			builder.Property(oi => oi.CreatedOnUtc).IsRequired();

			builder.Property(oi => oi.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureIndexes(EntityTypeBuilder<OrderItem> builder)
		{
			builder.HasIndex(oi => oi.BookId);
			builder.HasIndex(oi => oi.SourceId);
		}
	}
}
