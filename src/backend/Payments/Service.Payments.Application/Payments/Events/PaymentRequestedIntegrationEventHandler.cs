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
using Service.Payments.Application.Common.Interfaces;
using Service.Payments.Domain;
using Service.Payments.Domain.Payments;
using Service.Payments.Domain.PurchaseItems;
using Service.Payments.IntegrationEvents;

namespace Service.Payments.Application.Payments.Events
{
	/// <summary>
	/// Represents the <see cref="PaymentRequestedIntegrationEvent"/> handler.
	/// </summary>
	/// <param name="repository">The payment repository.</param>
	/// <param name="paymentService">The payment service.</param>
	/// <param name="db">The database.</param>
	/// <param name="eventBus">The event bus.</param>
	internal sealed class PaymentRequestedIntegrationEventHandler(
		IPaymentRepository repository,
		IPaymentService paymentService,
		IPaymentDb db,
		IEventBus eventBus)
		: IntegrationEventHandler<PaymentRequestedIntegrationEvent>
	{
		/// <inheritdoc/>
		public override async Task Handle(PaymentRequestedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
		{
			var payment = Payment.Create(
				new OrderId(integrationEvent.OrderId),
				new CustomerId(integrationEvent.CustomerId),
				PaymentStatus.Pending,
				integrationEvent.OrderedDateTimeUtc,
				integrationEvent.Items.Select(i => PurchaseItem.Create(
					new OrderItemId(i.Id),
					new BookId(i.BookId),
					new BookSourceId(i.SourceId),
					i.Title,
					i.Cover,
					i.ISBN,
					i.Language,
					BookFormat.FromName(i.FormatName)!,
					i.UnitPrice,
					i.Quantity).Value)!).Value!;

			repository.Create(payment);
			await db.SaveChangesAsync(cancellationToken);

			payment.InteractionUrl = await paymentService.CreateCheckoutSessionWithUrlAsync(payment, cancellationToken);
			payment.Status = PaymentStatus.UserInteractionRequired;
			repository.Update(payment);
			await db.SaveChangesAsync(cancellationToken);

			await eventBus.PublishAsync(new PaymentProcessedIntegrationEvent(
					Guid.NewGuid(),
					DateTime.UtcNow,
					payment.OrderId.Value,
					payment.Status.Name,
					null,
					payment.InteractionUrl),
				cancellationToken);
		}
	}
}
