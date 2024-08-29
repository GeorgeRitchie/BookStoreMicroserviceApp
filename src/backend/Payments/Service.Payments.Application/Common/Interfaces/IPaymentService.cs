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

namespace Service.Payments.Application.Common.Interfaces
{
	/// <summary>
	/// Represents the abstraction of real payments services (e.g. Stripe).
	/// </summary>
	public interface IPaymentService
	{
		/// <summary>
		/// Creates a checkout session on third party payment session and returns the url for user to interact with and finish the payment process.
		/// </summary>
		/// <param name="payment">Payment information.</param>
		/// <param name="cancellationToken">Cancelation token.</param>
		/// <returns>The url to created checkout session.</returns>
		Task<string> CreateCheckoutSessionWithUrlAsync(Payment payment, CancellationToken cancellationToken = default);
	}
}
