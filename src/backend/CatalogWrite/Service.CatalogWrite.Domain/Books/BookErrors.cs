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

namespace Service.CatalogWrite.Domain.Books
{
	/// <summary>
	/// Contains the Book errors.
	/// </summary>
	public static class BookErrors
	{
		/// <summary>
		/// Gets ISBN not unique error.
		/// </summary>
		public static Func<string, Error> ISBNIsNotUnique
			=> isbn => new("Book.ISBNIsNotUnique", $"The specified ISBN '{isbn}' is already in use.");

		/// <summary>
		/// Gets author required error used for books with no author.
		/// </summary>
		public static Error AuthorIsRequired
			=> new("Book.AuthorIsRequired", "Book must have at least one author.");

		/// <summary>
		/// Gets category required error used for books with no category.
		/// </summary>
		public static Error CategoryIsRequired
			=> new("Book.CategoryIsRequired", "Book must have at leas one category.");

		/// <summary>
		/// Gets empty title error.
		/// </summary>
		public static Error EmptyTitle
			=> new("Book.EmptyTitle", "Book's title cannot be null, empty or white-space string.");

		/// <summary>
		/// Gets invalid language code error.
		/// </summary>
		public static Func<string?, Error> InvalidLanguageCode
			=> lang => new(
				"Book.InvalidLanguageCode",
				$"The provided language code is not valid ISO 639-1 formatted language code: {lang}.");
	}
}
