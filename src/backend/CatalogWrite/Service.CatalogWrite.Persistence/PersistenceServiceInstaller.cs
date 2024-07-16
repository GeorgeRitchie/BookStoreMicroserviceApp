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

using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Extensions;
using Persistence.Inbox;
using Persistence.Interceptors;
using Persistence.Options;
using Persistence.Repositories;
using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.Authors;
using Service.CatalogWrite.Domain.Books;
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain.Publishers;
using Service.CatalogWrite.Persistence.Contracts;
using Service.CatalogWrite.Persistence.Repositories;
using Shared.Repositories;

namespace Service.CatalogWrite.Persistence
{
	/// <summary>
	/// Represents the CatalogWrite module persistence service installer.
	/// </summary>
	internal sealed class PersistenceServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
			=> services.AddDbContext<CatalogWriteDbContext>((serviceProvider, options) =>
				{
					string path = Directory.GetCurrentDirectory();
					ConnectionStringOptions connectionString = serviceProvider.GetService<IOptions<ConnectionStringOptions>>()!.Value;

					options
						.UseSqlServer(
							connectionString.Value.Replace("[DataDirectory]", path),
							dbContextOptionsBuilder => dbContextOptionsBuilder.WithMigrationHistoryTableInSchema(Schemas.CatalogWrite))
						.UseSnakeCaseNamingConvention()
						.AddInterceptors(
							new ConvertDomainEventsToOutboxMessagesInterceptor(),
							new UpdateAuditableEntitiesInterceptor());
				})
				// TODO ## For additional Unit Of Work implementations add here.
				.AddScoped<ICatalogDb, CatalogWriteDataBase>()
				// TODO ## IRepository implementations add here.
				.AddScoped<BookRepository>()
				.AddScoped<IRepository<Book, BookId>>(provider => provider.GetRequiredService<BookRepository>())
				.AddScoped<IBookRepository>(provider => provider.GetRequiredService<BookRepository>())
				.AddScoped<IRepository<Category, CategoryId>, Repository<Category, CategoryId, CatalogWriteDbContext>>()
				.AddScoped<IRepository<Author, AuthorId>, Repository<Author, AuthorId, CatalogWriteDbContext>>()
				.AddScoped<IRepository<Publisher, PublisherId>, Repository<Publisher, PublisherId, CatalogWriteDbContext>>();
	}
}
