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
using Service.Payments.Domain;
using Service.Payments.Domain.Payments;
using Service.Payments.IntegrationEvents;

namespace Service.Payments.Application.Payments.PaymentFailure
{
	/// <summary>
	/// Represents the <see cref="PaymentFailureCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="PaymentFailureCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The payment repository.</param>
	/// <param name="eventBus">The event bus.</param>
	internal sealed class PaymentFailureCommandHandler(
		IPaymentDb db,
		IPaymentRepository repository,
		IEventBus eventBus)
		: ICommandHandler<PaymentFailureCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(PaymentFailureCommand request, CancellationToken cancellationToken)
			=> await Result.Create(
						await repository.GetAll()
							.FirstOrDefaultAsync(i => i.Id == new PaymentId(request.PaymentId), cancellationToken))
				.MapFailure(() => PaymentErrors.NotFound(new PaymentId(request.PaymentId)))
				.Tap(payment =>
				{
					payment.Status = PaymentStatus.Failed;
					repository.Update(payment);
				})
				.Tap(() => db.SaveChangesAsync(cancellationToken))
				.Tap(payment => eventBus.PublishAsync(new PaymentProcessedIntegrationEvent(
											Guid.NewGuid(),
											DateTime.UtcNow,
											payment.OrderId.Value,
											payment.Status.Name,
											PaymentErrors.PaymentFailed(),
											null),
										cancellationToken));
	}
}
