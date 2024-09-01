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

using Duende.IdentityServer.EntityFramework.DbContexts;
using Service.Identity.Data.Constants;
using Microsoft.EntityFrameworkCore;

namespace Service.Identity.Data
{
	// Add-Migration Init -Context PersistedGrantDbContext -OutputDir Data/Migrations/PersistedGrantDb
	// update-database -Context PersistedGrantDbContext
	// remove-migration -context "PersistedGrantDbContext"
	public class PersistedGrantDbContext(DbContextOptions<PersistedGrantDbContext> options)
		: PersistedGrantDbContext<PersistedGrantDbContext>(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schemas.IdsPersistedGrant);

			base.OnModelCreating(modelBuilder);
		}
	}
}
