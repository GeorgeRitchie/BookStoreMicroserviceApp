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
using Service.CatalogWrite.Domain.Books.Events;
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain.ImageSources;
using Service.CatalogWrite.Domain.Publishers;
using System.Text.RegularExpressions;

namespace Service.CatalogWrite.Domain.Books
{
	/// <summary>
	/// Represents the Book entity.
	/// </summary>
	public sealed class Book : Entity<BookId>, IAuditable
	{
		private const string pattern_ISO169_1_LangCode = "^[a-z]{2}$";

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
		/// Gets the book age rating.
		/// </summary>
		public uint AgeRating { get; private set; }

		/// <summary>
		/// Gets the book authors information.
		/// </summary>
		public IReadOnlyCollection<Author> Authors { get; private set; }

		/// <summary>
		/// Gets the book categories.
		/// </summary>
		public IReadOnlyCollection<Category> Categories { get; private set; }

		/// <summary>
		/// Gets the book publisher identifier if available.
		/// </summary>
		public PublisherId? PublisherId { get; private set; }

		/// <summary>
		/// Gets the book publisher information if available.
		/// </summary>
		public Publisher? Publisher { get; private set; }

		/// <summary>
		/// Gets the book published information if available.
		/// </summary>
		public DateOnly? PublishedDate { get; private set; }

		/// <summary>
		/// Gets the book images.
		/// </summary>
		public IReadOnlyCollection<ImageSource<BookImageType>> Images { get; private set; } = [];

		/// <summary>
		/// Gets the book source information to access (e. g. format & url to download).
		/// </summary>
		public IReadOnlyCollection<BookSource> Sources { get; private set; } = [];

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
		/// <param name="title">Book title.</param>
		/// <param name="isbn">Book ISBN.</param>
		/// <param name="language">Book language.</param>
		/// <param name="ageRating">Book age rating.</param>
		/// <param name="authors">Book authors.</param>
		/// <param name="categories">Book categories.</param>
		/// <param name="sources">Book sources.</param>
		/// <param name="images">Book images.</param>
		/// <param name="bookRepository">Book repository.</param>
		/// <param name="description">Book description.</param>
		/// <param name="publisher">Book publisher.</param>
		/// <param name="publishedDate">Book published date.</param>
		/// <param name="cancellationToken">Cancelation token.</param>
		/// <returns>The new <see cref="Book"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static async Task<Result<Book>> CreateAsync(
			string title,
			string? isbn,
			string language,
			uint ageRating,
			IEnumerable<Author> authors,
			IEnumerable<Category> categories,
			IEnumerable<BookSource> sources,
			IEnumerable<ImageSource<BookImageType>> images,
			IBookRepository bookRepository,
			string? description = null,
			Publisher? publisher = null,
			DateOnly? publishedDate = null,
			CancellationToken cancellationToken = default)
		{
			var result = Result.Success();
			// TODO make setting related entities better
			result
				.Ensure(() => string.IsNullOrWhiteSpace(title) == false, BookErrors.EmptyTitle)
				.Ensure(() => language != null
								&& Regex.IsMatch(language, pattern_ISO169_1_LangCode, RegexOptions.IgnoreCase),
						BookErrors.InvalidLanguageCode(language))
				.Ensure(() => authors?.Any() == true, BookErrors.AuthorIsRequired)
				.Ensure(() => categories?.Any() == true, BookErrors.CategoryIsRequired);

			if (isbn != null && await bookRepository.IsISBNUniqueAsync(isbn, cancellationToken) == false)
			{
				result.Ensure(() => false, BookErrors.ISBNIsNotUnique(isbn));
			}

			if (result.IsSuccess)
			{
				var book = new Book(new BookId(Guid.NewGuid()), false)
				{
					Title = title,
					Description = description ?? string.Empty,
					ISBN = isbn,
					AgeRating = ageRating,
					Language = language,
					Authors = authors.ToList(),
					Categories = categories.ToList(),
					PublisherId = publisher?.Id,
					Publisher = publisher,
					PublishedDate = publishedDate,
					Images = images?.ToList() ?? [],
					Sources = sources?.ToList() ?? [],
				};

				book.RaiseDomainEvent(new BookCreatedDomainEvent(
								Guid.NewGuid(),
								DateTime.UtcNow,
								book.Id,
								book.Title,
								book.Description,
								book.ISBN,
								book.Language,
								book.AgeRating,
								book.Authors,
								book.Categories,
								book.Publisher,
								book.PublishedDate,
								book.Sources));

				return Result.Success(book);
			}

			return result.Map<Book>(null!);
		}

		/// <summary>
		/// Changes book information.
		/// </summary>
		/// <param name="title">Book title.</param>
		/// <param name="isbn">Book ISBN.</param>
		/// <param name="language">Book language.</param>
		/// <param name="ageRating">Book age rating.</param>
		/// <param name="bookRepository">Book repository.</param>
		/// <param name="description">Book description.</param>
		/// <param name="publisher">Book publisher.</param>
		/// <param name="publishedDate">Book published date.</param>
		/// <param name="cancellationToken">Cancelation token.</param>
		/// <returns>The updated book.</returns>
		public async Task<Result<Book>> UpdateAsync(
			string title,
			string? isbn,
			string language,
			uint ageRating,
			IBookRepository bookRepository,
			string? description = null,
			Publisher? publisher = null,
			DateOnly? publishedDate = null,
			CancellationToken cancellationToken = default)
		{
			var result = Result.Success(this);
			// TODO make setting related entities better
			result
				.Ensure(() => string.IsNullOrWhiteSpace(title) == false, BookErrors.EmptyTitle)
				.Ensure(() => language != null
								&& Regex.IsMatch(language, pattern_ISO169_1_LangCode, RegexOptions.IgnoreCase),
						BookErrors.InvalidLanguageCode(language));

			if (isbn != null && await bookRepository.IsISBNUniqueAsync(isbn, cancellationToken) == false)
			{
				result.Ensure(() => false, BookErrors.ISBNIsNotUnique(isbn));
			}

			if (result.IsSuccess)
			{
				var _description = description ?? string.Empty;

				bool bookInfoChanged = Title != title
									|| Description != _description
									|| ISBN != isbn
									|| AgeRating != ageRating
									|| Language != language
									|| PublishedDate != publishedDate
									|| Publisher != publisher;

				if (bookInfoChanged)
				{
					Title = title;
					Description = _description;
					ISBN = isbn;
					AgeRating = ageRating;
					Language = language;
					PublishedDate = publishedDate;
					Publisher = publisher;

					RaiseDomainEvent(new BookUpdatedDomainEvent(
									Guid.NewGuid(),
									DateTime.UtcNow,
									Id,
									Title,
									Description,
									ISBN,
									Language,
									AgeRating,
									Authors,
									Categories,
									Publisher,
									PublishedDate,
									Sources));
				}
			}

			return result;
		}
	}
}
