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

using Service.Analytics.Domain.OrderItems;

namespace Service.Analytics.Domain.Orders
{
	/// <summary>
	/// Represents the Order entity.
	/// </summary>
	public sealed class Order : Entity<OrderId>, IAuditable
	{
		private List<OrderItem> items = [];
		private decimal? totalPrice = null;

		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the customer identifier.
		/// </summary>
		public CustomerId CustomerId { get; private set; }

		/// <summary>
		/// Gets the ordered date and time.
		/// </summary>
		public DateTime OrderedDateTimeUtc { get; private set; }

		/// <summary>
		/// Gets the shipment address if available.
		/// </summary>
		public Address? Address { get; private set; }

		/// <summary>
		/// Gets the ordered items collection.
		/// </summary>
		public IReadOnlyCollection<OrderItem> Items => items;

		/// <summary>
		/// Gets total price of the order.
		/// </summary>
		public decimal TotalPrice => totalPrice ??= items.Sum(i => i.UnitPrice * i.Quantity);

		/// <summary>
		/// Initializes a new instance of the <see cref="Order"/> class.
		/// </summary>
		/// <param name="id">The order identifier.</param>
		/// <param name="isDeleted">The order deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private Order(OrderId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Order"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Order()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="Order"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="id">The order identifier.</param>
		/// <param name="customerId">The customer identifier.</param>
		/// <param name="items">The ordering items.</param>
		/// <param name="orderedDateTime">The ordered date time.</param>
		/// <param name="address">The delivery address of the order.</param>
		/// <returns>The new <see cref="Order"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Order> Create(
			OrderId id,
			CustomerId customerId,
			IEnumerable<OrderItem> items,
			DateTime orderedDateTime,
			Address? address = null)
			=> Result.Success(new Order(id, false)
			{
				CustomerId = customerId,
				OrderedDateTimeUtc = orderedDateTime,
				items = items?.ToList()!,
				Address = address,
			})
				.Ensure(o => o.items?.Count > 0, OrderErrors.EmptyOrderItems());
	}
}
