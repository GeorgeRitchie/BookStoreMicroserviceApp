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
using Service.Analytics.Domain;
using Service.Analytics.Domain.OrderItems;
using Service.Analytics.Domain.Orders;
using Service.Orders.IntegrationEvents;

namespace Service.Analytics.Application.Orders.Events
{
	/// <summary>
	/// Represents the <see cref="OrderCompletedIntegrationEvent"/> handler.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="repository">The order repository.</param>
	/// <param name="db">The database.</param>
	internal sealed class OrderCompletedIntegrationEventHandler(
		ILogger<OrderCompletedIntegrationEventHandler> logger,
		IOrderRepository repository,
		IAnalyticsDb db)
		: IntegrationEventHandler<OrderCompletedIntegrationEvent>
	{
		/// <inheritdoc/>
		public override async Task Handle(OrderCompletedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
		{
			var address = integrationEvent.Address is null ? Result.Success<Address>(null)
													: Address.Create(integrationEvent.Address.Country,
																	integrationEvent.Address.Region,
																	integrationEvent.Address.District,
																	integrationEvent.Address.City,
																	integrationEvent.Address.Street,
																	integrationEvent.Address.Home);

			if (address.IsFailure)
			{
				logger.LogError("Failed to create order address due to: {@errors}", address.Errors);
				return;
			}

			var orderItems = integrationEvent.Items.Select(i => OrderItem.Create(new OrderItemId(i.Id),
																				new BookId(i.BookId),
																				i.Title,
																				i.Language,
																				new BookSourceId(i.SourceId),
																				i.FormatName,
																				i.UnitPrice,
																				i.Quantity,
																				i.ISBN,
																				i.Cover)).ToArray();

			var combinedOrderItemsResult = Result.Combine(orderItems);
			if (combinedOrderItemsResult.IsFailure)
			{
				logger.LogError("Failed to create order items due to: {@errors}", combinedOrderItemsResult.Errors);
				return;
			}

			var order = Order.Create(new OrderId(integrationEvent.OrderId),
									new CustomerId(integrationEvent.CustomerId),
									orderItems.Select(o => o.Value)!,
									integrationEvent.OrderedDateTimeUtc,
									address.Value);

			if (order.IsFailure)
			{
				logger.LogError("Failed to create order due to: {@errors}", order.Errors);
				return;
			}

			repository.Create(order.Value!);
			await db.SaveChangesAsync(cancellationToken);
		}
	}
}
