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
using Persistence.Converters;
using Service.Catalog.Domain.ImageSources;
using Shared.Extensions;

namespace Service.Catalog.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="ImageSource{TEnum}"/> entity configuration.
	/// </summary>
	internal sealed class ImageSourceConfigurations<T> : IEntityTypeConfiguration<ImageSource<T>> where T : Enumeration<T>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<ImageSource<T>> builder) =>
			 builder
				.Tap(ConfigureDataStructure);

		private static void ConfigureDataStructure(EntityTypeBuilder<ImageSource<T>> builder)
		{
			builder.Property(img => img.Source).IsRequired().HasColumnName(nameof(ImageSource<T>.Source));

			builder.Property(img => img.Type).IsRequired()
				.HasColumnName(nameof(ImageSource<T>.Type))
				.HasConversion<EnumerationConverter<T, int>>();

			builder.HasBaseType<Entity<ImageSourceId>>().UseTphMappingStrategy();
		}
	}
}
