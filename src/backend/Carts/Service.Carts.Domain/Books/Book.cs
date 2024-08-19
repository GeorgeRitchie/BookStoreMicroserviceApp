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

using Service.Carts.Domain.BookSources;
using System.Text.RegularExpressions;

namespace Service.Carts.Domain.Books
{
	/// <summary>
	/// Represents the Book entity.
	/// </summary>
	public sealed class Book : Entity<BookId>, IAuditable
	{
		private const string pattern_ISO169_1_LangCode = "^[a-z]{2}$";
		private const string isbnPattern = @"^(?:\d[\ |-]?){9}[\d|X]$|^(?:97[89][\ |-]?\d{1,5}[\ |-]?\d{1,7}[\ |-]?\d{1,7}[\ |-]?\d)$";

		private List<BookSource> sources = [];

		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the book title.
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// Gets the book description.
		/// </summary>
		public string Description { get; private set; } = string.Empty;

		/// <summary>
		/// Gets the book ISBN if available.
		/// </summary>
		public string? ISBN { get; private set; }

		/// <summary>
		/// Gets the book language in ISO 639-1 format (e. g. ru, en, fr).
		/// </summary>
		public string Language { get; private set; }

		/// <summary>
		/// Gets the book cover image if available.
		/// </summary>
		public string? Cover { get; private set; }

		/// <summary>
		/// Gets the book source information to access (e. g. format & url to download).
		/// </summary>
		public IReadOnlyCollection<BookSource> Sources => sources;

		/// <summary>
		/// Initializes a new instance of the <see cref="Book"/> class.
		/// </summary>
		/// <param name="id">The book identifier.</param>
		/// <param name="isDeleted">The book deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private Book(BookId id, bool isDeleted = false) : base(id, isDeleted)
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

		/// <summary>
		/// Creates a new <see cref="Book"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="bookId">Book identifier.</param>
		/// <param name="title">Book title.</param>
		/// <param name="isbn">Book ISBN.</param>
		/// <param name="language">Book language.</param>
		/// <param name="cover">Book cover image source.</param>
		/// <param name="description">Book description.</param>
		/// <returns>The new <see cref="Book"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Book> Create(
			BookId bookId,
			string title,
			string? isbn,
			string language,
			string? cover,
			string description)
			=> Result.Success()
				.Ensure(() => string.IsNullOrWhiteSpace(title) == false, BookErrors.EmptyTitle())
				.Ensure(() => language != null
								&& Regex.IsMatch(language, pattern_ISO169_1_LangCode, RegexOptions.IgnoreCase),
						BookErrors.InvalidLanguageCode(language))
				.Ensure(() => isbn is null
							|| Regex.IsMatch(isbn, isbnPattern), BookErrors.InvalidISBN(isbn))
				.Ensure(() => bookId is not null, BookErrors.NullBookId())
				.Map(() => new Book(bookId, false)
				{
					Title = title,
					Description = description ?? string.Empty,
					ISBN = isbn,
					Language = language,
					Cover = cover,
				});

		/// <summary>
		/// Changes book information.
		/// </summary>
		/// <param name="title">Book title.</param>
		/// <param name="isbn">Book ISBN.</param>
		/// <param name="language">Book language.</param>
		/// <param name="cover">Book cover image source.</param>
		/// <param name="description">Book description.</param>
		/// <returns>The updated book.</returns>
		public Result<Book> UpdateAsync(
			string title,
			string? isbn,
			string language,
			string? cover,
			string description)
			=> Result.Success(this)
					.Ensure(book => string.IsNullOrWhiteSpace(title) == false, BookErrors.EmptyTitle())
					.Ensure(book => language != null
									&& Regex.IsMatch(language, pattern_ISO169_1_LangCode, RegexOptions.IgnoreCase),
							BookErrors.InvalidLanguageCode(language))
					.Ensure(book => isbn is null
								|| Regex.IsMatch(isbn, isbnPattern), BookErrors.InvalidISBN(isbn))
					.Tap<Book>(book =>
					{
						Title = title;
						Description = description ?? string.Empty;
						ISBN = isbn;
						Language = language;
						Cover = cover;
					});
	}
}
