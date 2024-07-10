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
	/// Represents the result of an operation with success or failure status with error messages.
	/// </summary>
	public class Result
	{
		protected bool _status;
		protected readonly List<Error> _errors;

		/// <summary>
		/// Gets a value indicating whether the operation succeed.
		/// </summary>
		public bool IsSuccess => _status;

		/// <summary>
		/// Gets a value indicating whether the operation failed.
		/// </summary>
		public bool IsFailure => !IsSuccess;

		/// <summary>
		/// Gets an immutable collection of errors associated with the failure operation or empty collection for successful operation.
		/// </summary>
		public IReadOnlyCollection<Error> Errors => _errors;

		/// <summary>
		/// Initializes a new instance of the <see cref="Result"/> class.
		/// </summary>
		/// <param name="isSuccess">A flag indicating whether the operation succeed or failed.</param>
		/// <param name="error">An error message. Should be <see langword="null"/> for successful operations and not <see langword="null"/> for failure operations.</param>
		/// <exception cref="ArgumentException">Thrown when the combination of <paramref name="isSuccess"/> and <paramref name="error"/> is inappropriate.</exception>
		protected internal Result(bool isSuccess, Error? error)
		{
			if (isSuccess == true && error != Error.None)
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(error)}'");

			if (isSuccess == false && (error == null || error == Error.None))
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(error)}'");

			_status = isSuccess;
			_errors = isSuccess == true ? new() : new() { error! };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Result"/> class.
		/// </summary>
		/// <param name="isSuccess">A flag indicating whether the operation succeed or failed.</param>
		/// <param name="errors">A collection of error messages. Should be empty collection for successful operations and not empty collection for failure operations.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="errors"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when the combination of <paramref name="isSuccess"/> and <paramref name="errors"/> is inappropriate.</exception>
		protected internal Result(bool isSuccess, IEnumerable<Error> errors)
		{
			ArgumentNullException.ThrowIfNull(errors, nameof(errors));

			if (isSuccess == true && errors.Any() == true)
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(errors)}'");

			if (isSuccess == false && errors.Any() == false)
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(errors)}'");

			_status = isSuccess;
			_errors = errors.ToList();
		}

		/// <summary>
		/// Creates a new successful result.
		/// </summary>
		/// <returns>A successful result with no errors.</returns>
		public static Result Success() => new(true, Error.None);

		/// <summary>
		/// Creates a new successful <see cref="Result{TValue}"/> with the specified value.
		/// </summary>
		/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
		/// <param name="value">The value associated with the successful operation.</param>
		/// <returns>A successful <see cref="Result{TValue}"/> with the specified value.</returns>
		public static Result<TValue> Success<TValue>(TValue? value) => new(value, true, Error.None);

		/// <summary>
		/// Creates a new failed result with a default error message.
		/// </summary>
		/// <returns>A failed result with a default error message.</returns>
		public static Result Failure() => new(false, Error.Default);

		/// <summary>
		/// Creates a new failed <see cref="Result{TValue}"/> with the specified value and a default error message.
		/// </summary>
		/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
		/// <param name="value">The value associated with the failed operation.</param>
		/// <returns>A failed <see cref="Result{TValue}"/> with the specified value and a default error message.</returns>
		public static Result<TValue> Failure<TValue>(TValue? value) => new(value, false, Error.Default);

		/// <summary>
		/// Creates a new failed result with a specific error.
		/// </summary>
		/// <param name="error">The error associated with the failure.</param>
		/// <returns>A failed result with the specified error.</returns>
		public static Result Failure(Error error) => new(false, error);

		/// <summary>
		/// Creates a new failed <see cref="Result{TValue}"/> with the specified value and error message.
		/// </summary>
		/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
		/// <param name="value">The value associated with the failed operation.</param>
		/// <param name="error">The error message for the failed operation.</param>
		/// <returns>A failed <see cref="Result{TValue}"/> with the specified value and error message.</returns>
		public static Result<TValue> Failure<TValue>(TValue? value, Error error) => new(value, false, error);

		/// <summary>
		/// Creates a new failed <see cref="Result{TValue}"/> with the specified error message and default value for type <typeparamref name="TValue"/>.
		/// </summary>
		/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
		/// <param name="error">The error message for the failed operation.</param>
		/// <returns>A failed <see cref="Result{TValue}"/> with the specified error message and default value for type <typeparamref name="TValue"/>.</returns>
		public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

		/// <summary>
		/// Creates a new failed result with multiple errors.
		/// </summary>
		/// <param name="errors">A collection of errors associated with the failure.</param>
		/// <returns>A failed result with the specified errors.</returns>
		public static Result Failure(IEnumerable<Error> errors) => new(false, errors);

		/// <summary>
		/// Creates a new failed <see cref="Result{TValue}"/> with the specified value and a collection of error messages.
		/// </summary>
		/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
		/// <param name="errors">A collection of error messages for the failed operation.</param>
		/// <returns>A failed <see cref="Result{TValue}"/> with the specified value and a collection of error messages.</returns>
		public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors) => new(default, false, errors);

		/// <summary>
		/// Creates a new failed <see cref="Result{TValue}"/> with the specified value and a collection of error messages.
		/// </summary>
		/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
		/// <param name="value">The value associated with the failed operation.</param>
		/// <param name="errors">A collection of error messages for the failed operation.</param>
		/// <returns>A failed <see cref="Result{TValue}"/> with the specified value and a collection of error messages.</returns>
		public static Result<TValue> Failure<TValue>(TValue? value, IEnumerable<Error> errors) => new(value, false, errors);

		/// <summary>
		/// Returns a success <see cref="Result"/>.
		/// </summary>
		/// <param name="condition">The condition.</param>
		/// <returns>A new instance of <see cref="Result"/>.</returns>
		public static Result Create(bool condition) => condition ? Success() : Failure(Error.ConditionNotMet);

		/// <summary>
		/// Creates a new <see cref="Result{TValue}"/> with the specified nullable value and the specified error.
		/// </summary>
		/// <typeparam name="TValue">The result type.</typeparam>
		/// <param name="value">The result value.</param>
		/// <returns>A new instance of <see cref="Result{TValue}"/> with the specified value or an error.</returns>
		public static Result<TValue> Create<TValue>(TValue? value) => value is not null
																				? Success(value)
																				: Failure<TValue>(Error.NullValue);

		/// <summary>
		/// Ensures that the operation satisfies a given predicate, and if not, adds the specified error and makes result status failed.
		/// </summary>
		/// <param name="predicate">The predicate to be satisfied for the operation to be considered successful.</param>
		/// <param name="error">The error message to add if the predicate is not satisfied.</param>
		/// <returns>The current instance of <see cref="Result"/> with status satisfied to predicate result.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> or <paramref name="error"/> is <see langword="null"/>.</exception>
		public Result Ensure(Func<bool> predicate, Error error)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			ArgumentNullException.ThrowIfNull(error);

			if (predicate.Invoke() == false)
			{
				_status = false;
				_errors.Add(error);
			}

			return this;
		}

		/// <summary>
		/// Ensures that the successful operation satisfies a given predicate, and if not, adds the specified error and makes result status failed.
		/// Skips predicate for failure operation.
		/// </summary>
		/// <param name="predicate">The predicate to be satisfied for the operation to be considered successful.</param>
		/// <param name="error">The error message to add if the predicate is not satisfied.</param>
		/// <returns>The current instance of <see cref="Result"/> with status satisfied to predicate result.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> or <paramref name="error"/> is <see langword="null"/>.</exception>
		public Result EnsureOnSuccess(Func<bool> predicate, Error error)
		{
			if (IsSuccess)
				return Ensure(predicate, error);
			else
				return this;
		}

		/// <summary>
		/// Returns the first failure from the specified <paramref name="results"/>.
		/// If there is no failure, a success is returned.
		/// </summary>
		/// <param name="results">The results array.</param>
		/// <returns>
		/// The first failure from the specified <paramref name="results"/> array,or a success if it does not exist.
		/// </returns>
		public static async Task<Result> FirstFailureOrSuccess(params Func<Task<Result>>[] results)
		{
			foreach (Func<Task<Result>> resultTask in results)
			{
				Result result = await resultTask();

				if (result.IsFailure)
				{
					return result;
				}
			}

			return Success();
		}
	}
}
