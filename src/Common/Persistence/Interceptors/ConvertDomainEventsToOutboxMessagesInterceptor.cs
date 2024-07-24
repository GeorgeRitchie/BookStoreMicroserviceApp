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

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Domain.Primitives;
using Shared.Extensions;
using Persistence.Outbox;

namespace Persistence.Interceptors
{
	/// <summary>
	/// Represents the interceptor for converting domain events to outbox messages.
	/// </summary>
	public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
	{
		private static readonly JsonSerializerSettings JsonSerializerSettings = new()
		{
			TypeNameHandling = TypeNameHandling.All,
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
		};
		// TODO there are other SavingChanges method overloads, implement them too, because if code calls different SaveChanges method overload, it will call specific SavingChanges overload too
		/// <inheritdoc />
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
			DbContextEventData eventData,
			InterceptionResult<int> result,
			CancellationToken cancellationToken = default)
		{
			if (eventData.Context is null)
			{
				return base.SavingChangesAsync(eventData, result, cancellationToken);
			}

			IEnumerable<OutboxMessage> outboxMessages = CreateOutboxMessages(eventData.Context);

			eventData.Context.Set<OutboxMessage>().AddRange(outboxMessages);

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private static List<OutboxMessage> CreateOutboxMessages(DbContext dbContext) =>
			dbContext
				.ChangeTracker
				.Entries<IEntity>()
				.Select(entityEntry => entityEntry.Entity)
				.SelectMany(entity =>
					entity.GetDomainEvents()
						.Tap(entity.ClearDomainEvents))
				.Select(domainEvent => new OutboxMessage(
					domainEvent.Id,
					domainEvent.OccurredOnUtc,
					domainEvent.GetType().Name,
					JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
				.ToList();
	}
}
