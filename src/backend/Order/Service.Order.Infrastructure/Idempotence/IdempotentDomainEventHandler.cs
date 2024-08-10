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

using Application.Messaging;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Persistence.Outbox;
using Service.Order.Domain;
using Shared.Repositories;

namespace Service.Order.Infrastructure.Idempotence
{
	/// <summary>
	/// Represents the idempotent domain event handler, which checks if the domain event has already been handled previously.
	/// </summary>
	/// <typeparam name="TEvent">The domain event type.</typeparam>
	/// <remarks>
	/// Initializes a new instance of the <see cref="IdempotentDomainEventHandler{TEvent}"/> class.
	/// </remarks>
	/// <param name="decorated">The decorated domain event handler.</param>
	/// <param name="repository">The Repository of <see cref="OutboxMessageConsumer"/>.</param>
	/// <param name="db">The database <see cref="IOrderDb"/>.</param>
	internal sealed class IdempotentDomainEventHandler<TEvent>(
		IDomainEventHandler<TEvent> decorated,
		IRepository<OutboxMessageConsumer, Guid> repository,
		IOrderDb db)
		: IDomainEventHandler<TEvent>
		where TEvent : IDomainEvent
	{
		/// <inheritdoc />
		public async Task Handle(TEvent notification, CancellationToken cancellationToken)
		{
			var consumer = new OutboxMessageConsumer(notification.Id, decorated.GetType().FullName!);

			if (await IsOutboxMessageConsumedAsync(consumer, cancellationToken))
			{
				return;
			}

			await decorated.Handle(notification, cancellationToken);

			repository.Create(consumer);
			await db.SaveChangesAsync(cancellationToken);
		}

		private Task<bool> IsOutboxMessageConsumedAsync(OutboxMessageConsumer consumer,
														CancellationToken cancellationToken = default)
		{
			return repository.GetAll().AnyAsync(i => i.Id == consumer.Id && i.Name == consumer.Name, cancellationToken);
		}
	}
}
