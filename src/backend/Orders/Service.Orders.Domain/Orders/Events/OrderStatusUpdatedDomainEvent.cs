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
using Service.Orders.Domain.Shipments;
using Service.Orders.IntegrationEvents;

namespace Service.Orders.Domain.Orders.Events
{
	/// <summary>
	/// Represents the domain event that is raised when an Order status is updated.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="OrderId">The order identifier.</param>
	/// <param name="CustomerId">The customer identifier.</param>
	/// <param name="Status">The order status.</param>
	/// <param name="OrderedDateTimeUtc">The ordered date and time.</param>
	/// <param name="Address">The address.</param>
	/// <param name="Items">The ordered items.</param>
	public sealed record OrderStatusUpdatedDomainEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		Guid OrderId,
		CustomerId CustomerId,
		OrderStatus Status,
		DateTime OrderedDateTimeUtc,
		Address? Address,
		List<OrderItem> Items)
		: DomainEvent(Id, OccurredOnUtc);
}
