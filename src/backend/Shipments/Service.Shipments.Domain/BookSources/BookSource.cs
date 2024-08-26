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

using Service.Catalog.IntegrationEvents;
using Service.Shipments.Domain.Books;

namespace Service.Shipments.Domain.BookSources
{
	/// <summary>
	/// Represents the book source entity.
	/// </summary>
	public sealed class BookSource : Entity<BookSourceId>, IAuditable
	{
		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets or sets book format (e. g. paper, pdf, txt).
		/// </summary>
		public BookFormat Format { get; set; }

		/// <summary>
		/// Gets or sets book source url. For paper format book source url is <see langword="null"/>.
		/// </summary>
		public string? Url { get; set; }

		/// <summary>
		/// Gets or sets related book entity's identifier.
		/// </summary>
		public BookId BookId { get; set; }

		/// <summary>
		/// Gets or sets related book entity.
		/// </summary>
		public Book Book { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BookSource"/> class.
		/// </summary>
		/// <param name="id">The book source identifier.</param>
		/// <param name="isDeleted">The book source deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		public BookSource(BookSourceId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BookSource"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private BookSource()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}
	}
}
