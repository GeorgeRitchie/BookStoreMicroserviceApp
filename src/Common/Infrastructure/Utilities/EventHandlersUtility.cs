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
using Application.Messaging;
using MediatR;

namespace Infrastructure.Utilities
{
	/// <summary>
	/// Represents the event handlers utility class.
	/// </summary>
	public static class EventHandlersUtility
	{
		private static readonly Type NotificationHandlerType = typeof(INotificationHandler<>);
		private static readonly Type DomainEventHandlerType = typeof(IDomainEventHandler<>);
		private static readonly Type IntegrationEventHandlerType = typeof(IIntegrationEventHandler<>);

		/// <summary>
		/// Checks if the specified type implements the <see cref="IDomainEventHandler{TEvent}"/>.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>True if the specified type implements the <see cref="IDomainEventHandler{TEvent}"/>, otherwise false. </returns>
		public static bool ImplementsDomainEventHandler(Type type) =>
			type.GetInterfaces().Length != 0 &&
			type.GetInterfaces().All(interfaceType => IsNotificationHandler(interfaceType) || IsDomainEventHandler(interfaceType));

		/// <summary>
		/// Checks if the specified type implements the <see cref="IIntegrationEventHandler{TEvent}"/>.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>True if the specified type implements the <see cref="IIntegrationEventHandler{TEvent}"/>, otherwise false. </returns>
		public static bool ImplementsIntegrationEventHandler(Type type) => type.GetInterfaces().Any(IsIntegrationEventHandler);

		/// <summary>
		/// Checks if the specified type inherits from <see cref="INotificationHandler{TEvent}"/>.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>True if the specified type inherits from <see cref="INotificationHandler{TEvent}"/>, otherwise false. </returns>
		public static bool IsNotificationHandler(Type type) =>
			type.IsGenericType &&
			type.Name.StartsWith(NotificationHandlerType.Name, StringComparison.InvariantCulture);

		/// <summary>
		/// Checks if the specified type inherits from <see cref="IDomainEventHandler{TEvent}"/>.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>True if the specified type inherits from <see cref="IDomainEventHandler{TEvent}"/>, otherwise false. </returns>
		public static bool IsDomainEventHandler(Type type) =>
			type.IsGenericType &&
			type.Name.StartsWith(DomainEventHandlerType.Name, StringComparison.InvariantCulture);

		/// <summary>
		/// Checks if the specified type inherits from <see cref="IIntegrationEventHandler{TEvent}"/>.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>True if the specified type inherits from <see cref="IIntegrationEventHandler{TEvent}"/>, otherwise false. </returns>
		public static bool IsIntegrationEventHandler(Type type) =>
			type.IsGenericType &&
			type.Name.StartsWith(IntegrationEventHandlerType.Name, StringComparison.InvariantCulture);
	}
}
