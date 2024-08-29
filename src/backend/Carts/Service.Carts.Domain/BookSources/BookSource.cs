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

using Service.Carts.Domain.Books;
using Service.Catalog.IntegrationEvents;

namespace Service.Carts.Domain.BookSources
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
		/// Gets book format (e. g. paper, pdf, txt).
		/// </summary>
		public BookFormat Format { get; private set; }

		/// <summary>
		/// Gets book price.
		/// </summary>
		public decimal Price { get; private set; }

		/// <summary>
		/// Gets related book entity's identifier.
		/// </summary>
		public BookId BookId { get; private set; }

		/// <summary>
		/// Gets related book entity.
		/// </summary>
		public Book Book { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BookSource"/> class.
		/// </summary>
		/// <param name="id">The book source identifier.</param>
		/// <param name="isDeleted">The book source deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private BookSource(BookSourceId id, bool isDeleted = false) : base(id, isDeleted)
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

		/// <summary>
		/// Creates a new <see cref="BookSource"/> instance based on the specified parameters by applying validations.
		/// </summary>
		/// <param name="bookSourceId">The book source identifier.</param>
		/// <param name="book">The book entity this source belongs to.</param>
		/// <param name="format">The book source format.</param>
		/// <param name="price">The book source price.</param>
		/// <returns>The new <see cref="BookSource"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<BookSource> Create(
			BookSourceId bookSourceId,
			Book book,
			BookFormat format,
			decimal price)
			=> Result.Success()
				.Ensure(() => format is not null, BookSourceErrors.InvalidFormat())
				.Ensure(() => book is not null, BookSourceErrors.BookIsRequired())
				.Ensure(() => bookSourceId is not null, BookSourceErrors.NullBookSourceId())
				.Map(() => new BookSource(bookSourceId, false)
				{
					Book = book,
					BookId = book?.Id!,
					Price = price,
					Format = format,
				});

		/// <summary>
		/// Changes the book source information.
		/// </summary>
		/// <param name="price">The book price.</param>
		/// <returns>The updated book source.</returns>
		public Result<BookSource> Update(decimal price)
			=> Result.Success(this)
				.Tap(s => s.Price = price);
	}
}
