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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.Interceptors
{
	/// <summary>
	/// Represents the interceptor for updating auditable entity values.
	/// </summary>
	public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UpdateAuditableEntitiesInterceptor"/> class.
		/// </summary>
		public UpdateAuditableEntitiesInterceptor()
		{
		}

		/// <inheritdoc />
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
			DbContextEventData eventData,
			InterceptionResult<int> result,
			CancellationToken cancellationToken = default)
		{
			if (eventData.Context is null)
			{
				return base.SavingChangesAsync(eventData, result, cancellationToken);
			}

			DateTime utcNow = DateTime.UtcNow;

			foreach (EntityEntry<IAuditable> auditable in GetAuditableEntities(eventData.Context))
			{
				if (auditable.State == EntityState.Added)
				{
					auditable.Property(nameof(IAuditable.CreatedOnUtc)).CurrentValue = utcNow;
				}

				if (auditable.State == EntityState.Modified)
				{
					auditable.Property(nameof(IAuditable.ModifiedOnUtc)).CurrentValue = utcNow;
				}
			}

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private static IEnumerable<EntityEntry<IAuditable>> GetAuditableEntities(DbContext dbContext)
			=> dbContext.ChangeTracker.Entries<IAuditable>();
	}
}
