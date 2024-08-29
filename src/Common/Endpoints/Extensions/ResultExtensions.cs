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

using Microsoft.AspNetCore.Mvc;
using Shared.Results;

namespace Endpoints.Extensions
{
	/// <summary>
	/// Contains extension methods for working with the <see cref="Result"/> class.
	/// </summary>
	public static class ResultExtensions
	{
		/// <summary>
		/// Matches the result status and returns the result of either the on success or on failure functions.
		/// </summary>
		/// <param name="resultTask">The result task.</param>
		/// <param name="onSuccess">The on success function.</param>
		/// <param name="onFailure">The on failure function.</param>
		/// <returns>The action result based on the result status.</returns>
		public static async Task<ActionResult> Match(
			this Task<Result> resultTask,
			Func<ActionResult> onSuccess,
			Func<Result, ActionResult> onFailure)
		{
			Result result = await resultTask;

			return result.IsSuccess ? onSuccess() : onFailure(result);
		}

		/// <summary>
		/// Matches the result status and returns the result of either the on success or on failure functions.
		/// </summary>
		/// <typeparam name="TResult">The result type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="onSuccess">The on success function.</param>
		/// <param name="onFailure">The on failure function.</param>
		/// <returns>The action result based on the result status.</returns>
		public static async Task<ActionResult> Match<TResult>(
			this Task<Result<TResult>> resultTask,
			Func<TResult, ActionResult> onSuccess,
			Func<Result, ActionResult> onFailure)
		{
			Result<TResult> result = await resultTask;

			return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
		}
	}
}
