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

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Service.Shipments.WebApi.ServiceInstallers.Authentication
{
	/// <summary>
	/// Represents the <see cref="JwtBearerOptions"/> setup.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="JwtBearerOptionsSetup"/> class.
	/// </remarks>
	/// <param name="configuration">The configuration.</param>
	internal sealed class JwtBearerOptionsSetup(IConfiguration configuration) 
		: IConfigureNamedOptions<JwtBearerOptions>
	{
		private const string ConfigurationSectionName = "JwtBearer";

		/// <inheritdoc />
		public void Configure(JwtBearerOptions options)
			=> configuration.GetSection(ConfigurationSectionName).Bind(options);

		/// <inheritdoc />
		public void Configure(string name, JwtBearerOptions options)
			=> configuration.GetSection(ConfigurationSectionName).Bind(options);
	}
}
