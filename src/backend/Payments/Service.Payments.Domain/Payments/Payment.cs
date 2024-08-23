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

using Service.Payments.Domain.PurchaseItems;
using Service.Payments.IntegrationEvents;

namespace Service.Payments.Domain.Payments
{
	/// <summary>
	/// Represents the Payment entity.
	/// </summary>
	public sealed class Payment : Entity<PaymentId>, IAuditable
	{
		private List<PurchaseItem> items = [];
		private decimal? totalPrice = null;

		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the order identifier.
		/// </summary>
		public OrderId OrderId { get; private set; }

		/// <summary>
		/// Gets the customer identifier.
		/// </summary>
		public CustomerId CustomerId { get; private set; }

		/// <summary>
		/// Gets the payment status.
		/// </summary>
		public PaymentStatus Status { get; set; }

		/// <summary>
		/// Gets the ordered date and time.
		/// </summary>
		public DateTime OrderedDateTimeUtc { get; private set; }

		/// <summary>
		/// Gets the user interaction url to complete the payment.
		/// </summary>
		public string InteractionUrl { get; set; }

		/// <summary>
		/// Gets the purchase items collection.
		/// </summary>
		public IReadOnlyCollection<PurchaseItem> Items => items;

		/// <summary>
		/// Gets total price of the payment.
		/// </summary>
		public decimal TotalPrice => totalPrice ??= items.Sum(i => i.UnitPrice * i.Quantity);

		/// <summary>
		/// Initializes a new instance of the <see cref="Payment"/> class.
		/// </summary>
		/// <param name="id">The payment identifier.</param>
		/// <param name="isDeleted">The payment deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private Payment(PaymentId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Payment"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Payment()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="Payment"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="orderId">Order identifier.</param>
		/// <param name="customerId">Customer identifier.</param>
		/// <param name="paymentStatus">Payment status.</param>
		/// <param name="orderedDateTime">Ordered date time.</param>
		/// <param name="purchaseItems">Purchased items.</param>
		/// <param name="interactionUrl">Payment interaction url.</param>
		/// <returns>The new <see cref="Payment"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Payment> Create(
			OrderId orderId,
			CustomerId customerId,
			PaymentStatus paymentStatus,
			DateTime orderedDateTime,
			IEnumerable<PurchaseItem> purchaseItems,
			string? interactionUrl = null)
			=> Result.Success(new Payment(new PaymentId(Guid.NewGuid()), false)
			{
				OrderId = orderId,
				CustomerId = customerId,
				Status = paymentStatus,
				OrderedDateTimeUtc = orderedDateTime,
				items = purchaseItems.ToList(),
				InteractionUrl = interactionUrl ?? "",
			});
	}
}
