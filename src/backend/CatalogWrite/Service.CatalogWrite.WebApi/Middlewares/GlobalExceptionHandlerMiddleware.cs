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

using Microsoft.AspNetCore.Mvc;
using Shared.ExceptionAbstractions;

namespace Service.Catalog.WebApi.Middlewares
{
	/// <summary>
	/// Contains extension methods for <see cref="GlobalExceptionHandlerMiddleware"/>.
	/// </summary>
	internal static class GlobalExceptionHandlerMiddlewareExtensions
	{
		/// <summary>
		/// Adds <see cref="GlobalExceptionHandlerMiddleware"/> to DI chain.
		/// </summary>
		/// <param name="builder">The application builder.</param>
		/// <returns>The application builder with <see cref="GlobalExceptionHandlerMiddleware"/> added.</returns>
		public static IApplicationBuilder UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder builder)
			=> builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
	}

	/// <summary>
	/// Represents the global exception handler to convert unhandled exceptions to correct http responses.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="GlobalExceptionHandlerMiddleware"/> class.
	/// </remarks>
	/// <param name="next">The next middleware to call.</param>
	/// <param name="logger">The logger.</param>
	internal class GlobalExceptionHandlerMiddleware(RequestDelegate next,
													ILogger<GlobalExceptionHandlerMiddleware> logger)
	{
		private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> exceptionHandlers = [];

		/// <summary>
		/// Method used by DI to invoke for user requests in middlewares chain.
		/// </summary>
		/// <param name="context">The http context of user request.</param>
		/// <returns>The task.</returns>
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			Type type = ex.GetType();

			if (exceptionHandlers.TryGetValue(type, out Func<HttpContext, Exception, Task>? value))
				await value.Invoke(context, ex);
			else if (ex is IPublicException pex)
				await DefaultPublicExceptionHandler(context, pex);
			else if (ex is IInternalException iex)
				await DefaultInternalExceptionHandler(context, iex);
			else
				await HandleUnknownException(context, ex);
		}

		#region Default exception handlers

		private async Task HandleUnknownException(HttpContext context, Exception ex)
		{
			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "Internal Server Error",
				Detail = "Internal server error. Connect to support team.",
				Instance = context.Request.Path
			};

			context.Response.ContentType = "application/problem+json";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsJsonAsync(problemDetails);

			logger.LogError(ex, "Unknown exception caught.");
		}

		private async Task DefaultInternalExceptionHandler(HttpContext context, IInternalException ex)
		{
			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "Internal Exception",
				Detail = ex.Message,
				Instance = context.Request.Path,
				Extensions = { { "Code", ex.StatusCode.Name } },
			};

			context.Response.ContentType = "application/problem+json";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await context.Response.WriteAsJsonAsync(problemDetails);

			logger.LogInformation((Exception)ex,
				"Internal exception with code '{code}' caught. Exception thrown in '{source}' with message '{message}'\nDescription: {description}.",
				ex.StatusCode,
				ex.Source,
				ex.Message,
				ex.Description);
		}

		private async Task DefaultPublicExceptionHandler(HttpContext context, IPublicException ex)
		{
			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status400BadRequest,
				Title = "Bad Request",
				Detail = ex.Message,
				Instance = context.Request.Path,
				Extensions =
				{
					{ "Code", ex.StatusCode.Name },
					{ "Description", ex.Description },
				},
			};

			context.Response.ContentType = "application/problem+json";
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
			await context.Response.WriteAsJsonAsync(problemDetails);

			logger.LogInformation((Exception)ex,
				"Public exception with code '{code}' caught. Exception thrown in '{source}' with message '{message}'\nDescription: {description}.",
				ex.StatusCode,
				ex.Source,
				ex.Message,
				ex.Description);
		}

		#endregion

		#region Registered exceptions handlers

		// TODO __##__ Add your custom exception handlers here, don't forget to add logging

		#endregion
	}
}
