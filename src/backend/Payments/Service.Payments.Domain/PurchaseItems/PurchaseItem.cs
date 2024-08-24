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
	along with this program. If not, see <http://www.gnu.org/licenses/>.
*/

using Service.Catalog.IntegrationEvents;

namespace Service.Payments.Domain.PurchaseItems
{
	/// <summary>
	/// Represents the Purchase Item entity.
	/// </summary>
	public sealed class PurchaseItem : Entity<PurchaseItemId>, IAuditable
	{
		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the order item identifier.
		/// </summary>
		public OrderItemId OrderItemId { get; private set; }

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
		/// Gets the purchasing quantity.
		/// </summary>
		public uint Quantity { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PurchaseItem"/> class.
		/// </summary>
		/// <param name="id">The purchase item identifier.</param>
		/// <param name="isDeleted">The order purchase deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		public PurchaseItem(PurchaseItemId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PurchaseItem"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private PurchaseItem()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="PurchaseItem"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="orderItemId">Order item identifier.</param>
		/// <param name="bookId">Book identifier.</param>
		/// <param name="bookSourceId">Book source identifier.</param>
		/// <param name="title">Book title.</param>
		/// <param name="cover">Book cover image source.</param>
		/// <param name="isbn">Book ISBN.</param>
		/// <param name="language">Book language.</param>
		/// <param name="bookFormat">Book source format.</param>
		/// <param name="price">Book price.</param>
		/// <param name="quantity">Purchasing quantity.</param>
		/// <returns>The new <see cref="PurchaseItem"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<PurchaseItem> Create(
			OrderItemId orderItemId,
			BookId bookId,
			BookSourceId bookSourceId,
			string title,
			string? cover,
			string? isbn,
			string language,
			BookFormat bookFormat,
			decimal price,
			uint quantity)
			=> Result.Success(new PurchaseItem(new PurchaseItemId(Guid.NewGuid()), false)
			{
				BookId = bookId,
				Title = title,
				Cover = cover,
				Language = language,
				Format = bookFormat,
				ISBN = isbn,
				OrderItemId = orderItemId,
				SourceId = bookSourceId,
				Quantity = quantity,
				UnitPrice = price,
			});
	}
}
