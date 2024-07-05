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
using Service.CatalogWrite.Infrastructure.BackgroundJobs.ProcessInboxMessages;

namespace Service.CatalogWrite.Infrastructure.ServiceInstallers
{
	/// <summary>
	/// Represents the <see cref="ProcessInboxMessagesOptions"/> setup.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="ProcessInboxMessagesOptionsSetup"/> class.
	/// </remarks>
	/// <param name="configuration">The configuration.</param>
	internal sealed class ProcessInboxMessagesOptionsSetup(IConfiguration configuration)
		: IConfigureOptions<ProcessInboxMessagesOptions>
	{
		private const string ConfigurationSectionName = "Service:CatalogWrite:BackgroundJobs:ProcessInboxMessages";

		/// <inheritdoc />
		public void Configure(ProcessInboxMessagesOptions options)
			=> configuration.GetSection(ConfigurationSectionName).Bind(options);
	}
}
