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
using Microsoft.Extensions.Options;
using Persistence.Extensions;
using Persistence.Interceptors;
using Service.Identity.Data;
using Service.Identity.Data.Constants;

namespace Service.Identity.ServiceInstallers.DataBase
{
	/// <summary>
	/// Represents the all EF Core DbContexts services installer.
	/// </summary>
	internal sealed class DataBaseServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
			=> services
				.ConfigureOptions<BlazorDbContextOptionsSetup>()
				.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
				{
					string path = Directory.GetCurrentDirectory();
					BlazorDbContextOptions connectionString = serviceProvider.GetService<IOptions<BlazorDbContextOptions>>()!.Value;

					options
						.UseSqlServer(
							connectionString.UserManagementDbConnectionString.Replace("[DataDirectory]", path),
							dbContextOptionsBuilder => dbContextOptionsBuilder.WithMigrationHistoryTableInSchema(Schemas.UserManagementSchema))
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
				});
	}
}
