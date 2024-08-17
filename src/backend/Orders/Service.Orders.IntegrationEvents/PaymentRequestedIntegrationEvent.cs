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

using Application.EventBus;

namespace Service.Orders.IntegrationEvents
{
	/// <summary>
	/// Represents the integration event to initiate purchase.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="OrderId">The order identifier.</param>
	/// <param name="CustomerId">The customer identifier.</param>
	/// <param name="OrderedDateTimeUtc">The ordered date and time.</param>
	/// <param name="Items">Ordered items.</param>
	public sealed record PaymentRequestedIntegrationEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		Guid OrderId,
		Guid CustomerId,
		DateTime OrderedDateTimeUtc,
		List<OrderedItem> Items) : IntegrationEvent(Id, OccurredOnUtc);

	/// <summary>
	/// Represents ordered item to purchase.
	/// </summary>
	/// <param name="Id">Ordered item identifier.</param>
	/// <param name="BookId">Book identifier.</param>
	/// <param name="Title">Book title.</param>
	/// <param name="ISBN">Book ISBN.</param>
	/// <param name="Cover">Book cover image source.</param>
	/// <param name="Language">Book language.</param>
	/// <param name="SourceId">Book source identifier.</param>
	/// <param name="FormatName">Book format name.</param>
	/// <param name="UnitPrice">Book price.</param>
	/// <param name="Quantity">Purchasing quantity.</param>
	public sealed record OrderedItem(
		Guid Id,
		Guid BookId,
		string Title,
		string? ISBN,
		string? Cover,
		string Language,
		Guid SourceId,
		string FormatName,
		decimal UnitPrice,
		uint Quantity);
}
