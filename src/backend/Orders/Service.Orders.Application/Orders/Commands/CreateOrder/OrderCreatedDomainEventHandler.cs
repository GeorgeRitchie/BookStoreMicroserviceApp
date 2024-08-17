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

using Service.Catalog.IntegrationEvents;
using Service.Orders.Application.Common.Interfaces;
using Service.Orders.Domain;
using Service.Orders.Domain.Orders;
using Service.Orders.Domain.Orders.Events;
using Service.Orders.IntegrationEvents;

namespace Service.Orders.Application.Orders.Commands.CreateOrder
{
	/// <summary>
	/// Represents the <see cref="OrderCreatedDomainEvent"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="OrderCreatedDomainEventHandler"/> class.
	/// </remarks>
	/// <param name="logger">The logger.</param>
	/// <param name="grpcService">The grpc service.</param>
	/// <param name="repository">The order repository.</param>
	/// <param name="db">The database.</param>
	internal sealed class OrderCreatedDomainEventHandler(
		ILogger<OrderCreatedDomainEventHandler> logger,
		IOrderGrpcService grpcService,
		IOrderRepository repository,
		IOrderDb db)
		: IDomainEventHandler<OrderCreatedDomainEvent>
	{
		/// <inheritdoc />
		public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
		{
			var order = await repository.GetAll()
											.FirstOrDefaultAsync(o => o.Id == new OrderId(notification.OrderId),
																cancellationToken);

			if (order == null)
			{
				logger.LogError("Received domain event {eventName} that does not have entity with Id {id}",
					nameof(OrderCreatedDomainEvent),
					notification.OrderId);
				return;
			}

			var paperBooks = notification.Items.Where(i => i.Format == BookFormat.Paper).ToList();
			if (paperBooks.Count > 0)
			{
				var result = await grpcService.DecreasePaperBookSourceQuantityAsync(paperBooks, cancellationToken);

				if (result.IsFailure)
				{
					logger.LogError("Failed decrease paper books amount: {@errors}", result.Errors);

					order.UpdateStatus(OrderStatus.Failed);
					repository.Update(order);
					await db.SaveChangesAsync(cancellationToken);
					return;
				}
			}

			await order.UpdateStatus(OrderStatus.PaymentProcessing)
							.Tap<Order>(repository.Update)
							.Tap(() => db.SaveChangesAsync(cancellationToken));
		}
	}
}
