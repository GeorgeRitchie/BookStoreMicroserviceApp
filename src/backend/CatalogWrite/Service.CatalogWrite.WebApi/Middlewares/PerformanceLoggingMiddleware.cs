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
using Service.Catalog.WebApi.Options;
using System.Diagnostics;

namespace Service.Catalog.WebApi.Middlewares
{
	/// <summary>
	/// Contains extension methods for <see cref="PerformanceLoggingMiddlewareExtensions"/>.
	/// </summary>
	internal static class PerformanceLoggingMiddlewareExtensions
	{
		/// <summary>
		/// Adds <see cref="PerformanceLoggingMiddleware"/> to DI chain.
		/// </summary>
		/// <param name="builder">The application builder.</param>
		/// <returns>The application builder with <see cref="PerformanceLoggingMiddleware"/> added.</returns>
		public static IApplicationBuilder UsePerformanceLoggingMiddleware(this IApplicationBuilder builder)
			=> builder.UseMiddleware<PerformanceLoggingMiddleware>();
	}

	/// <summary>
	/// Represents the user request handling performance logging middleware.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="PerformanceLoggingMiddleware"/> class.
	/// </remarks>
	/// <param name="next">The next middleware to call.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="options">The web api options.</param>
	internal class PerformanceLoggingMiddleware(RequestDelegate next,
												ILogger<PerformanceLoggingMiddleware> logger,
												IOptions<WebApiOptions> options)
	{
		private readonly Stopwatch timer = new();

		/// <summary>
		/// Method used by DI to invoke for user requests in middlewares chain.
		/// </summary>
		/// <param name="context">The http context of user request.</param>
		/// <returns>The task.</returns>
		public async Task Invoke(HttpContext context)
		{
			try
			{
				timer.Start();
				await next(context);
			}
			finally
			{
				timer.Stop();
				LogPerformance(timer.ElapsedMilliseconds, context);
			}
		}

		private void LogPerformance(long elapsedMilliseconds, HttpContext context)
		{
			if (elapsedMilliseconds > options.Value.MaxRequestHandlingTimeMilliseconds)
			{
				logger.LogWarning(
					"Long handling request: {method} - {path} - {milliseconds} ms - max allowed: {maxAllowed} ms.",
					context.Request.Method,
					context.Request.Path,
					elapsedMilliseconds,
					options.Value.MaxRequestHandlingTimeMilliseconds);
			}
		}
	}
}
