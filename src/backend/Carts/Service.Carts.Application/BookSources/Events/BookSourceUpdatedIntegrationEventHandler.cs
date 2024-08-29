﻿/* 
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
using Service.Carts.Domain;
using Service.Carts.Domain.BookSources;
using Service.Catalog.IntegrationEvents;

namespace Service.Carts.Application.BookSources.Events
{
	/// <summary>
	/// Represents the <see cref="BookSourceUpdatedIntegrationEvent"/> handler.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="bookSourceRepository">The book source repository.</param>
	/// <param name="db">The database.</param>
	internal sealed class BookSourceUpdatedIntegrationEventHandler(
		ILogger<BookSourceUpdatedIntegrationEventHandler> logger,
		IBookSourceRepository bookSourceRepository,
		ICartDb db)
		: IntegrationEventHandler<BookSourceUpdatedIntegrationEvent>
	{
		/// <inheritdoc/>
		public override async Task Handle(BookSourceUpdatedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
			=> await Result.Create(await bookSourceRepository.GetAll()
						.FirstOrDefaultAsync(i => i.Id == new BookSourceId(integrationEvent.BookSourceId), cancellationToken))
				.Tap(result =>
				{
					if (result.IsFailure)
						logger.LogWarning("Received {eventName} event for not existing book source entity with id {bookSourceId}.",
							nameof(BookSourceUpdatedIntegrationEvent),
							integrationEvent.BookSourceId);
				})
				.Bind(bookSource => bookSource.Update(integrationEvent.Price))
				.Tap(result =>
				{
					if (result.IsFailure && result.Value is not null)
						logger.LogError("Book source updated failed in {eventName} event with errors: {@errors}.",
							nameof(BookSourceUpdatedIntegrationEvent),
							result.Errors);
				})
				.Tap<BookSource>(bookSourceRepository.Update)
				.Tap(() => db.SaveChangesAsync(cancellationToken));
	}
}
