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
using Microsoft.EntityFrameworkCore;
using Persistence.Inbox;
using Service.Catalog.Domain;
using Shared.Repositories;

namespace Service.Catalog.Infrastructure.Idempotence
{
	/// <summary>
	/// Represents the idempotent integration event handler, which checks if the integration event has already been handled previously.
	/// </summary>
	/// <typeparam name="TIntegrationEvent">The integration event type.</typeparam>
	/// <remarks>
	/// Initializes a new instance of the <see cref="IdempotentIntegrationEventHandler{TEvent}"/> class.
	/// </remarks>
	/// <param name="decorated">The decorated integration event handler.</param>
	/// <param name="repository">The Repository of <see cref="InboxMessageConsumer"/>.</param>
	/// <param name="db">The database <see cref="ICatalogDb"/>.</param>
	internal sealed class IdempotentIntegrationEventHandler<TIntegrationEvent>(
		IIntegrationEventHandler<TIntegrationEvent> decorated,
		IRepository<InboxMessageConsumer, Guid> repository,
		ICatalogDb db)
		: IntegrationEventHandler<TIntegrationEvent>
		where TIntegrationEvent : IIntegrationEvent
	{
		/// <inheritdoc />
		public override async Task Handle(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
		{
			var consumer = new InboxMessageConsumer(integrationEvent.Id, decorated.GetType().FullName!);

			if (await IsInboxMessageConsumedAsync(consumer, cancellationToken))
			{
				return;
			}

			await decorated.Handle(integrationEvent, cancellationToken);

			repository.Create(consumer);
			await db.SaveChangesAsync(cancellationToken);
		}

		private Task<bool> IsInboxMessageConsumedAsync(InboxMessageConsumer consumer,
														CancellationToken cancellationToken = default)
		{
			return repository.GetAll().AnyAsync(i => i.Id == consumer.Id && i.Name == consumer.Name, cancellationToken);
		}
	}
}
