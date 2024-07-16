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
	/// Represents repository interface with soft deletion support.
	/// </summary>
	/// <typeparam name="TEntity">The entity type this repository manages.</typeparam>
	/// <typeparam name="TEntityId">The type of id of entity.</typeparam>
	/// <typeparam name="TDbContext">The db context this repository belongs to.</typeparam>
	/// <param name="context">The DbContext that manages this entity.</param>
	/// <remarks>
	/// This implementation supports soft deletion, which means entities are not removed from the database but marked as deleted.
	/// </remarks>
	public class SoftDeletableRepository<TEntity, TEntityId, TDbContext>(TDbContext context) : IRepository<TEntity, TEntityId>,
															IWriteOnlyRepository<TEntity, TEntityId>,
															IReadOnlyRepository<TEntity>
															where TEntity : class, ISoftDeletable<TEntityId>
															where TEntityId : IEntityId
															where TDbContext : DbContext
	{
		protected readonly DbSet<TEntity> dbSet = context?.Set<TEntity>() ?? throw new ArgumentNullException(nameof(context));

		/// <inheritdoc/>
		public virtual TEntity Create(TEntity entity)
		{
			return dbSet.Add(entity).Entity;
		}

		/// <inheritdoc/>
		public virtual void CreateRange(IEnumerable<TEntity> entities)
		{
			dbSet.AddRange(entities);
		}

		/// <inheritdoc/>
		public virtual void Update(TEntity entity)
		{
			dbSet.Update(entity);
		}

		/// <inheritdoc/>
		public virtual void UpdateRange(IEnumerable<TEntity> entities)
		{
			dbSet.UpdateRange(entities);
		}

		/// <inheritdoc/>
		public virtual void Delete(TEntityId id)
		{
			// TODO test whether this will work when providing specific implementation of IEntityId
			Delete(dbSet.Find(id)!);
		}

		/// <inheritdoc/>
		public virtual void Delete(TEntity entity)
		{
			// This app based on soft deletion, that is why this action is prohibited
			// dbSet.Remove(entity);

			entity.MarkAsDeleted();
			Update(entity);
		}

		/// <inheritdoc/>
		public virtual void DeleteRange(IEnumerable<TEntity> entities)
		{
			// This app based on soft deletion, that is why this action is prohibited
			// dbSet.RemoveRange(entities);

			UpdateRange(entities.Select(e => { e.MarkAsDeleted(); return e; }));
		}

		/// <inheritdoc/>
		public virtual IQueryable<TEntity> GetAll()
		{
			return dbSet;
		}

		/// <inheritdoc/>
		public virtual IQueryable<TEntity> GetAllIgnoringQueryFilters()
		{
			return dbSet.IgnoreQueryFilters();
		}

		/// <inheritdoc/>
		public virtual IQueryable<TEntity> GetAllAsNoTracking()
		{
			return dbSet.AsNoTracking();
		}

		/// <inheritdoc/>
		public virtual IQueryable<TEntity> GetAllIgnoringQueryFiltersAsNoTracking()
		{
			return dbSet.IgnoreQueryFilters().AsNoTracking();
		}
	}
}
