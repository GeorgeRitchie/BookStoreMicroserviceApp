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
using Infrastructure.EventBus;
using MassTransit;
using Shared.Extensions;
using System.Reflection;

namespace Infrastructure.Extensions
{
	/// <summary>
	/// Contains extension methods for the <see cref="IRegistrationConfigurator"/> interface.
	/// </summary>
	public static class RegistrationConfiguratorExtensions
	{
		/// <summary>
		/// Adds the consumers defined in the specified assemblies.
		/// </summary>
		/// <param name="registrationConfigurator">The registration configurator.</param>
		/// <param name="assemblies">The assemblies to scan for consumers.</param>
		public static void AddConsumersFromAssemblies(this IRegistrationConfigurator registrationConfigurator, params Assembly[] assemblies) =>
			InstanceFactory
				.CreateFromAssemblies<IConsumerConfiguration>(assemblies)
				.ForEach(consumerInstaller => consumerInstaller.AddConsumers(registrationConfigurator));

		/// <summary>
		/// Adds the request clients defined in the specified assemblies.
		/// </summary>
		/// <param name="registrationConfigurator">The registration configurator.</param>
		/// <param name="assemblies">The assemblies to scan for request clients.</param>
		public static void AddRequestClientsFromAssemblies(this IRegistrationConfigurator registrationConfigurator, params Assembly[] assemblies) =>
			InstanceFactory
				.CreateFromAssemblies<IRequestClientConfiguration>(assemblies)
				.ForEach(consumerInstaller => consumerInstaller.AddRequestClients(registrationConfigurator));
	}
}
