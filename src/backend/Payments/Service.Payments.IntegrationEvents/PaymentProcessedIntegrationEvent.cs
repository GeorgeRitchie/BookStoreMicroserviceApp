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
using Shared.Errors;

namespace Service.Payments.IntegrationEvents
{
	/// <summary>
	/// Represents the integration event for the payment processed result.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="OrderId">The order identifier this payment belongs to.</param>
	/// <param name="StatusName">The result status name.</param>
	/// <param name="Error">The error of failure result.</param>
	/// <param name="UserInteractionUrl">The url used by user to finish payment process.</param>
	public sealed record PaymentProcessedIntegrationEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		Guid OrderId,
		string StatusName,
		Error? Error,
		string? UserInteractionUrl) : IntegrationEvent(Id, OccurredOnUtc);
}
