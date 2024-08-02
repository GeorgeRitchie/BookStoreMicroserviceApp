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
	/// Represents a validation result of some operation, with the errors that occurred.
	/// </summary>
	/// <typeparam name="TValue">The result value type.</typeparam>
	public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationResult{TValue}"/> class.
		/// </summary>
		/// <param name="errors">The errors.</param>
		protected internal ValidationResult(Error[] errors)
			: base(default, false, errors)
		{
		}

		/// <summary>
		/// Creates a new <see cref="ValidationResult{TValue}"/> with the specified errors.
		/// </summary>
		/// <param name="errors">The errors.</param>
		/// <returns>The new validation result instance with the specified errors.</returns>
		public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
	}
}
