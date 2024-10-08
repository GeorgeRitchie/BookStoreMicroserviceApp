﻿/* 
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

using Service.Catalog.Domain.Books;
using Service.Catalog.Domain.BookSources;

namespace Service.Catalog.Application.BooSources
{
	/// <summary>
	/// Contains the book source errors.
	/// </summary>
	internal static class BookSourceErrors
	{
		/// <summary>
		/// Gets book source not found error.
		/// </summary>
		internal static Func<BookSourceId, Error> NotFound
			=> bookSourceId => new NotFoundError("BookSource.NotFound",
								$"Book source with the identifier {bookSourceId.Value} was not found.");

		/// <summary>
		/// Gets book not found error.
		/// </summary>
		internal static Func<BookId, Error> BookNotFound
			=> bookId => new NotFoundError("BookSource.BookNotFound",
								$"Book with the identifier {bookId.Value} was not found.");

		/// <summary>
		/// Gets invalid book format, used to create <see cref="BookFormat"/>.
		/// </summary>
		internal static Error InvalidBookFormat
			=> new("BookSource.InvalidBookFormat", "Invalid book format.");

		/// <summary>
		/// Gets error for case when some required book sources not found.
		/// </summary>
		internal static Error SomeEntitiesNotFound
			=> new NotFoundError("BookSource.SomeEntitiesNotFound", "Some book sources not found.");

		/// <summary>
		/// Gets error quantity decrease to negative value.
		/// </summary>
		internal static Func<BookSourceId, Error> QuantityLessThanZero
			=> bookSourceId => new("BookSource.InvalidQuantityDecrease",
					$"The quantity for book source {bookSourceId.Value} cannot be decreased to a negative value.");
	}
}
