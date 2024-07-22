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

using Application.ServiceLifetimes;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Shared.Extensions;
using System.Reflection;

namespace Infrastructure.Extensions
{
	/// <summary>
	/// Contains extension methods for the <see cref="IServiceCollection"/> interface.
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Installs the services using the <see cref="IServiceInstaller"/> implementations defined in the specified assemblies.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="assemblies">The assemblies to scan for service installer implementations.</param>
		/// <returns>The same service collection so that multiple calls can be chained.</returns>
		public static IServiceCollection InstallServicesFromAssemblies(
			this IServiceCollection services,
			IConfiguration configuration,
			params Assembly[] assemblies) =>
			services.Tap(
				() => InstanceFactory
					.CreateFromAssemblies<IServiceInstaller>(assemblies)
					.ForEachElement(serviceInstaller => serviceInstaller.Install(services, configuration)));

		/// <summary>
		/// Installs the modules using the <see cref="IModuleInstaller"/> implementations defined in the specified assemblies.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="assemblies">The assemblies to scan for module installer implementations.</param>
		/// <returns>The same service collection so that multiple calls can be chained.</returns>
		public static IServiceCollection InstallModulesFromAssemblies(
			this IServiceCollection services,
			IConfiguration configuration,
			params Assembly[] assemblies) =>
			services.Tap(
				() => InstanceFactory
					.CreateFromAssemblies<IModuleInstaller>(assemblies)
					.ForEachElement(moduleInstaller => moduleInstaller.Install(services, configuration)));

		/// <summary>
		/// Adds all of the implementations of <see cref="ITransient"/> inside the specified assembly as transient.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="assembly">The assembly to scan for transient services.</param>
		/// <returns>The same service collection so that multiple calls can be chained.</returns>
		public static IServiceCollection AddTransientAsMatchingInterfaces(
			this IServiceCollection services,
			Assembly assembly) =>
			services.Scan(scan =>
				scan.FromAssemblies(assembly)
					.AddClasses(filter => filter.AssignableTo<ITransient>(), false)
					.UsingRegistrationStrategy(RegistrationStrategy.Throw)
					.AsMatchingInterface()// TODO check will this register one class that implements several interfaces with each interface? example is Repository and ExtendedRepository in Persistence project
					.WithTransientLifetime());

		/// <summary>
		/// Adds all of the implementations of <see cref="IScoped"/> inside the specified assembly as scoped.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="assembly">The assembly to scan for scoped services.</param>
		/// <returns>The same service collection so that multiple calls can be chained.</returns>
		public static IServiceCollection AddScopedAsMatchingInterfaces(
			this IServiceCollection services,
			Assembly assembly) =>
			services.Scan(scan =>
				scan.FromAssemblies(assembly)
					.AddClasses(filter => filter.AssignableTo<IScoped>(), false)
					.UsingRegistrationStrategy(RegistrationStrategy.Throw)
					.AsMatchingInterface()// TODO check will this register one class that implements several interfaces with each interface? example is Repository and ExtendedRepository in Persistence project
					.WithScopedLifetime());
	}
}
