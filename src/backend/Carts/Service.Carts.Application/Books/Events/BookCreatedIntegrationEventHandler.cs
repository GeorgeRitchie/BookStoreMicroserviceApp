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
using Service.Catalog.IntegrationEvents;

namespace Service.Carts.Application.Books.Events
{
	/// <summary>
	/// Represents the <see cref="BookCreatedIntegrationEvent"/> handler.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="repository">The book repository.</param>
	/// <param name="db">The database.</param>
	internal sealed class BookCreatedIntegrationEventHandler(
		ILogger<BookCreatedIntegrationEventHandler> logger,
		IBookRepository repository,
		ICartDb db)
		: IntegrationEventHandler<BookCreatedIntegrationEvent>
	{
		/// <inheritdoc/>
		public override async Task Handle(BookCreatedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
		{
			var bookId = new BookId(integrationEvent.BookId);
			if (await repository.GetAll().AnyAsync(i => i.Id == bookId, cancellationToken))
				return;

			await Book.Create(bookId,
								integrationEvent.Title,
								integrationEvent.ISBN,
								integrationEvent.Language,
								null, // TODO add new event or update current to include book image
								integrationEvent.Description)
						.Tap(result =>
						{
							if (result.IsFailure)
								logger.LogError("Book create failed in {eventName} event with errors: {@errors}.",
									nameof(BookCreatedIntegrationEvent),
									result.Errors);
						})
						.Tap<Book>(book => repository.Create(book))
						.Tap(() => db.SaveChangesAsync(cancellationToken));
		}
	}
}
