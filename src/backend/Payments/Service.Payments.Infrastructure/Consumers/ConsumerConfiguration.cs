﻿/* 
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

using Infrastructure.EventBus;
using MassTransit;
using Service.Orders.IntegrationEvents;
using Service.Payments.Domain;
using Service.Payments.Infrastructure.Idempotence;

namespace Service.Payments.Infrastructure.Consumers
{
	/// <summary>
	/// Represents the consumer configuration for the Payment service.
	/// </summary>
	internal sealed class ConsumerConfiguration : IConsumerConfiguration
	{
		/// <inheritdoc />
		public void AddConsumers(IRegistrationConfigurator registrationConfigurator)
		{
			registrationConfigurator.AddConsumer<IntegrationEventConsumer<PaymentRequestedIntegrationEvent, IPaymentDb>>();

			// TODO __##__ Add here message-broker message consumers
		}
	}
}
