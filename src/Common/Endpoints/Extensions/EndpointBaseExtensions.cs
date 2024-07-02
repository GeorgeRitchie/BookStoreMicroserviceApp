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

using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Errors;
using Shared.Results;

namespace Endpoints.Extensions
{
	/// <summary>
	/// Contains extension methods for the <see cref="EndpointBase"/> class.
	/// </summary>
	public static class EndpointBaseExtensions
	{
		private readonly static Error BadRequestError = new("BadRequest",
															"The request parameters were incorrect or incomplete.");

		/// <summary>
		/// Handles the failure result and returns the appropriate response.
		/// </summary>
		/// <param name="endpoint">The endpoint.</param>
		/// <param name="result">The failure result.</param>
		/// <returns>The appropriate response based on the result type.</returns>
		/// <exception cref="InvalidOperationException"> when this method is invoked with a success result.</exception>
		public static ActionResult HandleFailure(this EndpointBase endpoint, Result result) =>
			result switch
			{
				{ IsSuccess: true } =>
					throw new InvalidOperationException("This method can't be invoked for a success result."),
				IValidationResult validationResult =>
					endpoint.BadRequest(
						CreateProblemDetails(
							"Validation Error",
							StatusCodes.Status400BadRequest,
							IValidationResult.ValidationError,
							validationResult.Errors)),
				var notFoundResult when notFoundResult.Errors.Count == 1
										&& notFoundResult.Errors.ElementAt(0) is NotFoundError notFound =>
					endpoint.NotFound(CreateProblemDetails("Not Found", StatusCodes.Status404NotFound, notFound)),
				var conflictResult when conflictResult.Errors.Count == 1
										&& conflictResult.Errors.ElementAt(0) is ConflictError conflict =>
					endpoint.Conflict(CreateProblemDetails("Conflict", StatusCodes.Status409Conflict, conflict)),
				var badRequest =>
					endpoint.BadRequest(
						CreateProblemDetails(
							"Bad Request",
							StatusCodes.Status400BadRequest,
							BadRequestError,
							badRequest.Errors)),
			};

		private static ProblemDetails CreateProblemDetails(string title, int status, Error error, IEnumerable<Error>? errors = null) =>
			new()
			{
				Title = title,
				Type = error.Code,
				Detail = error.Message,
				Status = status,
				Extensions = { { nameof(errors), errors } }
			};
	}
}
