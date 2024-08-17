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
using Service.Orders.Domain;
using Service.Orders.Domain.Orders;
using Service.Orders.IntegrationEvents;
using Service.Shipments.IntegrationEvents;

namespace Service.Orders.Application.Orders.Events
{
	/// <summary>
	/// Represents the <see cref="ShipmentProcessedIntegrationEvent"/> handler.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="orderRepository">The order repository.</param>
	/// <param name="db">The database.</param>
	internal sealed class ShipmentProcessedIntegrationEventHandler(
		ILogger<ShipmentProcessedIntegrationEventHandler> logger,
		IOrderRepository orderRepository,
		IOrderDb db)
		: IntegrationEventHandler<ShipmentProcessedIntegrationEvent>
	{
		/// <inheritdoc/>
		public override async Task Handle(ShipmentProcessedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
		{
			var order = GetOrder(integrationEvent);

			if (order == null)
			{
				return;
			}

			order.Shipment!.Update(ShipmentStatus.FromName(integrationEvent.StatusName)!);

			if (integrationEvent.StatusName == ShipmentStatus.Shipped.Name)
			{
				order.UpdateStatus(OrderStatus.Completed);
			}

			orderRepository.Update(order);
			await db.SaveChangesAsync(cancellationToken);
		}

		private Order? GetOrder(ShipmentProcessedIntegrationEvent integrationEvent)
		{
			var order = orderRepository.GetAll()
					.Include(o => o.Shipment)
					.FirstOrDefault(i => i.Id == new OrderId(integrationEvent.OrderId));

			if (order == null)
				logger.LogError("Shipment processed integration event received for unknown order id {orderId} - {@event}",
									 integrationEvent.OrderId,
									 integrationEvent);

			return order;
		}
	}
}
