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

using Microsoft.Extensions.Options;
using Service.Payments.Application.Common.Interfaces;
using Service.Payments.Domain.Payments;
using Stripe;
using Stripe.Checkout;

namespace Service.Payments.Infrastructure.Services
{
	/// <summary>
	/// Represents the Stripe payment server interaction adapter.
	/// </summary>
	/// <param name="options">The Stripe options.</param>
	internal sealed class StripePaymentService(
		IOptions<StripeOptions> options) : IPaymentService
	{
		// For more info see https://www.youtube.com/watch?v=SYazJ2ddZ4s&t=286s
		// For more info see https://docs.stripe.com/payments/accept-a-payment
		// For more info see https://docs.stripe.com/payments/checkout/custom-success-page
		// For more extra info see https://www.youtube.com/playlist?list=PLy1nL-pvL2M4cNNoUtjWevYSci4ubsbhC
		// For more extra info see https://www.youtube.com/playlist?list=PLS6F722u-R6Jux1EXZf5FrvIxVGKREVEn

		/// <inheritdoc/>
		public async Task<string> CreateCheckoutSessionWithUrlAsync(Payment payment, CancellationToken cancellationToken = default)
		{
			StripeConfiguration.ApiKey = options.Value.ApiKey;

			var lineItems = payment.Items.Select(i => new SessionLineItemOptions
			{
				PriceData = new SessionLineItemPriceDataOptions
				{
					UnitAmountDecimal = i.UnitPrice * 100,
					Currency = "usd",
					ProductData = new SessionLineItemPriceDataProductDataOptions
					{
						Name = i.Title,
						Images = [i.Cover],
					}
				},
				Quantity = i.Quantity,
			}).ToList();

			var sessionOptions = new SessionCreateOptions
			{
				PaymentMethodTypes = ["card"],
				LineItems = lineItems,
				Mode = "payment",
				SuccessUrl = GetSuccessUrl(payment.Id),
				CancelUrl = GetCancelUrl(payment.Id),
				PaymentIntentData = new SessionPaymentIntentDataOptions
				{
					SetupFutureUsage = "off_session",
				},
			};

			var service = new SessionService();
			Session session = await service.CreateAsync(sessionOptions, cancellationToken: cancellationToken);

			return session.Url;
		}

		private string GetSuccessUrl(PaymentId paymentId)
		{
			var baseUrl = options.Value.SuccessUrlAddress.TrimEnd('/');
			return baseUrl + "?pid=" + paymentId.Value.ToString();
		}

		private string GetCancelUrl(PaymentId paymentId)
		{
			var baseUrl = options.Value.FailureUrlAddress.TrimEnd('/');
			return baseUrl + "?pid=" + paymentId.Value.ToString();
		}
	}
}
