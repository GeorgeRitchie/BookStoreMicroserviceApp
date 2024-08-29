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
using Service.Catalog.Domain.BookSources.Events;
using Service.Catalog.IntegrationEvents;

namespace Service.Catalog.Application.BooSources.Commands.CreateBookSource
{
	/// <summary>
	/// Represents the <see cref="BookSourceCreatedDomainEvent"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="BookSourceCreatedDomainEventHandler"/> class.
	/// </remarks>
	/// <param name="eventBus">The event bus.</param>
	internal sealed class BookSourceCreatedDomainEventHandler(IEventBus eventBus)
		: IDomainEventHandler<BookSourceCreatedDomainEvent>
	{
		/// <inheritdoc />
		public async Task Handle(BookSourceCreatedDomainEvent notification, CancellationToken cancellationToken)
			=> await eventBus.PublishAsync(
				new BookSourceCreatedIntegrationEvent(
					notification.Id,
					notification.OccurredOnUtc,
					notification.BookSourceId.Value,
					notification.Format,
					notification.Quantity,
					notification.Price,
					notification.Url,
					notification.PreviewUrl,
					notification.BookId.Value),
				cancellationToken);
	}
}
