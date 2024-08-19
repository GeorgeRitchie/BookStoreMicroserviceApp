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

namespace Service.Carts.Domain.Books
{
	/// <summary>
	/// Contains the Book errors.
	/// </summary>
	public static class BookErrors
	{
		/// <summary>
		/// Gets <see langword="null"> book identifier error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error NullBookId()
			=> new("Book.NullBookId", "Book entity must have valid identifier.");

		/// <summary>
		/// Gets invalid ISBN error.
		/// </summary>
		/// <param name="isbn">The invalid ISBN.</param>
		/// <returns>The error.</returns>
		public static Error InvalidISBN(string? isbn)
			=> new("Book.InvalidISBN", $"The passed ISBN '{isbn}' is not valid.");

		/// <summary>
		/// Gets empty title error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error EmptyTitle()
			=> new("Book.EmptyTitle", "Book's title cannot be null, empty or white-space string.");

		/// <summary>
		/// Gets invalid language code error.
		/// </summary>
		/// <param name="lang">The invalid language code.</param>
		/// <returns>The error.</returns>
		public static Error InvalidLanguageCode(string? lang)
			=> new("Book.InvalidLanguageCode",
				$"The provided language code is not valid ISO 639-1 formatted language code: {lang}.");
	}
}
