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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Extensions;
using Persistence.Inbox;
using Persistence.Interceptors;
using Persistence.Options;
using Persistence.Outbox;
using Persistence.Repositories;
using Service.Catalog.Domain;
using Service.Catalog.Domain.Authors;
using Service.Catalog.Domain.Books;
using Service.Catalog.Domain.BookSources;
using Service.Catalog.Domain.Categories;
using Service.Catalog.Domain.ImageSources;
using Service.Catalog.Domain.Publishers;
using Service.Catalog.Persistence.Contracts;
using Service.Catalog.Persistence.Repositories;
using Shared.Repositories;

namespace Service.Catalog.Persistence
{
	/// <summary>
	/// Represents the Catalog module persistence service installer.
	/// </summary>
	internal sealed class PersistenceServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
			=> services.AddDbContext<CatalogDbContext>((serviceProvider, options) =>
				{
					string path = Directory.GetCurrentDirectory();
					ConnectionStringOptions connectionString = serviceProvider.GetService<IOptions<ConnectionStringOptions>>()!.Value;

					options
						.UseSqlServer(
							connectionString.Value.Replace("[DataDirectory]", path),
							dbContextOptionsBuilder => dbContextOptionsBuilder.WithMigrationHistoryTableInSchema(Schemas.Catalog))
						.UseSnakeCaseNamingConvention()
						.AddInterceptors(
							new ConvertDomainEventsToOutboxMessagesInterceptor(),
							new UpdateAuditableEntitiesInterceptor());

					// TODO __##__ Enabling EF Core Sql logging for development environment.
					var environment = serviceProvider.GetService<IHostEnvironment>();
					if (environment.IsDevelopment())
					{
						var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
						options.UseLoggerFactory(loggerFactory);
					}
				})
				// TODO __##__ For additional Unit Of Work implementations add here.
				.AddScoped<ICatalogDb, CatalogDataBase>()
				// TODO __##__ IRepository implementations add here.
				.AddScoped<BookRepository>()
				.AddScoped<IRepository<Book, BookId>>(provider => provider.GetRequiredService<BookRepository>())
				.AddScoped<IBookRepository>(provider => provider.GetRequiredService<BookRepository>())
				.AddScoped<IRepository<ImageSource<BookImageType>, ImageSourceId>, Repository<ImageSource<BookImageType>, ImageSourceId, CatalogDbContext>>()
				.AddScoped<IBookSourceRepository, BookSourceRepository>()
				.AddScoped<IRepository<Category, CategoryId>, SoftDeletableRepository<Category, CategoryId, CatalogDbContext>>()
				.AddScoped<IRepository<ImageSource<CategoryImageType>, ImageSourceId>, Repository<ImageSource<CategoryImageType>, ImageSourceId, CatalogDbContext>>()
				.AddScoped<IRepository<Author, AuthorId>, SoftDeletableRepository<Author, AuthorId, CatalogDbContext>>()
				.AddScoped<IRepository<ImageSource<AuthorImageType>, ImageSourceId>, Repository<ImageSource<AuthorImageType>, ImageSourceId, CatalogDbContext>>()
				.AddScoped<IRepository<Publisher, PublisherId>, SoftDeletableRepository<Publisher, PublisherId, CatalogDbContext>>()
				.AddScoped<IRepository<ImageSource<PublisherImageType>, ImageSourceId>, Repository<ImageSource<PublisherImageType>, ImageSourceId, CatalogDbContext>>()
				.AddScoped<IRepository<OutboxMessage, Guid>, Repository<OutboxMessage, Guid, CatalogDbContext>>()
				.AddScoped<IRepository<OutboxMessageConsumer, Guid>, Repository<OutboxMessageConsumer, Guid, CatalogDbContext>>()
				.AddScoped<IRepository<InboxMessage, Guid>, Repository<InboxMessage, Guid, CatalogDbContext>>()
				.AddScoped<IRepository<InboxMessageConsumer, Guid>, Repository<InboxMessageConsumer, Guid, CatalogDbContext>>();
	}
}
