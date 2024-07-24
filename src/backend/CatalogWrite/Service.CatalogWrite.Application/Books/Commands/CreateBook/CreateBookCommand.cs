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

using Service.CatalogWrite.Domain.Authors;
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain.Publishers;

namespace Service.CatalogWrite.Application.Books.Commands.CreateBook
{
	/// <summary>
	/// Represents the command to create a new book.
	/// </summary>
	public sealed class CreateBookCommand : ICommand<Guid>
	{
		/// <summary>
		/// Book title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Book description.
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Book ISBN if available.
		/// </summary>
		public string? ISBN { get; set; }

		/// <summary>
		/// Book language in ISO 639-1 format (e. g. ru, en, fr).
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		/// Book age rating.
		/// </summary>
		public uint AgeRating { get; set; }

		/// <summary>
		/// Book publisher identifier if available.
		/// </summary>
		public PublisherId? PublisherId { get; set; }

		/// <summary>
		/// Book published date if available.
		/// </summary>
		public DateOnly? PublishedDate { get; set; }

		/// <summary>
		/// Book authors identifiers.
		/// </summary>
		public List<AuthorId> AuthorIds { get; set; }

		/// <summary>
		/// Book categories identifiers.
		/// </summary>
		public List<CategoryId> CategoryIds { get; set; }
	}
}
