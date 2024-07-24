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

using Service.CatalogWrite.Domain.Books;
using Service.CatalogWrite.Domain.ImageSources;

namespace Service.CatalogWrite.Application.Books
{
	/// <summary>
	/// Contains the book errors.
	/// </summary>
	internal static class BookErrors
	{
		/// <summary>
		/// Gets invalid photo file error. Requires file name.
		/// </summary>
		internal static Func<string, Error> OnlyPhotoFileIsAllowed
			=> fileName => new("Book.OnlyPhotoFileIsAllowed",
								$"Passed file '{fileName}' is not a valid image file.");

		/// <summary>
		/// Gets book update operation failed error.
		/// </summary>
		internal static Error UpdateOperationFailed
			=> new("Book.UpdateOperationFailed", "Book update operation failed.");

		/// <summary>
		/// Gets book set image operation failed error. Requires file name.
		/// </summary>
		internal static Func<string, Error> SetImageOperationFailed
			=> fileName => new("Book.SetImageOperationFailed",
								$"Setting file '{fileName}' to book failed.");

		/// <summary>
		/// Gets book not found error.
		/// </summary>
		internal static Func<BookId, Error> NotFound
			=> bookId => new("Book.NotFound",
								$"Book with the identifier {bookId.Value} was not found.");

		/// <summary>
		/// Gets book image not found error.
		/// </summary>
		internal static Func<BookId, ImageSourceId, Error> BookImageNotFound
			=> (bookId, imageId) => new(
				"Book.ImageNotFound",
				$"Book with the identifier {bookId.Value} does not have image with identifier '{imageId.Value}'.");
	}
}
