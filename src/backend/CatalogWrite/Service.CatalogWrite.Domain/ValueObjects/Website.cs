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
	/// Represents the Website value object.
	/// </summary>
	public sealed class Website : ValueObject
	{
		/// <summary>
		/// Gets the site url.
		/// </summary>
		public string Url { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Website"/> class.
		/// </summary>
		/// <param name="url">The site url.</param>
		private Website(string url)
		{
			Url = url;
		}

		/// <inheritdoc/>
		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Url;
		}

		/// <summary>
		/// Creates a new <see cref="Website"/> instance based on the specified parameters by applying validations.
		/// </summary>
		/// <param name="url">The website url.</param>
		/// <returns>The new <see cref="Website"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Website> Create(string url)
			=> Result.Success(url)
				.Ensure(u => string.IsNullOrWhiteSpace(u) == false, ValueObjectsErrors.Website.EmptyUrlAddress)
				.Ensure(IsValidUrl, ValueObjectsErrors.Website.InvalidUrlAddress(url))
				.Bind(u => Result.Success(new Website(u)));

		private static bool IsValidUrl(string? url)
			=> Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? resultUri)
				&& (resultUri.IsAbsoluteUri == false
					|| (resultUri.IsAbsoluteUri == true
						&& (resultUri.Scheme == Uri.UriSchemeHttp || resultUri.Scheme == Uri.UriSchemeHttps)));
	}
}
