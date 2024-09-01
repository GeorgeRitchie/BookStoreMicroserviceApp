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

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Data.Constants;

namespace Service.Identity.Data
{
	// add-migration Init -context ApplicationDbContext -o Data/Migrations/ApplicationDbMigrations
	// update-database -context ApplicationDbContext
	// migration -context ApplicationDbContext
	// remove-migration -context ApplicationDbContext
	// drop-database -context ApplicationDbContext
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: IdentityDbContext<User, Role, Guid>(options)
	{
		public DbSet<Permission> Permissions { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.HasDefaultSchema(Schemas.UserManagementSchema);

			base.OnModelCreating(builder);

			builder.Entity<Permission>()
				.HasMany(i => i.Roles)
				.WithMany(i => i.Permissions);
		}
	}
}
