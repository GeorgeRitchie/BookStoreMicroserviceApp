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

using Newtonsoft.Json;

namespace Service.Catalog.Domain.Books
{
	/// <summary>
	/// Represents the book image type enumeration.
	/// </summary>
	public sealed class BookImageType : Enumeration<BookImageType>
	{
		public static readonly BookImageType Cover = new("Cover", 0);
		public static readonly BookImageType Icon = new("Icon", 1);
		public static readonly BookImageType Preview = new("Preview", 2);

		/// <summary>
		/// Initializes a new instance of the <see cref="BookImageType"/> class.
		/// </summary>
		/// <inheritdoc/>
		[JsonConstructor]
		private BookImageType(string name, int value) : base(name, value)
		{
		}
	}
}
