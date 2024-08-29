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
using Service.Orders.Domain.Payments;
using Service.Orders.IntegrationEvents;
using Service.Payments.IntegrationEvents;

namespace Service.Orders.Application.Orders.Events
{
	/// <summary>
	/// Represents the <see cref="PaymentProcessedIntegrationEvent"/> handler.
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="orderRepository">The order repository.</param>
	/// <param name="db">The database.</param>
	internal sealed class PaymentProcessedIntegrationEventHandler(
		ILogger<PaymentProcessedIntegrationEventHandler> logger,
		IOrderRepository orderRepository,
		IOrderDb db)
		: IntegrationEventHandler<PaymentProcessedIntegrationEvent>
	{
		/// <inheritdoc/>
		public override async Task Handle(PaymentProcessedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
		{
			if (integrationEvent.StatusName == PaymentStatus.UserInteractionRequired.Name)
			{
				var order = GetOrder(integrationEvent);

				if (order == null)
				{
					return;
				}

				var payment = CreatePayment(integrationEvent);

				if (payment == null)
				{
					return;
				}

				order.Payment = payment;
				orderRepository.Update(order);
				await db.SaveChangesAsync(cancellationToken);
			}
			else if (integrationEvent.StatusName == PaymentStatus.Failed.Name)
			{
				var order = GetOrder(integrationEvent);

				if (order == null)
				{
					return;
				}

				if (order.Payment == null)
				{
					order.Payment = CreatePayment(integrationEvent);
				}
				else
				{
					UpdatePayment(order, integrationEvent);
				}

				order.UpdateStatus(OrderStatus.Failed);
				orderRepository.Update(order);
				await db.SaveChangesAsync(cancellationToken);
			}
			else if (integrationEvent.StatusName == PaymentStatus.Successful.Name)
			{
				var order = GetOrder(integrationEvent);

				if (order == null)
				{
					return;
				}

				UpdatePayment(order, integrationEvent);

				order.UpdateStatus(OrderStatus.ShippingProcessing);
				orderRepository.Update(order);
				await db.SaveChangesAsync(cancellationToken);
			}
			else
			{
				logger.LogError("Payment message with unknow status {unknownPaymentStatus}", integrationEvent.StatusName);
			}
		}

		private Order? GetOrder(PaymentProcessedIntegrationEvent integrationEvent)
		{
			var order = orderRepository.GetAll()
					.Include(o => o.Items)
					.Include(o => o.Payment)
					.FirstOrDefault(i => i.Id == new OrderId(integrationEvent.OrderId));

			if (order == null)
				logger.LogError("Payment processed integration event received for unknown order id {orderId} - {@event}",
									 integrationEvent.OrderId,
									 integrationEvent);

			return order;
		}

		private Payment? CreatePayment(PaymentProcessedIntegrationEvent integrationEvent)
		{
			var paymentResult = Payment.Create(PaymentStatus.FromName(integrationEvent.StatusName)!,
												integrationEvent.Error,
												integrationEvent.UserInteractionUrl);

			if (paymentResult.IsFailure)
				logger.LogError("Payment entity creation failed {@errors}", paymentResult.Errors);

			return paymentResult.Value;
		}

		private bool UpdatePayment(Order order, PaymentProcessedIntegrationEvent integrationEvent)
		{
			var updateResult = order.Payment!.Update(PaymentStatus.FromName(integrationEvent.StatusName)!,
												integrationEvent.Error,
												integrationEvent.UserInteractionUrl);

			if (updateResult.IsFailure)
				logger.LogError("Payment entity update failed {@errors}", updateResult.Errors);

			return updateResult.IsSuccess;
		}
	}
}
