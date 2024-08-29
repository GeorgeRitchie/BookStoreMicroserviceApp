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

using Microsoft.Extensions.Options;

namespace Service.Carts.WebApi.Options
{
	/// <summary>
	/// Represents the <see cref="WebApiOptions"/> setup.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="WebApiOptionsSetup"/> class.
	/// </remarks>
	/// <param name="configuration">The configuration.</param>
	internal sealed class WebApiOptionsSetup(IConfiguration configuration) : IConfigureOptions<WebApiOptions>
	{
		private const string ConfigurationSectionName = "Service:Cart:WebApiOptions";

		/// <inheritdoc />
		public void Configure(WebApiOptions options)
			=> configuration.GetSection(ConfigurationSectionName).Bind(options);
	}
}
