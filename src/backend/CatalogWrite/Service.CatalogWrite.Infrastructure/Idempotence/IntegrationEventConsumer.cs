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
using MassTransit;
using Newtonsoft.Json;
using Persistence.Inbox;
using Shared.Repositories;

namespace Service.CatalogWrite.Infrastructure.Idempotence
{
	/// <summary>
	/// Represents the integration event consumer.
	/// </summary>
	/// <typeparam name="TIntegrationEvent">The integration event type.</typeparam>
	/// <remarks>
	/// Initializes a new instance of the <see cref="IntegrationEventConsumer{TIntegrationEvent}"/> class.
	/// </remarks>
	/// <param name="repository">The Repository of <see cref="InboxMessage"/>.</param>
	/// <param name="db">The database.</param>
	internal sealed class IntegrationEventConsumer<TIntegrationEvent, IDb>(IRepository<InboxMessage> repository, IDb db)
		: IConsumer<TIntegrationEvent>
		where TIntegrationEvent : class, IIntegrationEvent
		where IDb : IDataBase<IDb>
	{
		/// <inheritdoc />
		public async Task Consume(ConsumeContext<TIntegrationEvent> context)
		{
			TIntegrationEvent integrationEvent = context.Message;

			var inboxMessage = new InboxMessage(
				integrationEvent.Id,
				integrationEvent.OccurredOnUtc,
				integrationEvent.GetType().Name,
				JsonConvert.SerializeObject(
					integrationEvent,
					new JsonSerializerSettings
					{
						TypeNameHandling = TypeNameHandling.All
					}));

			repository.Create(inboxMessage);
			await db.SaveChangesAsync();
		}
	}
}
