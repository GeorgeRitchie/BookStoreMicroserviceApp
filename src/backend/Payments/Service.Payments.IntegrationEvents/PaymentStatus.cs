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

using Domain.Primitives;

namespace Service.Payments.IntegrationEvents
{
	/// <summary>
	/// Represents the Payment status enumeration.
	/// </summary>
	public sealed class PaymentStatus : Enumeration<PaymentStatus>
	{
		public static readonly PaymentStatus Pending = new("Pending", 0);
		public static readonly PaymentStatus UserInteractionRequired = new("UserInteractionRequired", 1);
		public static readonly PaymentStatus Failed = new("Failed", 2);
		public static readonly PaymentStatus Successful = new("Successful", 3);

		/// <summary>
		/// Initializes a new instance of the <see cref="PaymentStatus"/> class.
		/// </summary>
		/// <inheritdoc/>
		private PaymentStatus(string name, int value) : base(name, value)
		{
		}
	}
}
