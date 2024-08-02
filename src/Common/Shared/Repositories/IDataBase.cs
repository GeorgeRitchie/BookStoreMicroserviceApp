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

using System.Data;

namespace Shared.Repositories
{
	/// <summary>
	/// Represents general database interface.
	/// </summary>
	/// <typeparam name="TDb">Specific database type.</typeparam>
	public interface IDataBase<TDb> where TDb : IDataBase<TDb>
	{
		/// <summary>
		/// Starts a new database transaction.
		/// </summary>
		/// <returns>The transaction initiated.</returns>
		object BeginTransaction();

		/// <summary>
		/// Asynchronously starts a new database transaction.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
		/// <returns>The transaction initiated.</returns>
		Task<object> BeginTransactionAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Starts a new database transaction with the specified isolation level.
		/// </summary>
		/// <param name="isolationLevel">The isolation level of the transaction.</param>
		/// <returns>The transaction initiated.</returns>
		object BeginTransaction(IsolationLevel isolationLevel);

		/// <summary>
		/// Asynchronously starts a new database transaction with the specified isolation level.
		/// </summary>
		/// <param name="isolationLevel">The isolation level of the transaction.</param>
		/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
		/// <returns>The transaction initiated.</returns>
		Task<object> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);

		/// <summary>
		/// Commits the current database transaction.
		/// </summary>
		void CommitTransaction();

		/// <summary>
		/// Asynchronously commits the current database transaction.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task CommitTransactionAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Rolls back the current database transaction, undoing all changes made in the transaction.
		/// </summary>
		void RollbackTransaction();

		/// <summary>
		/// Asynchronously rolls back the current database transaction, undoing all changes made in the transaction.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

		/// <summary>
		/// Saves all changes made in this context to the database.
		/// </summary>
		/// <returns>The number of state entries written to the database.</returns>
		int SaveChanges();

		/// <summary>
		/// Asynchronously saves all changes made in this context to the database.
		/// </summary>
		/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
		/// <returns>The number of state entries written to the database.</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
