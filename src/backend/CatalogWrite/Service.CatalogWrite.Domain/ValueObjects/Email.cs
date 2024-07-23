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
	/// Represents the Email value object.
	/// </summary>
	public sealed class Email : ValueObject
	{
		private const string pattern =
				@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
				+ @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

		/// <summary>
		/// Gets the email address.
		/// </summary>
		public string EmailAddress { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Email"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Email()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Email"/> class.
		/// </summary>
		/// <param name="emailAddress">The email address.</param>
		private Email(string emailAddress)
		{
			EmailAddress = emailAddress;
		}

		/// <inheritdoc/>
		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return EmailAddress;
		}

		/// <summary>
		/// Creates a new <see cref="Email"/> instance based on the specified parameters by applying validations.
		/// </summary>
		/// <param name="email">The email address.</param>
		/// <returns>The new <see cref="Email"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Email> Create(string email)
			=> Result.Success(email)
				.Ensure(e => string.IsNullOrWhiteSpace(e) == false, ValueObjectsErrors.Email.EmptyEmailAddress)
				.EnsureOnSuccess(e => Regex.IsMatch(e, pattern, RegexOptions.IgnoreCase), ValueObjectsErrors.Email.InvalidEmailAddress(email))
				.Bind(e => Result.Success(new Email(e)));
	}
}
