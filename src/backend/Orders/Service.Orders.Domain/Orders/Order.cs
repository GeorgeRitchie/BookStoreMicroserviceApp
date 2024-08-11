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

using Service.Orders.Domain.OrderItems;
using Service.Orders.Domain.Orders.Events;
using Service.Orders.Domain.Payments;
using Service.Orders.Domain.Shipments;
using Service.Orders.IntegrationEvents;

namespace Service.Orders.Domain.Orders
{
	/// <summary>
	/// Represents the Order entity.
	/// </summary>
	public sealed class Order : Entity<OrderId>, IAuditable
	{
		private List<OrderItem> items = [];

		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the customer identifier.
		/// </summary>
		public CustomerId CustomerId { get; private set; }

		/// <summary>
		/// Gets the order status.
		/// </summary>
		public OrderStatus Status { get; private set; }

		/// <summary>
		/// Gets the ordered date and time.
		/// </summary>
		public DateTime OrderedDateTimeUtc { get; private set; }

		/// <summary>
		/// Gets or sets the order payment information if available.
		/// </summary>
		public Payment? Payment { get; set; }

		/// <summary>
		/// Gets or sets the order shipment information if available.
		/// </summary>
		public Shipment? Shipment { get; set; }

		/// <summary>
		/// Gets the ordered items collection.
		/// </summary>
		public IReadOnlyCollection<OrderItem> Items => items;

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
		/// <param name="customerId">The customer identifier.</param>
		/// <param name="items">The ordering items.</param>
		/// <returns>The new <see cref="Order"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Order> Create(CustomerId customerId, IEnumerable<OrderItem> items)
			=> Result.Success(new Order(new OrderId(Guid.NewGuid()), false)
			{
				CustomerId = customerId,
				Status = OrderStatus.Pending,
				OrderedDateTimeUtc = DateTime.UtcNow,
				items = items?.ToList()!,
			})
				.Ensure(o => o.items?.Count > 0, OrderErrors.EmptyOrderItems())
				.Tap(o => o.RaiseDomainEvent(new OrderCreatedDomainEvent(Guid.NewGuid(),
												DateTime.UtcNow,
												o.CustomerId,
												o.Status,
												o.OrderedDateTimeUtc,
												o.Shipment?.Address,
												o.items.ToList())));

		/// <summary>
		/// Updates order status.
		/// </summary>
		/// <param name="status">The new status.</param>
		/// <returns>The updated order.</returns>
		public Result<Order> UpdateStatus(OrderStatus status)
		{
			if (Status != status)
			{
				Status = status;
				RaiseDomainEvent(new OrderStatusUpdatedDomainEvent(Guid.NewGuid(),
									DateTime.UtcNow,
									CustomerId,
									Status,
									OrderedDateTimeUtc,
									Shipment?.Address,
									items.ToList()));
			}

			return Result.Success(this);
		}
	}
}
