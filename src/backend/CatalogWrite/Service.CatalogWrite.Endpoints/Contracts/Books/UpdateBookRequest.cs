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

namespace Service.CatalogWrite.Endpoints.Contracts.Books
{
	/// <summary>
	/// Represents the request to update the specified book.
	/// </summary>
	public sealed class UpdateBookRequest
	{
		/// <summary>
		/// The book identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// New book title or <see langword="null"/> if no change required.
		/// </summary>
		public string? Title { get; set; }

		/// <summary>
		/// New book description or <see langword="null"/> if no change required.
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// New book ISBN or <see langword="null"/> if no change required.
		/// </summary>
		public string? ISBN { get; set; }

		/// <summary>
		/// New book language or <see langword="null"/> if no change required.
		/// </summary>
		public string? Language { get; set; }

		/// <summary>
		/// New book age rating or <see langword="null"/> if no change required.
		/// </summary>
		public uint? AgeRating { get; set; }

		/// <summary>
		/// New book published date or <see langword="null"/> if no change required.
		/// </summary>
		public DateOnly? PublishedDate { get; set; }

		/// <summary>
		/// New publisher identifier, <see cref="Guid.Empty"/> to remove old or <see langword="null"/> if no change required.
		/// </summary>
		public Guid? PublisherId { get; set; }
	}
}
