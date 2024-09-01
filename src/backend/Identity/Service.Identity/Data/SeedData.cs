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

using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Identity.Options;
using Service.Identity.ServiceInstallers.IDS;
using Service.Identity.Services;

namespace Service.Identity.Data
{
	public static class SeedData
	{
		private static List<Role> roles =
		[
			new Role(Guid.NewGuid(), Role.Admin)
			{
				Permissions =
				[
					new Permission
					{
						Id = Guid.NewGuid(),
						Name = "UpdateShipment",
					},
					new Permission
					{
						Id = Guid.NewGuid(),
						Name = "ReadShipment",
					},
				],
			},
			new Role(Guid.NewGuid(), Role.User)
			{
				Permissions =
				[

				],
			},
		];

		public static void EnsureSeedData(WebApplication app)
		{
			using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var blazorOptions = scope.ServiceProvider.GetRequiredService<IOptions<BlazorOptions>>()!.Value;

			var userManagementDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

			if (blazorOptions.EnableAutoMigration)
				userManagementDb.Database.Migrate();

			// TODO instead of these three methods create CRUD UI operations for Permission, Role and RolePermission classes
			UpdatePermissions(userManagementDb);
			UpdateRoles(userManagementDb);
			UpdateRolePermissions(userManagementDb);
			AddAdminIfNotExist(scope);

			MigrateAndSeedIdsDatabases(scope);
		}

		private static void AddAdminIfNotExist(IServiceScope scope)
		{
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
			var users = userManager.GetUsersInRoleAsync(Role.Admin).Result;
			var smtpOptions = scope.ServiceProvider.GetRequiredService<IOptions<SmtpOptions>>().Value;
			if (users.Count == 0)
			{
				var admin = new User()
				{
					FirstName = smtpOptions.From,
					LastName = smtpOptions.From,
					UserName = smtpOptions.From,  // Set the username directly here
					Email = smtpOptions.From,      // Set the email directly here
					EmailConfirmed = true          // Ensure the email is marked as confirmed
				};

				var result = userManager.CreateAsync(admin, "Hello1!").Result;
				if (result.Succeeded == false)
				{
					throw new Exception(result.Errors.First().Code);
				}
				else
				{
					var roleResult = userManager.AddToRoleAsync(admin, Role.Admin).Result;
					if (roleResult.Succeeded == false)
					{
						throw new Exception(roleResult.Errors.First().Code);
					}
				}
			}
		}

		private static void UpdateRolePermissions(ApplicationDbContext userManagementDb)
		{
			var dbRoles = userManagementDb.Roles.Include(i => i.Permissions).ToList();
			foreach (var dbRole in dbRoles)
			{
				var role = roles.First(i => i.Name == dbRole.Name);
				if (dbRole.Permissions.Count != role.Permissions.Count)
				{
					var permissionsNames = role.Permissions.Select(i => i.Name).ToList();
					dbRole.Permissions.Clear();
					dbRole.Permissions = userManagementDb.Permissions.Where(i => permissionsNames.Contains(i.Name)).ToList();
					userManagementDb.SaveChanges();
				}
			}
		}

		private static void UpdateRoles(ApplicationDbContext userManagementDb)
		{
			var dbRoles = userManagementDb.Roles.ToList();
			var rolesToAdd = roles.Except(dbRoles, new RoleNameComparer()).ToList();
			if (rolesToAdd.Count != 0)
			{
				userManagementDb.Roles.AddRange(rolesToAdd);
				userManagementDb.SaveChanges();
			}
			var rolesToRemove = dbRoles.Except(roles, new RoleNameComparer()).ToList();
			if (rolesToRemove.Count != 0)
			{
				userManagementDb.Roles.RemoveRange(rolesToRemove);
				userManagementDb.SaveChanges();
			}
		}

		private static void UpdatePermissions(ApplicationDbContext userManagementDb)
		{
			var dbPermissions = userManagementDb.Permissions.ToList();
			var permissions = roles.SelectMany(i => i.Permissions).Distinct().ToList();
			var permissionsToAdd = permissions.Except(dbPermissions, new PermissionNameComparer()).ToList();
			if (permissionsToAdd.Count != 0)
			{
				userManagementDb.Permissions.AddRange(permissionsToAdd);
				userManagementDb.SaveChanges();
			}
			var permissionsToRemove = dbPermissions.Except(permissions, new PermissionNameComparer()).ToList();
			if (permissionsToRemove.Count != 0)
			{
				userManagementDb.Permissions.RemoveRange(permissionsToRemove);
				userManagementDb.SaveChanges();
			}
		}

		private static void MigrateAndSeedIdsDatabases(IServiceScope scope)
		{
			scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

			var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
			context.Database.Migrate();

			if (!context.Clients.Any())
			{
				foreach (var client in IdsConfig.Clients.ToList())
				{
					context.Clients.Add(client.ToEntity());
				}
				context.SaveChanges();
			}

			if (!context.IdentityResources.Any())
			{
				foreach (var resource in IdsConfig.IdentityResources.ToList())
				{
					context.IdentityResources.Add(resource.ToEntity());
				}
				context.SaveChanges();
			}

			if (!context.ApiScopes.Any())
			{
				foreach (var resource in IdsConfig.ApiScopes.ToList())
				{
					context.ApiScopes.Add(resource.ToEntity());
				}
				context.SaveChanges();
			}

			if (!context.ApiResources.Any())
			{
				foreach (var resource in IdsConfig.ApiResources.ToList())
				{
					context.ApiResources.Add(resource.ToEntity());
				}
				context.SaveChanges();
			}
		}
	}

	internal class RoleNameComparer : IEqualityComparer<Role>
	{
		public bool Equals(Role x, Role y)
		{
			if (x == null || y == null)
				return false;

			return x.NormalizedName == y.NormalizedName;
		}

		public int GetHashCode(Role obj)
		{
			return obj.Name?.GetHashCode() ?? 0;
		}
	}

	internal class PermissionNameComparer : IEqualityComparer<Permission>
	{
		public bool Equals(Permission x, Permission y)
		{
			if (x == null || y == null)
				return false;

			return x.Name == y.Name;
		}

		public int GetHashCode(Permission obj)
		{
			return obj.Name?.GetHashCode() ?? 0;
		}
	}
}
