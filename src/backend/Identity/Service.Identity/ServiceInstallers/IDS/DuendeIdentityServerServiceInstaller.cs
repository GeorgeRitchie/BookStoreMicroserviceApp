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
using Persistence.Extensions;
using Service.Identity.Data;
using Service.Identity.Data.Constants;
using Service.Identity.ServiceInstallers.DataBase;

namespace Service.Identity.ServiceInstallers.IDS
{
	/// <summary>
	/// Represents the Duende IdentityServer service installer.
	/// </summary>
	internal sealed class DuendeIdentityServerServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
		{
			string dbConnectionString = configuration.GetConnectionString(nameof(BlazorDbContextOptions.IdsDbConnectionString)).Replace("[DataDirectory]", Directory.GetCurrentDirectory());

			services.AddIdentityServer(options =>
			{
				options.Events.RaiseErrorEvents = true;
				options.Events.RaiseInformationEvents = true;
				options.Events.RaiseFailureEvents = true;
				options.Events.RaiseSuccessEvents = true;

				options.UserInteraction.LoginUrl = "/Account/Login";
				options.UserInteraction.LogoutUrl = "/Account/Logout";
				options.UserInteraction.ErrorUrl = "/Error";
			})
				.AddJwtBearerClientAuthentication()
				.AddAspNetIdentity<User>()

				// Configuring IdentityServer4 to use EF Core + MS Sql Server as storage (instead of InMemory)
				// See https://identityserver4.readthedocs.io/en/latest/quickstarts/5_entityframework.html
				.AddConfigurationStore<ConfigurationDbContext>(options =>
				{
					options.ConfigureDbContext = b => b.UseSqlServer(dbConnectionString,
						sql => sql.MigrationsAssembly(AssemblyReference.Assembly.GetName().Name)
									.WithMigrationHistoryTableInSchema(Schemas.IdsConfigurationSchema))
					.UseSnakeCaseNamingConvention();
				})
				.AddOperationalStore<PersistedGrantDbContext>(options =>
				{
					options.ConfigureDbContext = b => b.UseSqlServer(dbConnectionString,
						sql => sql.MigrationsAssembly(AssemblyReference.Assembly.GetName().Name)
									.WithMigrationHistoryTableInSchema(Schemas.IdsPersistedGrant))
					.UseSnakeCaseNamingConvention();
				})
				//.AddSigningCredential(certificate);                                    // Adding real certificate
				.AddDeveloperSigningCredential();                                        // TODO __##__ Adding test certificate (WARNING USE ONLY FOR DEVELOPMENT)
		}
	}
}
