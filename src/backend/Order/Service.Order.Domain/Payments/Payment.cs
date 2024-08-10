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

using Service.Payments.IntegrationEvents;

namespace Service.Order.Domain.Payments
{
	/// <summary>
	/// Represents the Payment entity.
	/// </summary>
	public sealed class Payment : Entity<PaymentId>, IAuditable
	{
		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the payment status.
		/// </summary>
		public PaymentStatus Status { get; private set; }

		/// <summary>
		/// Gets the failure payment error.
		/// </summary>
		public Error? Error { get; private set; }

		/// <summary>
		/// Gets the url for payment required user interaction.
		/// </summary>
		public Uri? UserInteractionUrl { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Payment"/> class.
		/// </summary>
		/// <param name="id">The payment identifier.</param>
		/// <param name="isDeleted">The payment deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private Payment(PaymentId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Payment"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Payment()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="Payment"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="paymentStatus">Payment status.</param>
		/// <param name="error">Failure payment error.</param>
		/// <param name="userInteractionUrl">Payment user interaction url.</param>
		/// <returns>The new <see cref="Payment"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Payment> Create(
			PaymentStatus paymentStatus,
			Error? error = null,
			string? userInteractionUrl = null)
		{
			Uri? validUrl = null;
			return Result.Create(userInteractionUrl is null
								|| IsValidUrl(userInteractionUrl, out validUrl))
							.Map(() => new Payment(new PaymentId(Guid.NewGuid()), false)
							{
								Status = paymentStatus,
								Error = error,
								UserInteractionUrl = validUrl,
							})
							.MapFailure(() => PaymentErrors.InvalidUrl(userInteractionUrl));
		}

		/// <summary>
		/// Changes the payment information.
		/// </summary>
		/// <param name="paymentStatus">Payment status.</param>
		/// <param name="error">Failure payment error.</param>
		/// <param name="userInteractionUrl">Payment user interaction url.</param>
		/// <returns>The updated payment.</returns>
		public Result<Payment> Update(
			PaymentStatus paymentStatus,
			Error? error = null,
			string? userInteractionUrl = null)
		{
			Uri? validUrl = null;
			return Result.Success(this)
					.Ensure(e => userInteractionUrl is null || IsValidUrl(userInteractionUrl, out validUrl),
							PaymentErrors.InvalidUrl(userInteractionUrl))
					.Tap(payment =>
					{
						payment.Status = paymentStatus;
						payment.Error = error;
						payment.UserInteractionUrl = validUrl;
					});
		}

		private static bool IsValidUrl(string? url, out Uri? validatedUri)
		{
			// UriKind.Absolute ensures that the URL is fully qualified (has scheme, etc.)
			return Uri.TryCreate(url, UriKind.Absolute, out validatedUri)
					&& (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
		}
	}
}
