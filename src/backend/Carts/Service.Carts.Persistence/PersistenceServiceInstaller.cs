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
using Persistence.Extensions;
using Persistence.Inbox;
using Persistence.Interceptors;
using Persistence.Options;
using Persistence.Outbox;
using Persistence.Repositories;
using Service.Carts.Domain;
using Service.Carts.Persistence.Contracts;
using Shared.Repositories;

namespace Service.Carts.Persistence
{
	/// <summary>
	/// Represents the Cart module persistence service installer.
	/// </summary>
	internal sealed class PersistenceServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
			=> services.AddDbContext<CartDbContext>((serviceProvider, options) =>
			{
				string path = Directory.GetCurrentDirectory();
				ConnectionStringOptions connectionString = serviceProvider.GetService<IOptions<ConnectionStringOptions>>()!.Value;

				options
					.UseSqlServer(
						connectionString.Value.Replace("[DataDirectory]", path),
						dbContextOptionsBuilder => dbContextOptionsBuilder.WithMigrationHistoryTableInSchema(Schemas.Cart))
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
				.AddScoped<ICartDb, CartDataBase>()
				// TODO __##__ IRepository implementations add here.
				.AddScoped<IRepository<OutboxMessage, Guid>, Repository<OutboxMessage, Guid, CartDbContext>>()
				.AddScoped<IRepository<OutboxMessageConsumer, Guid>, Repository<OutboxMessageConsumer, Guid, CartDbContext>>()
				.AddScoped<IRepository<InboxMessage, Guid>, Repository<InboxMessage, Guid, CartDbContext>>()
				.AddScoped<IRepository<InboxMessageConsumer, Guid>, Repository<InboxMessageConsumer, Guid, CartDbContext>>();
	}
}
