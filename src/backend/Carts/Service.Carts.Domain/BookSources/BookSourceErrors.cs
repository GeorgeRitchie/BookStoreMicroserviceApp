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

namespace Service.Carts.Domain.BookSources
{
	/// <summary>
	/// Contains the book source errors.
	/// </summary>
	public static class BookSourceErrors
	{
		/// <summary>
		/// Gets <see langword="null"> book source identifier error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error NullBookSourceId()
			=> new("BookSource.NullBookSourceId", "Book source entity must have valid identifier.");

		/// <summary>
		/// Gets invalid book source format error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error InvalidFormat()
			=> new("BookSource.InvalidFormat", "Book source entity must have valid book format.");

		/// <summary>
		/// Gets book required for book source error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error BookIsRequired()
			=> new("BookSource.BookIsRequired", "For book source it is mandatory to have book.");
	}
}
