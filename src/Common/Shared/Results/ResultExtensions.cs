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

using Shared.Errors;

namespace Shared.Results
{
	/// <summary>
	/// Contains extension methods for working with the <see cref="Result"/> class.
	/// </summary>
	public static class ResultExtensions
	{
		/// <summary>
		/// Maps the success result based on the specified mapping function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The mapping function.</param>
		/// <returns>The mapped result.</returns>
		public static Result<TOut> Map<TOut>(this Result result, Func<TOut> func) =>
			result.IsSuccess ? func() : Result.Failure<TOut>(result.Errors);

		/// <summary>
		/// Maps the success result based on the specified mapping function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The mapping function.</param>
		/// <returns>The mapped result.</returns>
		public static async Task<Result<TOut>> Map<TOut>(this Task<Result> resultTask, Func<TOut> func) =>
			(await resultTask).Map(func);

		/// <summary>
		/// Maps the success result based on the specified mapping function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The mapping function.</param>
		/// <returns>The mapped result.</returns>
		public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func) =>
			result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(result.Errors);

		/// <summary>
		/// Maps the success result based on the specified mapping function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The mapping function.</param>
		/// <returns>The mapped result.</returns>
		public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> func) =>
			(await resultTask).Map(func);

		/// <summary>
		/// Maps the failure result based on the specified error function, otherwise returns a success result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The error function.</param>
		/// <returns>The mapped result.</returns>
		public static Result<TIn> MapFailure<TIn>(this Result<TIn> result, Func<Error> func) =>
			result.IsSuccess ? result : Result.Failure<TIn>(func());

		/// <summary>
		/// Maps the failure result based on the specified error function, otherwise returns a success result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The error function.</param>
		/// <returns>The mapped result.</returns>
		public static async Task<Result<TIn>> MapFailure<TIn>(this Task<Result<TIn>> resultTask, Func<Error> func) =>
			(await resultTask).MapFailure(func);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static Result Bind<TIn>(this Result<TIn> result, Func<TIn, Result> func) =>
			result.IsSuccess ? func(result.Value) : Result.Failure(result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result> Bind<TIn>(this Task<Result<TIn>> result, Func<TIn, Result> func) =>
			(await result).Bind(func);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func) =>
			result.IsSuccess ? await func(result.Value) : Result.Failure(result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func) =>
			result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func) =>
			result.IsSuccess ? await func(result.Value) : Result.Failure<TOut>(result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result<TOut>> Bind<TOut>(this Result result, Func<Task<Result<TOut>>> func) =>
			result.IsSuccess ? await func() : Result.Failure<TOut>(result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static Result<TOut> Bind<TOut>(this Result result, Func<Result<TOut>> func) =>
			result.IsSuccess ? func() : Result.Failure<TOut>(result.Errors);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
																Func<TIn, Result<TOut>> func) =>
			(await resultTask).Bind(func);

		/// <summary>
		/// Binds the success result based on the specified binding function, otherwise returns a failure result.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The binding function.</param>
		/// <returns>The bound result.</returns>
		public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
																Func<TIn,
																Task<Result<TOut>>> func) =>
			await (await resultTask).Bind(func);

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="func">The function.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result> Tap(this Result result, Func<Task> func)
		{
			if (result.IsSuccess)
			{
				await func();
			}

			return result;
		}

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The function.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result> Tap(this Task<Result> resultTask, Func<Task> func) =>
			await (await resultTask).Tap(func);

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static Result<TIn> Tap<TIn>(this Result<TIn> result, Action<TIn> action)
		{
			if (result.IsSuccess)
			{
				action(result.Value);
			}

			return result;
		}

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="func">The function.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask, Func<Task> func) =>
			await (await resultTask).Tap(func);

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask, Action<TIn> action) =>
			(await resultTask).Tap(action);

		/// <summary>
		/// Tap will execute the provided action if the result is successful.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="func">The function.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result<TIn>> Tap<TIn>(this Result<TIn> result, Func<Task> func)
		{
			if (result.IsSuccess)
			{
				await func();
			}

			return result;
		}

		/// <summary>
		/// On failure will execute the provided action if the result is a failure.
		/// </summary>
		/// <param name="resultTask">The result task.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result> OnFailure(this Task<Result> resultTask, Action<IEnumerable<Error>> action)
		{
			Result result = await resultTask;

			if (result.IsFailure)
			{
				action(result.Errors);
			}

			return result;
		}

		/// <summary>
		/// On failure will execute the provided action if the result is a failure.
		/// </summary>
		/// <param name="resultTask">The result task.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result> OnFailure(this Task<Result> resultTask, Func<IEnumerable<Error>, Task> action)
		{
			Result result = await resultTask;

			if (result.IsFailure)
			{
				await action(result.Errors);
			}

			return result;
		}

		/// <summary>
		/// On failure will execute the provided action if the result is a failure.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result<TIn>> OnFailure<TIn>(this Task<Result<TIn>> resultTask,
															Action<IEnumerable<Error>> action)
		{
			Result<TIn> result = await resultTask;

			if (result.IsFailure)
			{
				action(result.Errors);
			}

			return result;
		}

		/// <summary>
		/// On failure will execute the provided action if the result is a failure.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="resultTask">The result task.</param>
		/// <param name="action">The action.</param>
		/// <returns>The same result.</returns>
		public static async Task<Result<TIn>> OnFailure<TIn>(this Task<Result<TIn>> resultTask,
															Func<IEnumerable<Error>, Task> action)
		{
			Result<TIn> result = await resultTask;

			if (result.IsFailure)
			{
				await action(result.Errors);
			}

			return result;
		}

		/// <summary>
		/// Filter will return the success result if the specified predicate evaluates to true.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <param name="result">The result.</param>
		/// <param name="predicate">The predicate.</param>
		/// <returns>The same result if the specified predicate evaluates to true.</returns>
		public static Result<TIn> Filter<TIn>(this Result<TIn> result, Func<TIn, bool> predicate)
		{
			if (result.IsFailure)
			{
				return result;
			}

			return predicate(result.Value) ? result : Result.Failure<TIn>(Error.ConditionNotMet);
		}

		/// <summary>
		/// Converts a <see cref="Result"/> object to a <see cref="Task{TResult}"/> with the same <see cref="Result"/> type.
		/// This method facilitates asynchronous handling of a synchronous result, allowing it
		/// to be used in async-await operations without modification of the original result.
		/// </summary>
		/// <param name="result">The <see cref="Result"/> object to be converted into a <see cref="Task{TResult}"/>.</param>
		/// <returns>A <see cref="Task{TResult}"/> encapsulating the provided <see cref="Result"/> object.</returns>
		public static Task<Result> ToTask(this Result result) => Task.FromResult(result);

		/// <summary>
		/// Converts a generic <see cref="Result{T}"/> object to a <see cref="Task{TResult}"/> with the same <see cref="Result{T}"/> type.
		/// This method facilitates asynchronous handling of a synchronous result, allowing it
		/// to be used in async-await operations without modification of the original result.
		/// </summary>
		/// <typeparam name="TValue">The type of the value encapsulated by the <see cref="Result{T}"/> object.</typeparam>
		/// <param name="result">The generic <see cref="Result{T}"/> object to be converted into a <see cref="Task{TResult}"/>.</param>
		/// <returns>A <see cref="Task{TResult}"/> encapsulating the provided <see cref="Result{T}"/> object, containing a value of type <typeparamref name="TValue"/>.</returns>
		public static Task<Result<TValue?>> ToTask<TValue>(this Result<TValue?> result) => Task.FromResult(result);
	}
}
