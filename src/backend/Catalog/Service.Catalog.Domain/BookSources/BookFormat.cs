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

namespace Service.Catalog.Domain.BookSources
{
	/// <summary>
	/// Represents the book source format enumeration.
	/// Enumeration value can be used as file extension.
	/// </summary>
	public sealed class BookFormat : Enumeration<BookFormat, string>
	{
		public static readonly BookFormat Paper = new("Paper", "");
		public static readonly BookFormat Pdf = new("PDF", ".pdf");
		public static readonly BookFormat Txt = new("TXT", ".txt");
		public static readonly BookFormat Epub = new("EPUB", ".epub");
		public static readonly BookFormat Fb2 = new("FB2", ".fb2");
		public static readonly BookFormat Djvu = new("DJVU", ".djvu");

		/// <summary>
		/// Initializes a new instance of the <see cref="BookFormat"/> class.
		/// </summary>
		/// <inheritdoc/>
		[JsonConstructor]
		private BookFormat(string name, string value) : base(name, value)
		{
		}

		/// <inheritdoc/>
		protected override bool IsValueEqual(string? first, string? second)
		{
			return first == second;
		}
	}
}
