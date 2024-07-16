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
using Persistence.Repositories;
using Service.CatalogWrite.Domain.Books;

namespace Service.CatalogWrite.Persistence.Repositories
{
	/// <summary>
	/// Represents repository implementation for <see cref="Book"/> entity.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="BookRepository"/> class.
	/// </remarks>
	/// <param name="dbContext">The database context.</param>
	internal sealed class BookRepository(CatalogWriteDbContext context)
		: SoftDeletableRepository<Book, BookId, CatalogWriteDbContext>(context), IBookRepository
	{
		/// <inheritdoc/>
		public async Task<bool> IsISBNUniqueAsync(string isbn, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(isbn);

			return (await dbSet.AnyAsync(b => b.ISBN == isbn, cancellationToken)) == false;
		}
	}
}
