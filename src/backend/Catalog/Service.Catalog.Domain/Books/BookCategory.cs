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

using Service.Catalog.Domain.Categories;

namespace Service.Catalog.Domain.Books
{
	/// <summary>
	/// Represents the book-category join entity.
	/// </summary>
	public sealed class BookCategory
	{
		/// <summary>
		/// Gets the book identifier.
		/// </summary>
		public BookId BookId { get; private set; }

		/// <summary>
		/// Gets the book entity.
		/// </summary>
		public Book? Book { get; private set; }

		/// <summary>
		/// Gets the category identifier.
		/// </summary>
		public CategoryId CategoryId { get; private set; }

		/// <summary>
		/// Gets the category entity.
		/// </summary>
		public Category? Category { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BookCategory"/> class.
		/// </summary>
		/// <remarks>
		/// Required by EF Core.
		/// </remarks>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		private BookCategory()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BookCategory"/> class.
		/// </summary>
		/// <param name="bookId">The book identifier.</param>
		/// <param name="categoryId">The category identifier.</param>
		public BookCategory(BookId bookId, CategoryId categoryId)
		{
			BookId = bookId;
			CategoryId = categoryId;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BookCategory"/> class.
		/// </summary>
		/// <param name="book">The book.</param>
		/// <param name="category">The category.</param>
		public BookCategory(Book book, Category category)
		{
			BookId = book.Id;
			CategoryId = category.Id;
			Book = book;
			Category = category;
		}
	}
}
