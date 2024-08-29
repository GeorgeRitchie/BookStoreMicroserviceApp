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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Authorization.Options
{
	/// <summary>
	/// Represents the <see cref="PermissionAuthorizationOptions"/> setup.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="PermissionAuthorizationOptionsSetup"/> class.
	/// </remarks>
	/// <param name="configuration">The configuration.</param>
	internal sealed class PermissionAuthorizationOptionsSetup(IConfiguration configuration)
		: IConfigureOptions<PermissionAuthorizationOptions>
	{
		private const string ConfigurationSectionName = "Authorization:Permissions";

		/// <inheritdoc />
		public void Configure(PermissionAuthorizationOptions options)
			=> configuration.GetSection(ConfigurationSectionName).Bind(options);
	}
}
