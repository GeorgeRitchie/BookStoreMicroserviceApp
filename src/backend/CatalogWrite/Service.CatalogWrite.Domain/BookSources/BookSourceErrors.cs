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

namespace Service.CatalogWrite.Domain.BookSources
{
	/// <summary>
	/// Contains the book source errors.
	/// </summary>
	public static class BookSourceErrors
	{
		/// <summary>
		/// Gets invalid source url error.
		/// </summary>
		public static Error InvalidSourceUrl
			=> new("BookSource.InvalidSourceUrl", "For electronic book the valid url to source file is required.");

		/// <summary>
		/// Gets invalid preview url error.
		/// </summary>
		public static Error InvalidPreviewUrl
			=> new("BookSource.InvalidPreviewUrl", "Invalid url for book preview source.");

		/// <summary>
		/// Gets book required for book source error.
		/// </summary>
		public static Error BookIsRequired
			=> new("BookSource.BookIsRequired", "For book source it is mandatory to have book.");
	}
}
