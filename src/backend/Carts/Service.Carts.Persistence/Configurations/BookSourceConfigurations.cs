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
using Service.Carts.Domain.BookSources;
using Service.Catalog.IntegrationEvents;
using Service.Carts.Persistence.Contracts;
using Shared.Extensions;

namespace Service.Carts.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="BookSource"/> entity configuration.
	/// </summary>
	internal sealed class BookSourceConfigurations : IEntityTypeConfiguration<BookSource>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<BookSource> builder) =>
			 builder
				.Tap(ConfigureDataStructure);

		private static void ConfigureDataStructure(EntityTypeBuilder<BookSource> builder)
		{
			builder.ToTable(TableNames.BookSources);

			builder.HasKey(bs => bs.Id);

			builder.Property(bs => bs.Id).ValueGeneratedNever()
							.HasConversion(bsId => bsId.Value, value => new BookSourceId(value));

			builder.Property(book => book.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.Property(bs => bs.Format)
					.IsRequired()
					.HasConversion<EnumerationConverter<BookFormat, string>>();

			builder.Property(bs => bs.Price).IsRequired(true);

			builder.Property(bs => bs.CreatedOnUtc).IsRequired();

			builder.Property(bs => bs.ModifiedOnUtc).IsRequired(false);
		}
	}
}
