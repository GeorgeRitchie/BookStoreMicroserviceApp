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

using Infrastructure.Configuration;
using Infrastructure.Extensions;
using MassTransit;

namespace Service.CatalogWrite.WebApi.ServiceInstallers.EventBus
{
	/// <summary>
	/// Represents the event bus service installer.
	/// </summary>
	internal sealed class EventBusServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration) =>
			services
				.ConfigureOptions<MassTransitHostOptionsSetup>()
				// TODO __##__ Use for RabbitMQ Message Broker implementation.
				.ConfigureOptions<RabbitMqOptionsSetup>()
				.AddMassTransit(busConfigurator =>
				{
					busConfigurator.SetKebabCaseEndpointNameFormatter();

					busConfigurator.AddConsumersFromAssemblies(Infrastructure.AssemblyReference.Assembly);

					busConfigurator.AddRequestClientsFromAssemblies(Authorization.AssemblyReference.Assembly);

					// TODO __##__ Use for In Memory Message Broker implementation.
					busConfigurator.UsingInMemory((context, configurator) => configurator.ConfigureEndpoints(context));

					// TODO __##__ Use for RabbitMQ Message Broker implementation.
					//busConfigurator.UsingRabbitMq((context, configurator) =>
					//{
					//	var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

					//	configurator.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, h =>
					//	{
					//		h.Username(rabbitMqOptions.Username);
					//		h.Password(rabbitMqOptions.Password);
					//	});

					//	configurator.ConfigureEndpoints(context);
					//});
				});
	}
}
