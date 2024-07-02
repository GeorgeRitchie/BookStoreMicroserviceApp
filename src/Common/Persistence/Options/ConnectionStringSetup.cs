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

namespace Persistence.Options
{
	/// <summary>
	/// Represents the <see cref="ConnectionStringOptions"/> setup.
	/// </summary>
	internal sealed class ConnectionStringSetup : IConfigureOptions<ConnectionStringOptions>
	{
		private const string ConnectionStringName = "Database";
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="ConnectionStringSetup"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public ConnectionStringSetup(IConfiguration configuration) => _configuration = configuration;

		/// <inheritdoc />
		public void Configure(ConnectionStringOptions options)
			=> options.Value = _configuration.GetConnectionString(ConnectionStringName);
	}
}
