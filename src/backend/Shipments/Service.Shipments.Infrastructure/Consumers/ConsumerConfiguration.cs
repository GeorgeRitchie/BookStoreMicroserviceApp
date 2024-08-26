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

using Infrastructure.EventBus;
using MassTransit;
using Service.Catalog.IntegrationEvents;
using Service.Orders.IntegrationEvents;
using Service.Shipments.Domain;
using Service.Shipments.Infrastructure.Idempotence;

namespace Service.Shipments.Infrastructure.Consumers
{
	/// <summary>
	/// Represents the consumer configuration for the Shipment service.
	/// </summary>
	internal sealed class ConsumerConfiguration : IConsumerConfiguration
	{
		/// <inheritdoc />
		public void AddConsumers(IRegistrationConfigurator registrationConfigurator)
		{
			registrationConfigurator.AddConsumer<IntegrationEventConsumer<BookCreatedIntegrationEvent, IShipmentDb>>();
			registrationConfigurator.AddConsumer<IntegrationEventConsumer<BookUpdatedIntegrationEvent, IShipmentDb>>();
			registrationConfigurator.AddConsumer<IntegrationEventConsumer<BookSourceCreatedIntegrationEvent, IShipmentDb>>();
			registrationConfigurator.AddConsumer<IntegrationEventConsumer<BookSourceUpdatedIntegrationEvent, IShipmentDb>>();
			registrationConfigurator.AddConsumer<IntegrationEventConsumer<ShipmentRequestedIntegrationEvent, IShipmentDb>>();

			// TODO __##__ Add here message-broker message consumers
		}
	}
}
