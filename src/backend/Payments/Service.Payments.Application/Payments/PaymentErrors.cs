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

using Service.Payments.Domain.Payments;

namespace Service.Payments.Application.Payments
{
	/// <summary>
	/// Contains the payment errors.
	/// </summary>
	internal static class PaymentErrors
	{
		/// <summary>
		/// Gets payment not found error.
		/// </summary>
		/// <param name="paymentId">The not found payment identifier.</param>
		/// <returns>The error.</returns>
		internal static NotFoundError NotFound(PaymentId paymentId)
			=> new("Payment.NotFound", $"Payment with the identifier {paymentId.Value} was not found.");

		/// <summary>
		/// Gets the payment failed error.
		/// </summary>
		/// <returns>The error.</returns>
		internal static Error PaymentFailed()
			=> new("Payment.Failed", "Payment process failed.");
	}
}
