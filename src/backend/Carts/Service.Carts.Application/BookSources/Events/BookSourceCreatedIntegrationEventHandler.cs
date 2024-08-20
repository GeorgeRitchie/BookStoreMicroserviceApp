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
using Service.Carts.Domain;
using Service.Carts.Domain.Books;
using Service.Carts.Domain.BookSources;
using Service.Catalog.IntegrationEvents;

namespace Service.Carts.Application.BookSources.Events
{
	/// <summary>
	/// Represents the <see cref="BookSourceCreatedIntegrationEvent"/> handler.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="bookSourceRepository">The book source repository.</param>
	/// <param name="bookRepository">The book repository.</param>
	/// <param name="db">The database.</param>
	internal sealed class BookSourceCreatedIntegrationEventHandler(
		ILogger<BookSourceCreatedIntegrationEventHandler> logger,
		IBookSourceRepository bookSourceRepository,
		IBookRepository bookRepository,
		ICartDb db)
		: IntegrationEventHandler<BookSourceCreatedIntegrationEvent>
	{
		/// <inheritdoc/>
		public override async Task Handle(BookSourceCreatedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
		{
			var bookSourceId = new BookSourceId(integrationEvent.BookSourceId);
			if (await bookSourceRepository.GetAll().AnyAsync(i => i.Id == bookSourceId, cancellationToken))
				return;

			await Result.Create(await bookRepository.GetAll()
						.FirstOrDefaultAsync(i => i.Id == new BookId(integrationEvent.BookId), cancellationToken))
				.Tap(result =>
				{
					if (result.IsFailure)
						logger.LogError("Received book source create event for not existing book entity with id {bookId}.",
							integrationEvent.BookId);
				})
				.Bind(book => BookSource.Create(
					bookSourceId,
					book,
					BookFormat.FromName(integrationEvent.FormatName)!,
					integrationEvent.Price))
				.Tap(result =>
				{
					if (result.IsFailure && result.Value is not null)
						logger.LogError("Book source create failed in {eventName} event with errors: {@errors}.",
							nameof(BookSourceCreatedIntegrationEvent),
							result.Errors);
				})
				.Tap<BookSource>(bookSource => bookSourceRepository.Create(bookSource))
				.Tap(() => db.SaveChangesAsync(cancellationToken));
		}
	}
}
