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
using Service.CatalogWrite.Persistence.Contracts;
using Shared.Extensions;

namespace Service.CatalogWrite.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="Author"/> entity configuration.
	/// </summary>
	internal sealed class AuthorConfigurations : IEntityTypeConfiguration<Author>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Author> builder) =>
			 builder
				.Tap(ConfigureDataStructure)
				.Tap(ConfigureRelationships);

		private static void ConfigureDataStructure(EntityTypeBuilder<Author> builder)
		{
			builder.ToTable(TableNames.Authors);

			builder.HasKey(author => author.Id);

			builder.Property(author => author.Id).ValueGeneratedNever()
													 .HasConversion(userId => userId.Value, value => new AuthorId(value));

			builder.Property(author => author.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.HasQueryFilter(author => author.IsDeleted == false);

			builder.Property(author => author.FirstName).IsRequired().HasMaxLength(100);

			builder.Property(author => author.LastName).IsRequired().HasMaxLength(100);

			builder.Property(author => author.Description).IsRequired().HasDefaultValue(string.Empty);

			builder.OwnsOne(author => author.Email, email =>
			{
				email.Property(e => e.EmailAddress).IsRequired().HasMaxLength(200);

				email.HasIndex(e => e.EmailAddress);
			});

			builder.OwnsOne(author => author.Website, website =>
			{
				website.Property(e => e.Url).IsRequired().HasMaxLength(200);
			});

			builder.Property(author => author.CreatedOnUtc).IsRequired();

			builder.Property(author => author.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureRelationships(EntityTypeBuilder<Author> builder) =>
			builder.HasMany(author => author.Images)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);
	}
}
