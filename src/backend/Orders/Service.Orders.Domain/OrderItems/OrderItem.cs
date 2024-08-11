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
using System.Text.RegularExpressions;

namespace Service.Orders.Domain.OrderItems
{
	/// <summary>
	/// Represents the Order Item entity.
	/// </summary>
	public sealed class OrderItem : Entity<OrderItemId>, IAuditable
	{
		private const string pattern_ISO169_1_LangCode = "^[a-z]{2}$";
		private const string isbnPattern = @"^(?:\d[\ |-]?){9}[\d|X]$|^(?:97[89][\ |-]?\d{1,5}[\ |-]?\d{1,7}[\ |-]?\d{1,7}[\ |-]?\d)$";

		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the book identifier.
		/// </summary>
		public BookId BookId { get; private set; }

		/// <summary>
		/// Gets the book title.
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// Gets the book ISBN if available.
		/// </summary>
		public string? ISBN { get; private set; }

		/// <summary>
		/// Gets the book cover image if available.
		/// </summary>
		public string? Cover { get; private set; }

		/// <summary>
		/// Gets the book language.
		/// </summary>
		public string Language { get; private set; }

		/// <summary>
		/// Gets the book source identifier.
		/// </summary>
		public BookSourceId SourceId { get; private set; }

		/// <summary>
		/// Gets the book source format.
		/// </summary>
		public BookFormat Format { get; private set; }

		/// <summary>
		/// Gets the one unit price.
		/// </summary>
		public decimal UnitPrice { get; private set; }

		/// <summary>
		/// Gets the ordering quantity.
		/// </summary>
		public uint Quantity { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderItem"/> class.
		/// </summary>
		/// <param name="id">The order item identifier.</param>
		/// <param name="isDeleted">The order item deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private OrderItem(OrderItemId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OrderItem"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private OrderItem()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="OrderItem"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="bookId">The ordering book identifier.</param>
		/// <param name="title">The ordering book title.</param>
		/// <param name="language">The ordering book language.</param>
		/// <param name="sourceId">The ordering book source identifier.</param>
		/// <param name="format">The ordering book source format.</param>
		/// <param name="unitPrice">The ordering price.</param>
		/// <param name="quantity">The ordering quantity.</param>
		/// <param name="isbn">The ordering book ISBN.</param>
		/// <param name="cover">The ordering book cover image source.</param>
		/// <returns>The new <see cref="OrderItem"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<OrderItem> Create(
			BookId bookId,
			string title,
			string language,
			BookSourceId sourceId,
			BookFormat format,
			decimal unitPrice,
			uint quantity,
			string? isbn = null,
			string? cover = null)
			=> Result.Success(new OrderItem(new OrderItemId(Guid.NewGuid()), false)
			{
				BookId = bookId,
				Title = title,
				Language = language,
				SourceId = sourceId,
				Format = format,
				UnitPrice = unitPrice,
				Quantity = quantity,
				ISBN = isbn,
				Cover = cover,
			})
				.Ensure(oi => string.IsNullOrWhiteSpace(oi.Title) == false, OrderItemErrors.EmptyTitle())
				.Ensure(oi => oi.Language != null
								&& Regex.IsMatch(language, pattern_ISO169_1_LangCode, RegexOptions.IgnoreCase),
								OrderItemErrors.InvalidLanguageCode(language))
				.Ensure(oi => oi.ISBN == null || Regex.IsMatch(isbn, isbnPattern),
								OrderItemErrors.InvalidISBN(isbn))
				.Ensure(oi => oi.Quantity > 0, OrderItemErrors.InvalidQuantity(quantity));
	}
}
