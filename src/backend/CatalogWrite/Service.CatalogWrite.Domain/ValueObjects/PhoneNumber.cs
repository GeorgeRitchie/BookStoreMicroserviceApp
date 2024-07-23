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

using System.Text.RegularExpressions;

namespace Service.CatalogWrite.Domain.ValueObjects
{
	/// <summary>
	/// Represents the Phone number value object.
	/// </summary>
	public sealed class PhoneNumber : ValueObject
	{
		private const string pattern = @"^(\+[0-9]{12})$";

		/// <summary>
		/// Gets the phone number.
		/// </summary>
		public string Number { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PhoneNumber"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private PhoneNumber()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PhoneNumber"/> class.
		/// </summary>
		/// <param name="number">The phone number.</param>
		private PhoneNumber(string number)
		{
			Number = number;
		}

		/// <inheritdoc/>
		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Number;
		}

		/// <summary>
		/// Creates a new <see cref="PhoneNumber"/> instance based on the specified parameters by applying validations.
		/// </summary>
		/// <param name="number">The phone number.</param>
		/// <returns>The new <see cref="PhoneNumber"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<PhoneNumber> Create(string number)
			=> Result.Success(number)
				.Ensure(n => string.IsNullOrWhiteSpace(n) == false, ValueObjectsErrors.PhoneNumber.EmptyPhoneNumber)
				.EnsureOnSuccess(n => Regex.IsMatch(n, pattern, RegexOptions.IgnoreCase), ValueObjectsErrors.PhoneNumber.InvalidPhoneNumber(number))
				.Bind(e => Result.Success(new PhoneNumber(e)));
	}
}
