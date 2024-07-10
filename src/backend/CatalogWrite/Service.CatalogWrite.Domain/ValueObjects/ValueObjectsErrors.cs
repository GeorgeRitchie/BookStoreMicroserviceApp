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

namespace Service.CatalogWrite.Domain.ValueObjects
{
	/// <summary>
	/// Contains Value Objects errors.
	/// </summary>
	public static class ValueObjectsErrors
	{
		/// <summary>
		/// Contains Email errors.
		/// </summary>
		public static class Email
		{
			/// <summary>
			/// Gets email <see langword="null"/>, <see cref="string.Empty"/> or white-space string error.
			/// </summary>
			public static Error EmptyEmailAddress => new(
				"VO.Email.EmptyEmailAddress",
				"Email address cannot be null, empty or white-space string.");

			/// <summary>
			/// Gets invalid email error.
			/// </summary>
			public static Func<string, Error> InvalidEmailAddress =>
				email => new("VO.Email.InvalidEmailAddress", $"The specified value is not valid email address: {email}.");
		}

		/// <summary>
		/// Contains Website errors.
		/// </summary>
		public static class Website
		{
			/// <summary>
			/// Gets website <see langword="null"/>, <see cref="string.Empty"/> or white-space string error.
			/// </summary>
			public static Error EmptyUrlAddress => new(
				"VO.Website.EmptyUrlAddress",
				"Url address cannot be null, empty or white-space string.");

			/// <summary>
			/// Gets invalid url error.
			/// </summary>
			public static Func<string, Error> InvalidUrlAddress =>
				url => new("VO.Website.InvalidUrlAddress", $"The specified value is not valid url address: {url}.");
		}
	}
}
