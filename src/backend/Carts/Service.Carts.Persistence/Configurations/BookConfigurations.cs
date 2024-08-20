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
using Service.Carts.Domain.Books;
using Service.Carts.Persistence.Contracts;
using Shared.Extensions;

namespace Service.Carts.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="Book"/> entity configuration.
	/// </summary>
	internal sealed class BookConfigurations : IEntityTypeConfiguration<Book>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Book> builder) =>
			 builder
				.Tap(ConfigureDataStructure)
				.Tap(ConfigureRelationships);

		private static void ConfigureDataStructure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable(TableNames.Books);

			builder.HasKey(book => book.Id);

			builder.Property(book => book.Id).ValueGeneratedNever()
							.HasConversion(bookId => bookId.Value, value => new BookId(value));

			builder.Property(book => book.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.Property(book => book.Title).IsRequired().HasMaxLength(100);

			builder.Property(book => book.Description).IsRequired().HasDefaultValue(string.Empty);

			builder.Property(book => book.ISBN).IsRequired(false).HasMaxLength(20);

			builder.Property(book => book.Language).IsRequired().HasMaxLength(5);

			builder.Property(book => book.Cover).IsRequired(false);

			builder.Property(book => book.CreatedOnUtc).IsRequired();

			builder.Property(book => book.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureRelationships(EntityTypeBuilder<Book> builder)
		{
			builder.HasMany(book => book.Sources)
				.WithOne(s => s.Book)
				.HasForeignKey(s => s.BookId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}
