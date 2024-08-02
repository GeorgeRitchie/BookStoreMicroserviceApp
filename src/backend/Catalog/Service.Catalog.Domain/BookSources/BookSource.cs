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

using Service.Catalog.Domain.Books;
using Service.Catalog.Domain.BookSources.Events;

namespace Service.Catalog.Domain.BookSources
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
		/// Gets books quantity in storage. (Used only for paper format book sources).
		/// </summary>
		public uint? StockQuantity { get; private set; }

		/// <summary>
		/// Gets book price.
		/// </summary>
		public decimal Price { get; private set; }

		/// <summary>
		/// Gets book source url. For paper format book source url is <see langword="null"/>.
		/// </summary>
		public string? Url { get; private set; }

		/// <summary>
		/// Gets book's preview source url of if available.
		/// </summary>
		public string? PreviewUrl { get; private set; }

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
		/// <param name="book">The book entity this source belongs to.</param>
		/// <param name="format">The book source format.</param>
		/// <param name="url">The book source url.</param>
		/// <param name="quantity">The paper formatted book source quantity.</param>
		/// <param name="price">The book source price.</param>
		/// <param name="previewUrl">The book preview source url.</param>
		/// <returns>The new <see cref="BookSource"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<BookSource> Create(
			Book book,
			BookFormat format,
			string? url,
			uint quantity,
			decimal price,
			string? previewUrl = null)
			=> Result.Success(
					new BookSource(new BookSourceId(Guid.NewGuid()), false)
					{
						Book = book,
						BookId = book?.Id!,
						Price = price,
						StockQuantity = quantity,
						Format = format,
						Url = url,
						PreviewUrl = previewUrl
					})
				.Ensure(s => s.Format == BookFormat.Paper
							|| s.Format != BookFormat.Paper && IsValidUrl(s.Url), BookSourceErrors.InvalidSourceUrl)
				.Ensure(s => s.PreviewUrl is null
							|| s.PreviewUrl is not null && IsValidUrl(s.PreviewUrl), BookSourceErrors.InvalidPreviewUrl)
				.Ensure(s => s.Book is not null, BookSourceErrors.BookIsRequired)
				.Tap(s =>
				{
					if (s.Format == BookFormat.Paper)
						s.Url = null;
					else
						s.StockQuantity = null;
				})
				.Tap(s => s.RaiseDomainEvent(new BookSourceCreatedDomainEvent(
											Guid.NewGuid(),
											DateTime.UtcNow,
											s.Id,
											s.Format.Name,
											s.StockQuantity,
											s.Price,
											s.Url,
											s.PreviewUrl,
											s.BookId)));

		/// <summary>
		/// Changes the book source information.
		/// </summary>
		/// <param name="url">The source url.</param>
		/// <param name="quantity">The book quantity.</param>
		/// <param name="price">The book price.</param>
		/// <param name="previewUrl">The book preview url.</param>
		/// <returns>The updated book source.</returns>
		public Result<BookSource> Update(
			string? url,
			uint quantity,
			decimal price,
			string? previewUrl = null)
			=> Result.Success(this)
				.Ensure(s => s.Format == BookFormat.Paper
							|| s.Format != BookFormat.Paper && IsValidUrl(url), BookSourceErrors.InvalidSourceUrl)
				.Ensure(s => previewUrl is null
							|| previewUrl is not null && IsValidUrl(previewUrl), BookSourceErrors.InvalidPreviewUrl)
				.Tap(s =>
				{
					uint? _quantity = s.Format == BookFormat.Paper ? quantity : null;
					string? _url = s.Format != BookFormat.Paper ? url : null;
					bool infoChanged = s.StockQuantity != _quantity
										|| s.Price != price
										|| s.PreviewUrl != previewUrl
										|| s.Url != _url;

					if (infoChanged)
					{
						s.Url = _url;
						s.StockQuantity = _quantity;
						s.PreviewUrl = previewUrl;
						s.Price = price;

						s.RaiseDomainEvent(new BookSourceUpdatedDomainEvent(
											Guid.NewGuid(),
											DateTime.UtcNow,
											s.Id,
											s.StockQuantity,
											s.Price,
											s.Url,
											s.PreviewUrl));
					}
				});

		/// <inheritdoc/>
		public override void MarkAsDeleted()
		{
			base.MarkAsDeleted();

			RaiseDomainEvent(new BookSourceDeletedDomainEvent(
							Guid.NewGuid(),
							DateTime.UtcNow,
							Id));
		}

		private static bool IsValidUrl(string? url)
			=> Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? resultUri)
				&& (resultUri.IsAbsoluteUri == false
					|| resultUri.IsAbsoluteUri == true
						&& (resultUri.Scheme == Uri.UriSchemeHttp || resultUri.Scheme == Uri.UriSchemeHttps));
	}
}
