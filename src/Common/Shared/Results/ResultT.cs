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
	/// Represents the result of an operation with a return value of type <typeparamref name="TValue"/>.
	/// </summary>
	/// <typeparam name="TValue">The type of value associated with the result.</typeparam>
	public class Result<TValue> : Result
	{
		private readonly TValue? _value;

		/// <summary>
		/// Gets the value associated with the result.
		/// </summary>
		public TValue? Value => _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="Result{TValue}"/> class.
		/// </summary>
		/// <param name="value">The value associated with the result.</param>
		/// <inheritdoc cref="Result(bool, Error?)"/>
		protected internal Result(TValue? value, bool isSuccess, Error? error) : base(isSuccess, error)
		{
			_value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Result{TValue}"/> class.
		/// </summary>
		/// <param name="value">The value associated with the result.</param>
		/// <inheritdoc cref="Result(bool, IEnumerable{Error})"/>
		protected internal Result(TValue? value, bool isSuccess, IEnumerable<Error> errors) : base(isSuccess, errors)
		{
			_value = value;
		}

		/// <summary>
		/// Implicitly converts a nullable value of type <typeparamref name="TValue"/> to an <see cref="Result{TValue}"/>.
		/// If the value is <see langword="null"/>, a failure result is created; otherwise, a success result is created.
		/// </summary>
		/// <param name="value">The nullable value to be converted to a result.</param>
		/// <returns>An <see cref="Result{TValue}"/> representing the converted value.</returns>
		public static implicit operator Result<TValue>(TValue? value) => Create(value);

		/// <summary>
		/// Implicitly converts an <see cref="Result{TValue}"/> to a nullable value of type <typeparamref name="TValue"/>.
		/// </summary>
		/// <param name="result">The result to be converted to a nullable value.</param>
		/// <returns>The value associated with the result.</returns>
		public static implicit operator TValue?(Result<TValue> result) => result.Value;

		/// <summary>
		/// Ensures that the result satisfies a given predicate, and if not, adds the specified error and makes result status failed.
		/// </summary>
		/// <param name="predicate">The predicate to be satisfied for the result to be considered successful.</param>
		/// <param name="error">The error message to add if the predicate is not satisfied.</param>
		/// <returns>The current instance of <see cref="Result{TValue}"/> with status satisfied to predicate result.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> or <paramref name="error"/> is <see langword="null"/>.</exception>
		public Result<TValue> Ensure(Func<TValue?, bool> predicate, Error error)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			if (error == null) throw new ArgumentNullException(nameof(error));

			if (predicate.Invoke(_value) == false)
			{
				_status = false;
				_errors.Add(error);
			}

			return this;
		}

		/// <summary>
		/// Returns the first failure from the specified <paramref name="results"/>.
		/// If there is no failure, a success is returned.
		/// </summary>
		/// <param name="results">The results array.</param>
		/// <returns>
		/// The first failure from the specified <paramref name="results"/> array, or a success if it does not exist.
		/// </returns>
		public static async Task<Result<TValue>> FirstFailureOrSuccess(params Func<Task<Result<TValue>>>[] results)
		{
			Result<TValue>? result = null;

			foreach (Func<Task<Result<TValue>>> resultTask in results)
			{
				result = await resultTask();

				if (result.IsFailure)
				{
					return result;
				}
			}

			return result;
		}
	}
}
