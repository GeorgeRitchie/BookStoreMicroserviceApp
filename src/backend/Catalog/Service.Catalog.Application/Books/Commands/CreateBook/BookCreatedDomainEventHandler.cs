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
using Service.Catalog.Domain.Books.Events;
using Service.Catalog.IntegrationEvents;

namespace Service.Catalog.Application.Books.Commands.CreateBook
{
	/// <summary>
	/// Represents the <see cref="BookCreatedDomainEvent"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="BookCreatedDomainEventHandler"/> class.
	/// </remarks>
	/// <param name="eventBus">The event bus.</param>
	internal sealed class BookCreatedDomainEventHandler(IEventBus eventBus)
		: IDomainEventHandler<BookCreatedDomainEvent>
	{
		/// <inheritdoc />
		public async Task Handle(BookCreatedDomainEvent notification, CancellationToken cancellationToken)
			=> await eventBus.PublishAsync(
				new BookCreatedIntegrationEvent(
					notification.Id,
					notification.OccurredOnUtc,
					notification.BookId.Value,
					notification.Title,
					notification.Description,
					notification.ISBN,
					notification.Language,
					notification.AgeRating,
					notification.Authors.Select(i => i.Id.Value),
					notification.Categories.Select(i => i.Id.Value),
					notification.PublisherId?.Value,
					notification.PublishedDate),
				cancellationToken);
	}
}
