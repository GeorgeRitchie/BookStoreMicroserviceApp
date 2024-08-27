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

using Application.Behaviors;
using Application.Mapper;
using FluentValidation;
using Infrastructure.Configuration;
using Infrastructure.Utilities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Analytics.Infrastructure.Idempotence;
using BaseApplication = Application;
using AnalyticsApplication = Service.Analytics.Application;

namespace Service.Analytics.Infrastructure.ServiceInstallers
{
	/// <summary>
	/// Represents the Analytics service application layer service installer.
	/// </summary>
	internal sealed class ApplicationServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration) =>
			services
				.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AnalyticsApplication.AssemblyReference.Assembly))
				// TODO __##__ Add here your Mediator pipeline behaviors
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
				// TODO __##__ Add here your FluentValidation validators, or assemblies that contain such validators
				.AddValidatorsFromAssembly(
					AnalyticsApplication.AssemblyReference.Assembly,
					includeInternalTypes: true)
				.AddValidatorsFromAssembly(BaseApplication.AssemblyReference.Assembly, includeInternalTypes: true)
				// TODO __##__ Add here your AutoMapper mappers, or assemblies that contain such mappers
				.AddAutoMapper(config => config.AddProfile(new AssemblyMappingProfile(AnalyticsApplication.AssemblyReference.Assembly)))
				.Tap(DecorateDomainEventHandlersWithIdempotency)
				.Tap(AddAndDecorateIntegrationEventHandlersWithIdempotency);

		private static void DecorateDomainEventHandlersWithIdempotency(IServiceCollection services) =>
			// TODO __##__ Use here assembly with Domain event handlers that you want decorate with IdempotentDomainEventHandler
			AnalyticsApplication.AssemblyReference.Assembly
				.GetTypes()
				.Where(EventHandlersUtility.ImplementsDomainEventHandler)
				.ForEachElement(type =>
				{
					Type closedNotificationHandler = type.GetInterfaces().First(EventHandlersUtility.IsNotificationHandler);

					Type[] arguments = closedNotificationHandler.GetGenericArguments();

					Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(arguments);

					services.Decorate(closedNotificationHandler, closedIdempotentHandler);
				});

		private static void AddAndDecorateIntegrationEventHandlersWithIdempotency(IServiceCollection services) =>
			// TODO __##__ Use here assembly with Integration event handlers that you want decorate with IdempotentIntegrationEventHandler
			AnalyticsApplication.AssemblyReference.Assembly
				.GetTypes()
				.Where(EventHandlersUtility.ImplementsIntegrationEventHandler)
				.ForEachElement(integrationEventHandlerType =>
				{
					Type closedIntegrationEventHandler = integrationEventHandlerType
						.GetInterfaces()
						.First(EventHandlersUtility.IsIntegrationEventHandler);

					Type[] arguments = closedIntegrationEventHandler.GetGenericArguments();

					Type closedIdempotentHandler = typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(arguments);

					services.AddScoped(integrationEventHandlerType);

					services.Decorate(integrationEventHandlerType, closedIdempotentHandler);
				});
	}
}
