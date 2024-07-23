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
using Service.CatalogWrite.Domain.Authors;
using Service.CatalogWrite.Domain.Books;
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Persistence.Contracts;
using Shared.Extensions;

namespace Service.CatalogWrite.Persistence.Configurations
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
				.Tap(ConfigureRelationships)
				.Tap(ConfigureIndexes);

		private static void ConfigureDataStructure(EntityTypeBuilder<Book> builder)
		{
			builder.ToTable(TableNames.Books);

			builder.HasKey(book => book.Id);

			builder.Property(book => book.Id).ValueGeneratedNever()
							.HasConversion(bookId => bookId.Value, value => new BookId(value));

			builder.Property(book => book.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.HasQueryFilter(book => book.IsDeleted == false);

			builder.Property(book => book.Title).IsRequired().HasMaxLength(100);

			builder.Property(book => book.Description).IsRequired().HasDefaultValue(string.Empty);

			builder.Property(book => book.ISBN).IsRequired(false).HasMaxLength(20);

			builder.Property(book => book.Language).IsRequired().HasMaxLength(5);

			builder.Property(book => book.AgeRating).IsRequired();

			builder.Property(book => book.PublishedDate).IsRequired(false);

			builder.Property(book => book.CreatedOnUtc).IsRequired();

			builder.Property(book => book.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureRelationships(EntityTypeBuilder<Book> builder)
		{
			builder.HasMany(book => book.Images)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(book => book.Publisher)
				.WithMany()
				.HasForeignKey(book => book.PublisherId)
				.OnDelete(DeleteBehavior.SetNull)
				.IsRequired(false);

			builder.HasMany(book => book.Sources)
				.WithOne(s => s.Book)
				.HasForeignKey(s => s.BookId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasMany(book => book.Authors)
				.WithMany(author => author.Books)
				.UsingEntity<BookAuthor>(
					jt => jt.HasOne(o => o.Author)
							.WithMany()
							.HasForeignKey(o => o.AuthorId)
							.OnDelete(DeleteBehavior.Cascade)
							.IsRequired(),
					jt => jt.HasOne(o => o.Book)
							.WithMany()
							.HasForeignKey(o => o.BookId)
							.OnDelete(DeleteBehavior.Cascade)
							.IsRequired(),
					jt =>
					{
						jt.HasKey(o => new { o.BookId, o.AuthorId });
						jt.Property(o => o.BookId).ValueGeneratedNever()
								.HasConversion(bookId => bookId.Value, value => new BookId(value));
						jt.Property(o => o.AuthorId).ValueGeneratedNever()
								.HasConversion(authorId => authorId.Value, value => new AuthorId(value));
					});

			builder.HasMany(book => book.Categories)
				.WithMany(c => c.Books)
				.UsingEntity<BookCategory>(

					jt => jt.HasOne(o => o.Category)
							.WithMany()
							.HasForeignKey(o => o.CategoryId)
							.OnDelete(DeleteBehavior.Cascade)
							.IsRequired(),
					jt => jt.HasOne(o => o.Book)
							.WithMany()
							.HasForeignKey(o => o.BookId)
							.OnDelete(DeleteBehavior.Cascade)
							.IsRequired(),
					jt =>
					{
						jt.HasKey(o => new { o.BookId, o.CategoryId });
						jt.Property(o => o.BookId).ValueGeneratedNever()
								.HasConversion(bookId => bookId.Value, value => new BookId(value));
						jt.Property(o => o.CategoryId).ValueGeneratedNever()
								.HasConversion(categoryId => categoryId.Value, value => new CategoryId(value));
					});
		}

		private static void ConfigureIndexes(EntityTypeBuilder<Book> builder) =>
			builder.HasIndex(book => book.ISBN).IsUnique();
	}
}
