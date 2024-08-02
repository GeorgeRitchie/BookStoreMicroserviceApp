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
using Service.Catalog.Domain.Authors;
using Service.Catalog.Domain.Books;
using Service.Catalog.Domain.Categories;
using Service.Catalog.Domain.Publishers;
using Service.Catalog.Persistence.Configurations;
using Service.Catalog.Persistence.Contracts;

namespace Service.Catalog.Persistence
{
	// add-migration Init -context CatalogDbContext -o Migrations
	// update-database -context CatalogDbContext
	// migration -context CatalogDbContext
	// remove-migration -context CatalogDbContext
	// drop-database -context CatalogDbContext

	/// <summary>
	/// Represents the Catalog module database context.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="CatalogDbContext"/> class.
	/// </remarks>
	/// <param name="options">The database context options.</param>
	public sealed class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
	{
		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schemas.Catalog);

			modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

			modelBuilder.ApplyConfiguration(new ImageSourceConfigurations<AuthorImageType>());
			modelBuilder.ApplyConfiguration(new ImageSourceConfigurations<CategoryImageType>());
			modelBuilder.ApplyConfiguration(new ImageSourceConfigurations<PublisherImageType>());
			modelBuilder.ApplyConfiguration(new ImageSourceConfigurations<BookImageType>());

			// TODO __##__ For any entity to be added to db schema add property with DbSet<T> to this class, or create IEntityTypeConfiguration<T>, or have relation with already added entity.
		}
	}
}
