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

using Serilog.Context;
using Service.Catalog.WebApi.Helpers;

namespace Service.Catalog.WebApi.Middlewares
{
	/// <summary>
	/// Contains extension methods for <see cref="CorrelationTokenMiddlewareExtensions"/>.
	/// </summary>
	internal static class CorrelationTokenMiddlewareExtensions
	{
		/// <summary>
		/// Adds <see cref="CorrelationTokenMiddleware"/> to DI chain.
		/// </summary>
		/// <param name="builder">The application builder.</param>
		/// <returns>The application builder with <see cref="CorrelationTokenMiddleware"/> added.</returns>
		public static IApplicationBuilder UseCorrelationTokenMiddleware(this IApplicationBuilder builder)
			=> builder.UseMiddleware<CorrelationTokenMiddleware>();
	}

	/// <summary>
	/// Represents correlation token setup middleware.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="CorrelationTokenMiddleware"/> class.
	/// </remarks>
	/// <param name="next">The next middleware to call.</param>
	/// <param name="logger">The logger.</param>
	internal class CorrelationTokenMiddleware(RequestDelegate next,
												ILogger<CorrelationTokenMiddleware> logger)
	{
		/// <summary>
		/// Method used by DI to invoke for user requests in middlewares chain.
		/// </summary>
		/// <param name="context">The http context of user request.</param>
		/// <returns>The task.</returns>
		public async Task Invoke(HttpContext context)
		{
			if (!context.Request.Headers.TryGetValue(ConstantValues.CorrelationTokenHeaderName, out var correlationTokenValue)
				|| !Guid.TryParse(correlationTokenValue, out var correlationToken))
			{
				correlationToken = Guid.NewGuid();
				logger.LogWarning(
					"Request without valid Correlation-Token: {method} - {path}. Generated new token: {correlationToken}",
					context.Request.Method,
					context.Request.Path,
					correlationToken);
			}

			context.Response.Headers[ConstantValues.CorrelationTokenHeaderName] = correlationToken.ToString();
			using (LogContext.PushProperty(ConstantValues.CorrelationTokenHeaderName, correlationToken.ToString()))
			{
				await next(context);
			}
		}
	}
}
