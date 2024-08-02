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

using Infrastructure.BackgroundJobs;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Service.Catalog.Infrastructure.ServiceInstallers
{
	/// <summary>
	/// Represents the CategoryWrite service background jobs service installer.
	/// </summary>
	internal sealed class BackgroundJobsServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration) =>
			services
				.ConfigureOptions<ProcessInboxMessagesOptionsSetup>()
				.ConfigureOptions<ProcessOutboxMessagesOptionsSetup>()
				.Tap(AddRecurringJobConfigurations);

		private static void AddRecurringJobConfigurations(IServiceCollection services) =>
			services.Scan(scan =>
				scan.FromAssemblies(AssemblyReference.Assembly)
					.AddClasses(filter => filter.Where(type => type.IsAssignableTo(typeof(IRecurringJobConfiguration))), false)
					.UsingRegistrationStrategy(RegistrationStrategy.Append)
					.AsImplementedInterfaces()
					.WithTransientLifetime());
	}
}
