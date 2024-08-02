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

namespace Service.Catalog.Domain.Publishers
{
	/// <summary>
	/// Contains the Publisher errors.
	/// </summary>
	public static class PublisherErrors
	{
		/// <summary>
		/// Gets publisher empty name error.
		/// </summary>
		public static Error EmptyName => new(
			"Publisher.EmptyName",
			"Publisher name cannot be null, empty or white-space string.");

		/// <summary>
		/// Gets publisher empty address error.
		/// </summary>
		public static Error EmptyAddress => new(
			"Publisher.EmptyAddress",
			"Publisher's address is required");

		/// <summary>
		/// Gets publisher empty city error.
		/// </summary>
		public static Error EmptyCity => new(
			"Publisher.EmptyCity",
			"Publisher's city is required.");

		/// <summary>
		/// Gets publisher empty country error.
		/// </summary>
		public static Error EmptyCountry => new(
			"Publisher.EmptyCountry",
			"Publisher's country is required.");
	}
}
