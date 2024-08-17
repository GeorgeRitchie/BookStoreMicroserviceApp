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

namespace Service.Orders.Domain.Payments
{
	/// <summary>
	/// Contains the Payment errors.
	/// </summary>
	public static class PaymentErrors
	{
		/// <summary>
		/// Gets the invalid user interaction url error.
		/// </summary>
		/// <param name="url">The invalid url.</param>
		/// <returns>The error.</returns>
		public static Error InvalidUrl(string? url)
			=> new("Payment.InvalidUrl", $"The user interaction url is invalid: {url}");
	}
}
