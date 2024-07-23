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
using Service.CatalogWrite.Domain.Publishers;
using Service.CatalogWrite.Persistence.Contracts;
using Shared.Extensions;

namespace Service.CatalogWrite.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="Publisher"/> entity configuration.
	/// </summary>
	internal sealed class PublisherConfigurations : IEntityTypeConfiguration<Publisher>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Publisher> builder) =>
			 builder
				.Tap(ConfigureDataStructure)
				.Tap(ConfigureRelationships);

		private static void ConfigureDataStructure(EntityTypeBuilder<Publisher> builder)
		{
			builder.ToTable(TableNames.Publishers);

			builder.HasKey(publisher => publisher.Id);

			builder.Property(publisher => publisher.Id).ValueGeneratedNever()
							.HasConversion(publisherId => publisherId.Value, value => new PublisherId(value));

			builder.Property(publisher => publisher.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.HasQueryFilter(publisher => publisher.IsDeleted == false);

			builder.Property(publisher => publisher.Name).IsRequired().HasMaxLength(100);

			builder.Property(publisher => publisher.Address).IsRequired().HasMaxLength(200);

			builder.Property(publisher => publisher.City).IsRequired().HasMaxLength(100);

			builder.Property(publisher => publisher.Country).IsRequired().HasMaxLength(100);

			builder.OwnsOne(publisher => publisher.Email, email =>
			{
				email.Property(e => e.EmailAddress).IsRequired().HasMaxLength(200);
			});

			builder.OwnsOne(publisher => publisher.Website, website =>
			{
				website.Property(e => e.Url).IsRequired().HasMaxLength(200);
			});

			builder.OwnsOne(publisher => publisher.PhoneNumber, website =>
			{
				website.Property(e => e.Number).IsRequired().HasMaxLength(15);
			});

			builder.Property(publisher => publisher.CreatedOnUtc).IsRequired();

			builder.Property(publisher => publisher.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureRelationships(EntityTypeBuilder<Publisher> builder) =>
			builder.HasMany(publisher => publisher.Images)
				.WithOne()
				.OnDelete(DeleteBehavior.Cascade);
	}
}
