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
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain.ImageSources;
using Service.CatalogWrite.Persistence.Contracts;
using Shared.Extensions;

namespace Service.CatalogWrite.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="Category"/> entity configuration.
	/// </summary>
	internal sealed class CategoryConfigurations : IEntityTypeConfiguration<Category>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Category> builder) =>
			 builder
				.Tap(ConfigureDataStructure)
				.Tap(ConfigureRelationships)
				.Tap(ConfigureIndexes);

		private static void ConfigureDataStructure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable(TableNames.Categories);

			builder.HasKey(category => category.Id);

			builder.Property(category => category.Id).ValueGeneratedNever()
							.HasConversion(categoryId => categoryId.Value, value => new CategoryId(value));

			builder.Property(category => category.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.HasQueryFilter(category => category.IsDeleted == false);

			builder.Property(category => category.Title).IsRequired().HasMaxLength(100);

			builder.Property(category => category.Description).IsRequired().HasDefaultValue(string.Empty);

			builder.Property(category => category.CreatedOnUtc).IsRequired();

			builder.Property(category => category.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureRelationships(EntityTypeBuilder<Category> builder) =>
			builder.HasOne(category => category.Icon)
				.WithOne()
				.HasForeignKey<ImageSource<CategoryImageType>>()
				.OnDelete(DeleteBehavior.Cascade);

		private static void ConfigureIndexes(EntityTypeBuilder<Category> builder)
		{
			builder.HasIndex(category => category.Title).IsUnique();
		}
	}
}
