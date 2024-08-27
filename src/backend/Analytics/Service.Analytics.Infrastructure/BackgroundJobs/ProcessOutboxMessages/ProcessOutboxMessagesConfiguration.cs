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
using Microsoft.Extensions.Options;

namespace Service.Analytics.Infrastructure.BackgroundJobs.ProcessOutboxMessages
{
	/// <summary>
	/// Represents the <see cref="ProcessOutboxMessagesJob"/> configuration.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="ProcessOutboxMessagesConfiguration"/> class.
	/// </remarks>
	/// <param name="options">The options.</param>
	internal sealed class ProcessOutboxMessagesConfiguration(IOptions<ProcessOutboxMessagesOptions> options)
		: IRecurringJobConfiguration
	{
		private readonly ProcessOutboxMessagesOptions _options = options.Value;

		/// <inheritdoc />
		public string Name => typeof(ProcessOutboxMessagesJob).FullName!;

		/// <inheritdoc />
		public Type Type => typeof(ProcessOutboxMessagesJob);

		/// <inheritdoc />
		public int IntervalInSeconds => _options.IntervalInSeconds;
	}
}
