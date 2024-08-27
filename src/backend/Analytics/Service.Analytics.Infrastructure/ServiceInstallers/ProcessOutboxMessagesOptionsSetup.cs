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
using Service.Analytics.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

namespace Service.Analytics.Infrastructure.ServiceInstallers
{
	/// <summary>
	/// Represents the <see cref="ProcessOutboxMessagesOptions"/> setup.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="ProcessOutboxMessagesOptionsSetup"/> class.
	/// </remarks>
	/// <param name="configuration">The configuration.</param>
	internal sealed class ProcessOutboxMessagesOptionsSetup(IConfiguration configuration)
		: IConfigureOptions<ProcessOutboxMessagesOptions>
	{
		private const string ConfigurationSectionName = "Service:Analytics:BackgroundJobs:ProcessOutboxMessages";

		/// <inheritdoc />
		public void Configure(ProcessOutboxMessagesOptions options)
			=> configuration.GetSection(ConfigurationSectionName).Bind(options);
	}
}
