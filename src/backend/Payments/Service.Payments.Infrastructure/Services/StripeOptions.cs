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

namespace Service.Payments.Infrastructure.Services
{
	/// <summary>
	/// Represents the <see cref="StripePaymentService"/> options.
	/// </summary>
	internal sealed class StripeOptions
	{
		/// <summary>
		/// Gets the url for success redirect address.
		/// </summary>
		public string SuccessUrlAddress { get; set; }

		/// <summary>
		/// Gets the url for failure redirect address.
		/// </summary>
		public string FailureUrlAddress { get; set; }

		/// <summary>
		/// Gets the Stripe server api key.
		/// </summary>
		public string ApiKey { get; set; }
	}
}
