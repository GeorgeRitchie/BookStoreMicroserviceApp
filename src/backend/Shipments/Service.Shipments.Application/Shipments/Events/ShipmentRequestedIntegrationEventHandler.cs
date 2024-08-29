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
using Service.Catalog.IntegrationEvents;
using Service.Orders.IntegrationEvents;
using Service.Shipments.Domain;
using Service.Shipments.Domain.BookSources;
using Service.Shipments.Domain.ShipmentItems;
using Service.Shipments.Domain.Shipments;
using Service.Shipments.IntegrationEvents;

namespace Service.Shipments.Application.Shipments.Events
{
	/// <summary>
	/// Represents the <see cref="ShipmentRequestedIntegrationEvent"/> handler.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="bookSourceRepository">The book source repository.</param>
	/// <param name="shipmentRepository">The shipment repository.</param>
	/// <param name="db">The database.</param>
	/// <param name="eventBus">The event bus.</param>
	internal sealed class ShipmentRequestedIntegrationEventHandler(
		ILogger<ShipmentRequestedIntegrationEventHandler> logger,
		IBookSourceRepository bookSourceRepository,
		IShipmentRepository shipmentRepository,
		IShipmentDb db,
		IEventBus eventBus)
		: IntegrationEventHandler<ShipmentRequestedIntegrationEvent>
	{
		/// <inheritdoc/>
		public override async Task Handle(ShipmentRequestedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
		{
			var shipment = new Shipment(new ShipmentId(Guid.NewGuid()), false)
			{
				OrderedDateTimeUtc = integrationEvent.OrderedDateTimeUtc,
				CustomerId = new CustomerId(integrationEvent.CustomerId),
				OrderId = new OrderId(integrationEvent.OrderId),
				Address = integrationEvent.Address == null ? null : new Address
				{
					Country = integrationEvent.Address.Country,
					Region = integrationEvent.Address.Region,
					District = integrationEvent.Address.District,
					City = integrationEvent.Address.City,
					Street = integrationEvent.Address.Street,
					Home = integrationEvent.Address.Home,
				},
				Status = ShipmentStatus.Pending,
			};

			bool allEBooks = true;

			foreach (var item in integrationEvent.Items)
			{
				var bs = await bookSourceRepository.GetAll().FirstOrDefaultAsync(i => i.Id == new BookSourceId(item.SourceId), cancellationToken);

				if (bs == null)
				{
					logger.LogError("{eventName} requested for unknown book source with id: {id}, event data {@eventData}",
						nameof(ShipmentRequestedIntegrationEvent),
						item.SourceId,
						integrationEvent);

					return;
				}

				if (bs.Format == BookFormat.Paper)
					allEBooks = false;

				shipment.Items.Add(new ShipmentItem(new ShipmentItemId(Guid.NewGuid()), false)
				{
					Shipment = shipment,
					Quantity = item.Quantity,
					BookSource = bs,
				});
			}

			shipmentRepository.Create(shipment);
			await db.SaveChangesAsync(cancellationToken);

			if (allEBooks)
			{
				shipment.Status = ShipmentStatus.Shipped;

				shipmentRepository.Update(shipment);
				await db.SaveChangesAsync(cancellationToken);

				await eventBus.PublishAsync(new ShipmentProcessedIntegrationEvent(
												Guid.NewGuid(),
												DateTime.UtcNow,
												shipment.OrderId.Value,
												shipment.Status.Name,
												null),
											cancellationToken);
			}
		}
	}
}
