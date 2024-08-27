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

namespace Service.Analytics.Infrastructure.BackgroundJobs.ProcessInboxMessages
{
	/// <summary>
	/// Represents the <see cref="ProcessInboxMessagesJob"/> options.
	/// </summary>
	internal sealed class ProcessInboxMessagesOptions
	{
		/// <summary>
		/// Gets the interval in seconds.
		/// </summary>
		public int IntervalInSeconds { get; init; }

		/// <summary>
		/// Gets the retry count.
		/// </summary>
		public int RetryCount { get; init; }

		/// <summary>
		/// Gets the batch size.
		/// </summary>
		public int BatchSize { get; init; }
	}
}
