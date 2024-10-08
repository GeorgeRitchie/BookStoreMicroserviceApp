﻿/* 
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
using Quartz;

namespace Service.Carts.WebApi.ServiceInstallers.BackgroundJobs
{
	/// <summary>
	/// Represents the <see cref="QuartzOptions"/> setup.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RecurringJobsSetup"/> class.
	/// </remarks>
	/// <param name="recurringJobConfigurations">The recurring job configurations.</param>
	internal sealed class RecurringJobsSetup(IEnumerable<IRecurringJobConfiguration> recurringJobConfigurations)
		: IConfigureOptions<QuartzOptions>
	{
		/// <inheritdoc />
		public void Configure(QuartzOptions options) =>
			recurringJobConfigurations.ForEachElement(configuration =>
				options
					.AddJob(
						configuration.Type,
						jobBuilder => jobBuilder.WithIdentity(configuration.Name))
					.AddTrigger(triggerBuilder =>
						triggerBuilder
							.ForJob(configuration.Name)
							.WithSimpleSchedule(scheduleBuilder =>
								scheduleBuilder
									.WithIntervalInSeconds(configuration.IntervalInSeconds)
									.RepeatForever())));
	}
}
