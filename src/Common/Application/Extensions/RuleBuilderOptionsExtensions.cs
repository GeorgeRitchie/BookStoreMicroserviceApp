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

using FluentValidation;
using Shared.Errors;

namespace Application.Extensions
{
	/// <summary>
	/// Contains extension methods for the <see cref="IRuleBuilderOptions{T,TProperty}"/> class.
	/// </summary>
	public static class RuleBuilderOptionsExtensions
	{
		/// <summary>
		/// Specifies a custom validation error to use if validation fails.
		/// </summary>
		/// <typeparam name="T">The validated type.</typeparam>
		/// <typeparam name="TProperty">The type of the property being validated.</typeparam>
		/// <param name="rule">The rule.</param>
		/// <param name="error">The validation error.</param>
		/// <returns>The same rule, to allow for multiple calls to be chained.</returns>
		public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Error error) =>
			rule.WithErrorCode(error.Code).WithMessage(error.Message);
	}
}
