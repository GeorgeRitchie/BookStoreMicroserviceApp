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

namespace Service.Carts.Infrastructure.BackgroundJobs.ProcessInboxMessages
{
	/// <summary>
	/// Represents the <see cref="ProcessInboxMessagesJob"/> configuration.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="ProcessInboxMessagesConfiguration"/> class.
	/// </remarks>
	/// <param name="options">The options.</param>
	internal sealed class ProcessInboxMessagesConfiguration(IOptions<ProcessInboxMessagesOptions> options)
		: IRecurringJobConfiguration
	{
		private readonly ProcessInboxMessagesOptions _options = options.Value;

		/// <inheritdoc />
		public string Name => typeof(ProcessInboxMessagesJob).FullName!;

		/// <inheritdoc />
		public Type Type => typeof(ProcessInboxMessagesJob);

		/// <inheritdoc />
		public int IntervalInSeconds => _options.IntervalInSeconds;
	}
}
