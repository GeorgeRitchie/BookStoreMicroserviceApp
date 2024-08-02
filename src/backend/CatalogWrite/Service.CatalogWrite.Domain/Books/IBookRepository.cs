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

using Shared.Repositories;

namespace Service.Catalog.Domain.Books
{
	/// <summary>
	/// Represents the Book repository interface.
	/// </summary>
	public interface IBookRepository : IRepository<Book, BookId>
	{
		/// <summary>
		/// Checks if the specified ISBN is unique.
		/// </summary>
		/// <param name="isbn">The ISBN to check.</param>
		/// <param name="cancellationToken">The cancelation token.</param>
		/// <returns><see langword="true"/> if the ISBN is unique, otherwise <see langword="false"/>.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="isbn"/> is <see langword="null"/>.</exception>
		Task<bool> IsISBNUniqueAsync(string isbn, CancellationToken cancellationToken = default);
	}
}
