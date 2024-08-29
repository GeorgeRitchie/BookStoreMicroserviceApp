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

using Service.Shipments.Domain.BookSources;

namespace Service.Shipments.Domain.Books
{
	/// <summary>
	/// Represents the Book entity.
	/// </summary>
	public sealed class Book : Entity<BookId>, IAuditable
	{
		private List<BookSource> sources = [];

		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets or sets the book title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the book description.
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the book ISBN if available.
		/// </summary>
		public string? ISBN { get; set; }

		/// <summary>
		/// Gets or sets the book language in ISO 639-1 format (e. g. ru, en, fr).
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		/// Gets or sets the book age rating.
		/// </summary>
		public uint AgeRating { get; set; }

		/// <summary>
		/// Gets the book source information to access (e. g. format & url to download).
		/// </summary>
		public List<BookSource> Sources => sources;

		/// <summary>
		/// Initializes a new instance of the <see cref="Book"/> class.
		/// </summary>
		/// <param name="id">The book identifier.</param>
		/// <param name="isDeleted">The book deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		public Book(BookId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Book"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Book()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}
	}
}
