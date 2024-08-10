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
using Quartz;

namespace Service.Order.WebApi.ServiceInstallers.BackgroundJobs
{
	/// <summary>
	/// Represents the <see cref="QuartzHostedServiceOptions"/> setup.
	/// </summary>
	internal sealed class QuartzHostedServiceOptionsSetup : IConfigureOptions<QuartzHostedServiceOptions>
	{
		/// <inheritdoc />
		public void Configure(QuartzHostedServiceOptions options) => options.WaitForJobsToComplete = true;
	}
}
