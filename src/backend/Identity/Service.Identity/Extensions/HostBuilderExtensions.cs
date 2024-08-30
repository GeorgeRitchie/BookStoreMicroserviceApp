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

using Serilog;

namespace Service.Identity.Extensions
{
	/// <summary>
	/// Contains extensions methods for the <see cref="IHostBuilder"/> interface.
	/// </summary>
	internal static class HostBuilderExtensions
	{
		/// <summary>
		/// Configures Serilog as a logging providers using the configuration defined in the application settings.
		/// </summary>
		/// <param name="hostBuilder">The host builder.</param>
		internal static void UseSerilogWithConfiguration(this IHostBuilder hostBuilder) =>
			hostBuilder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
				loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));
	}
}
