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

using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.CatalogWrite.Domain.ImageSources;
using Service.CatalogWrite.Persistence.Contracts;
using Shared.Extensions;

namespace Service.CatalogWrite.Persistence.Configurations
{
	/// <summary>
	/// Represents the base class for TPH strategy of EF Core for <see cref="ImageSource{TEnum}"/> generic entity configuration.
	/// </summary>
	internal sealed class ImageSourceBaseType : IEntityTypeConfiguration<Entity<ImageSourceId>>
	{
		/// <inheritdoc/>
		public void Configure(EntityTypeBuilder<Entity<ImageSourceId>> builder) =>
			builder.Tap(ConfigureDataStructure);

		private static void ConfigureDataStructure(EntityTypeBuilder<Entity<ImageSourceId>> builder)
		{
			builder.ToTable(TableNames.ImageSources);

			builder.HasKey(img => img.Id);

			builder.Property(img => img.Id).ValueGeneratedNever()
									.HasConversion(imgId => imgId.Value, value => new ImageSourceId(value));

			builder.Property(img => img.IsDeleted).IsRequired().HasDefaultValue(false);
		}
	}
}
