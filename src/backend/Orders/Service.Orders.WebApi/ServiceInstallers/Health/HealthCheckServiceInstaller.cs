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
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Service.Orders.Persistence;
using Service.Orders.WebApi.ServiceInstallers.DistributedCaching;
using Service.Orders.WebApi.ServiceInstallers.EventBus;

namespace Service.Orders.WebApi.ServiceInstallers.Health
{
	/// <summary>
	/// Represents the all health check services installer.
	/// </summary>
	internal sealed class HealthCheckServiceInstaller : IServiceInstaller
	{
		// For more info see https://www.youtube.com/watch?v=4abSfjdzqms

		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
		{
			var healthCheckBuilder = services.AddHealthChecks();

			healthCheckBuilder.AddDbContextCheck<OrderDbContext>();

			if (DistributedCacheServiceInstaller.IsRedisEnabled(configuration))
				healthCheckBuilder.AddRedis(serviceProvider
					=> serviceProvider.GetRequiredService<IOptions<RedisCacheOptions>>().Value.Configuration
						?? string.Empty);

			if (RabbitMqOptionsSetup.IsRabbitMqEnabled(configuration))
				healthCheckBuilder.AddRabbitMQ((serviceProvider, options) =>
				{
					var rabbitMqOptions = serviceProvider.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
					options.ConnectionFactory = new ConnectionFactory()
					{
						HostName = rabbitMqOptions.Host,
						VirtualHost = rabbitMqOptions.VirtualHost,
						UserName = rabbitMqOptions.Username,
						Password = rabbitMqOptions.Password,
						Port = AmqpTcpEndpoint.UseDefaultPort
					};
				});

			// TODO __##__ Add here health checks for new stuff that needed to be health monitored
		}
	}
}
