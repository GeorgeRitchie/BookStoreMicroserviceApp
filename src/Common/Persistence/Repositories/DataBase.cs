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

using Microsoft.EntityFrameworkCore;
using Shared.Repositories;
using System.Data;

namespace Persistence.Repositories
{
	/// <inheritdoc cref="IDataBase{TDb}"/>
	/// <param name="context">The EF Core DbContext.</param>
	public class DataBase<TDb>(DbContext context) : IDataBase<TDb> where TDb : IDataBase<TDb>
	{
		protected readonly DbContext _context = context ?? throw new ArgumentNullException(nameof(context));

		/// <inheritdoc/>
		public virtual object BeginTransaction()
		{
			return _context.Database.BeginTransaction();
		}

		/// <inheritdoc/>
		public virtual object BeginTransaction(IsolationLevel isolationLevel)
		{
			return _context.Database.BeginTransaction(isolationLevel);
		}

		/// <inheritdoc/>
		public virtual async Task<object> BeginTransactionAsync(CancellationToken cancellationToken = default)
		{
			return await _context.Database.BeginTransactionAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public virtual async Task<object> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
		{
			return await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
		}

		/// <inheritdoc/>
		public virtual void CommitTransaction()
		{
			_context.Database.CommitTransaction();
		}

		/// <inheritdoc/>
		public virtual Task CommitTransactionAsync(CancellationToken cancellationToken = default)
		{
			return _context.Database.CommitTransactionAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public virtual void RollbackTransaction()
		{
			_context.Database.RollbackTransaction();
		}

		/// <inheritdoc/>
		public virtual Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
		{
			return _context.Database.RollbackTransactionAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public virtual int SaveChanges()
		{
			return _context.SaveChanges();
		}

		/// <inheritdoc/>
		public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return _context.SaveChangesAsync(cancellationToken);
		}
	}
}
