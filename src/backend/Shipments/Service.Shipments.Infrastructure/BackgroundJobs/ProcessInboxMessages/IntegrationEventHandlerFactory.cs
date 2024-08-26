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
using Infrastructure.Utilities;
using System.Collections.Concurrent;

namespace Service.Shipments.Infrastructure.BackgroundJobs.ProcessInboxMessages
{
	/// <summary>
	/// Represents the integration event handler factory.
	/// </summary>
	internal static class IntegrationEventHandlerFactory
	{
		private static readonly ConcurrentDictionary<Type, List<Type>> EventHandlersDictionary = new();

		/// <summary>
		/// Gets the handlers for the specified type.
		/// </summary>
		/// <param name="type">The integration event type.</param>
		/// <param name="serviceProvider">The service provider.</param>
		/// <returns>THe collection of <see cref="IIntegrationEventHandler"/> instance that handle the specified integration event type.</returns>
		internal static IEnumerable<IIntegrationEventHandler> GetHandlers(Type type, IServiceProvider serviceProvider)
		{
			if (!EventHandlersDictionary.ContainsKey(type))
			{
				AddHandlersToDictionary(type);
			}

			foreach (Type eventHandlerType in EventHandlersDictionary[type])
			{
				yield return (serviceProvider.GetService(eventHandlerType) as IIntegrationEventHandler)!;
			}
		}

		private static void AddHandlersToDictionary(Type type) =>
			Application.AssemblyReference.Assembly
				.GetTypes()
				.Where(EventHandlersUtility.ImplementsIntegrationEventHandler)
				.ForEachElement(integrationEventHandlerType =>
				{
					Type closedIntegrationEventHandler = integrationEventHandlerType
						.GetInterfaces()
						.First(EventHandlersUtility.IsIntegrationEventHandler);

					Type[] arguments = closedIntegrationEventHandler.GetGenericArguments();

					if (arguments[0] != type)
					{
						return;
					}

					EventHandlersDictionary.AddOrUpdate(
						type,
						_ => [integrationEventHandlerType],
						(_, handlersList) =>
						{
							handlersList.Add(integrationEventHandlerType);

							return handlersList;
						});
				});
	}
}
