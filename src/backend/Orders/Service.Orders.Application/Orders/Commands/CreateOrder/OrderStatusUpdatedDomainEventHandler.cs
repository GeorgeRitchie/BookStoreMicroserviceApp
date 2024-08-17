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
using MediatR;
using Service.Catalog.IntegrationEvents;
using Service.Orders.Application.Common.Interfaces;
using Service.Orders.Domain;
using Service.Orders.Domain.Orders;
using Service.Orders.Domain.Orders.Events;
using Service.Orders.IntegrationEvents;

namespace Service.Orders.Application.Orders.Commands.CreateOrder
{
	/// <summary>
	/// Represents the <see cref="OrderStatusUpdatedDomainEvent"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="OrderStatusUpdatedDomainEventHandler"/> class.
	/// </remarks>
	/// <param name="logger">The logger.</param>
	/// <param name="grpcService">The grpc service.</param>
	/// <param name="eventBus">The event bus.</param>
	/// <param name="repository">The order repository.</param>
	/// <param name="db">The database.</param>
	internal sealed class OrderStatusUpdatedDomainEventHandler(
		ILogger<OrderStatusUpdatedDomainEventHandler> logger,
		IOrderGrpcService grpcService,
		IEventBus eventBus,
		IOrderRepository repository,
		IOrderDb db)
		: IDomainEventHandler<OrderStatusUpdatedDomainEvent>
	{
		/// <inheritdoc />
		public async Task Handle(OrderStatusUpdatedDomainEvent notification, CancellationToken cancellationToken)
		{
			if (notification.Status == OrderStatus.PaymentProcessing)
			{
				await eventBus.PublishAsync(new PaymentRequestedIntegrationEvent(
					notification.Id,
					notification.OccurredOnUtc,
					notification.OrderId,
					notification.CustomerId.Value,
					notification.OrderedDateTimeUtc,
					notification.Items.Select(i => new OrderedItem(
													i.Id.Value,
													i.BookId.Value,
													i.Title,
													i.ISBN,
													i.Cover,
													i.Language,
													i.SourceId.Value,
													i.Format.Name,
													i.UnitPrice,
													i.Quantity))
										.ToList()), cancellationToken);
			}
			else if (notification.Status == OrderStatus.ShippingProcessing)
			{
				// TODO finish it
			}
			else if (notification.Status == OrderStatus.Failed)
			{
				var order = GetOrder(notification);

				if (order == null)
					return;

				if (order.Payment is not null)
				{
					var paperBooks = notification.Items.Where(i => i.Format == BookFormat.Paper).ToList();
					if (paperBooks.Count > 0)
					{
						var result = await grpcService.IncreasePaperBookSourceQuantityAsync(paperBooks, cancellationToken);

						if (result.IsFailure)
						{
							logger.LogError("Failed increase paper books amount: {@errors}", result.Errors);
						}
					}
				}
			}
			else if (notification.Status == OrderStatus.Completed)
			{
				// TODO finish it
			}
			else
			{
				logger.LogError("Received domain event {eventName} with unknow status {unknownStatus}", 
					nameof(OrderStatusUpdatedDomainEvent),
					notification.Status);
			}
		}

		private Order? GetOrder(OrderStatusUpdatedDomainEvent notification)
		{
			var order = repository.GetAll()
					.Include(o => o.Payment)
					.FirstOrDefault(i => i.Id == new OrderId(notification.OrderId));

			if (order == null)
				logger.LogError("Received domain event {eventName} that does not have entity with Id {id}",
					nameof(OrderStatusUpdatedDomainEvent),
					notification.OrderId);

			return order;
		}
	}
}
