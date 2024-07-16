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
using Shared.Repositories;

namespace Persistence.Repositories
{
	/// <summary>
	/// Extends the <see cref="Repository{TEntity, TEntityId}"/> by providing additional functionality for hard deletion of entities.
	/// </summary>
	/// <typeparam name="TEntity">The entity type this repository manages.</typeparam>
	/// <typeparam name="TEntityId">The type of id of entity.</typeparam>
	/// <typeparam name="TDbContext">The db context this repository belongs to.</typeparam>
	/// <param name="context">The DbContext that manages this entity.</param>
	public class ExtendedSoftDeletableRepository<TEntity, TEntityId, TDbContext>(TDbContext context)
		: SoftDeletableRepository<TEntity, TEntityId, TDbContext>(context),
		IExtendedWriteOnlyRepository<TEntity, TEntityId>
			where TEntity : class, ISoftDeletable<TEntityId>
			where TEntityId : IEntityId
			where TDbContext : DbContext
	{
		/// <inheritdoc/>
		public void HardDelete(TEntityId id)
		{
			HardDelete(dbSet.Find(id)!);
		}

		/// <inheritdoc/>
		public void HardDelete(TEntity entity)
		{
			dbSet.Remove(entity);
		}

		/// <inheritdoc/>
		public void HardDeleteRange(IEnumerable<TEntity> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
