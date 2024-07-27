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
using Service.CatalogWrite.Domain.BookSources;
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
		private const string isbnPattern = @"^(?:\d[\ |-]?){9}[\d|X]$|^(?:97[89][\ |-]?\d{1,5}[\ |-]?\d{1,7}[\ |-]?\d{1,7}[\ |-]?\d)$";

		private List<Author> authors = [];
		private List<Category> categories = [];
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
		/// Gets the book age rating.
		/// </summary>
		public uint AgeRating { get; private set; }

		/// <summary>
		/// Gets the book publisher identifier if available.
		/// </summary>
		public PublisherId? PublisherId { get; private set; }

		/// <summary>
		/// Gets the book publisher information if available.
		/// </summary>
		public Publisher? Publisher { get; private set; }

		/// <summary>
		/// Gets the book published date if available.
		/// </summary>
		public DateOnly? PublishedDate { get; private set; }

		/// <summary>
		/// Gets the book images.
		/// </summary>
		public List<ImageSource<BookImageType>> Images { get; private set; } = [];

		/// <summary>
		/// Gets the book authors information.
		/// </summary>
		public IReadOnlyCollection<Author> Authors => authors;

		/// <summary>
		/// Gets the book categories.
		/// </summary>
		public IReadOnlyCollection<Category> Categories => categories;

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
		/// <param name="title">Book title.</param>
		/// <param name="isbn">Book ISBN.</param>
		/// <param name="language">Book language.</param>
		/// <param name="ageRating">Book age rating.</param>
		/// <param name="authors">Book authors.</param>
		/// <param name="categories">Book categories.</param>
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
			IBookRepository bookRepository,
			string? description = null,
			Publisher? publisher = null,
			DateOnly? publishedDate = null,
			CancellationToken cancellationToken = default)
		{
			var result = Result.Success();

			result
				.Ensure(() => string.IsNullOrWhiteSpace(title) == false, BookErrors.EmptyTitle)
				.Ensure(() => language != null
								&& Regex.IsMatch(language, pattern_ISO169_1_LangCode, RegexOptions.IgnoreCase),
						BookErrors.InvalidLanguageCode(language))
				.Ensure(() => authors?.Any() == true, BookErrors.AuthorIsRequired)
				.Ensure(() => categories?.Any() == true, BookErrors.CategoryIsRequired);

			if (isbn != null)
			{
				result.Ensure(() => Regex.IsMatch(isbn, isbnPattern), BookErrors.InvalidISBN(isbn));

				if (result.IsSuccess && await bookRepository.IsISBNUniqueAsync(isbn, cancellationToken) == false)
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
					authors = authors.ToList(),
					categories = categories.ToList(),
					PublisherId = publisher?.Id,
					Publisher = publisher,
					PublishedDate = publishedDate,
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
								book.PublisherId,
								book.PublishedDate));

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

			result
				.Ensure(() => string.IsNullOrWhiteSpace(title) == false, BookErrors.EmptyTitle)
				.Ensure(() => language != null
								&& Regex.IsMatch(language, pattern_ISO169_1_LangCode, RegexOptions.IgnoreCase),
						BookErrors.InvalidLanguageCode(language));

			if (isbn != null && isbn != ISBN && await bookRepository.IsISBNUniqueAsync(isbn, cancellationToken) == false)
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
									|| PublisherId != publisher?.Id;

				if (bookInfoChanged)
				{
					Title = title;
					Description = _description;
					ISBN = isbn;
					AgeRating = ageRating;
					Language = language;
					PublishedDate = publishedDate;
					PublisherId = publisher?.Id;
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
									PublisherId,
									PublishedDate));
				}
			}

			return result;
		}

		/// <summary>
		/// Adds author to current book's authors list.
		/// </summary>
		/// <param name="author">The author to add.</param>
		/// <returns>The updated book.</returns>
		public Result<Book> AddAuthor(Author author)
		{
			if (authors.Any(a => a.Id == author.Id) == false)
			{
				authors.Add(author);
				RaiseDomainEvent(new AuthorAddedToBookDomainEvent(
									Guid.NewGuid(),
									DateTime.UtcNow,
									Id,
									author.Id));
			}

			return Result.Success(this);
		}

		/// <summary>
		/// Removes author from current book's authors list.
		/// </summary>
		/// <param name="authorId">The author to remove.</param>
		/// <returns>The updated book, or failure result if author not found or there is only one in list.</returns>
		public Result<Book> RemoveAuthor(AuthorId authorId)
			=> Result.Create(authors.FirstOrDefault(i => i.Id == authorId))
				.MapFailure(() => BookErrors.BookDoesNotHaveAuthor(Id, authorId))
				.EnsureOnSuccess(author => authors.Count > 1, BookErrors.AuthorIsRequired)
				.Tap<Author>(author => authors.Remove(author))
				.Tap(() => RaiseDomainEvent(new AuthorRemovedFromBookDomainEvent(
											Guid.NewGuid(),
											DateTime.UtcNow,
											Id,
											authorId)))
				.Map(() => this);

		/// <summary>
		/// Adds category to current book's categories list.
		/// </summary>
		/// <param name="category">The category to add.</param>
		/// <returns>The updated book.</returns>
		public Result<Book> AddCategory(Category category)
		{
			if (categories.Any(a => a.Id == category.Id) == false)
			{
				categories.Add(category);
				RaiseDomainEvent(new CategoryAddedToBookDomainEvent(
									Guid.NewGuid(),
									DateTime.UtcNow,
									Id,
									category.Id));
			}

			return Result.Success(this);
		}

		/// <summary>
		/// Removes category from current book's categories list.
		/// </summary>
		/// <param name="categoryId">The category to remove.</param>
		/// <returns>The updated book, or failure result if category not found or there is only one in list.</returns>
		public Result<Book> RemoveCategory(CategoryId categoryId)
			=> Result.Create(categories.FirstOrDefault(i => i.Id == categoryId))
				.MapFailure(() => BookErrors.BookDoesNotHaveCategory(Id, categoryId))
				.EnsureOnSuccess(category => categories.Count > 1, BookErrors.CategoryIsRequired)
				.Tap<Category>(category => categories.Remove(category))
				.Tap(() => RaiseDomainEvent(new CategoryRemovedFromBookDomainEvent(
											Guid.NewGuid(),
											DateTime.UtcNow,
											Id,
											categoryId)))
				.Map(() => this);
	}
}
