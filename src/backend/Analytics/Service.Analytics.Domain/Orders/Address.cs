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

namespace Service.Analytics.Domain.Orders
{
	/// <summary>
	/// Represents the Shipment Address value object.
	/// </summary>
	public sealed class Address : ValueObject
	{
		/// <summary>
		/// Gets the shipping address country.
		/// </summary>
		public string Country { get; private set; }

		/// <summary>
		/// Gets the shipping address region.
		/// </summary>
		public string Region { get; private set; }

		/// <summary>
		/// Gets the shipping address district.
		/// </summary>
		public string District { get; private set; }

		/// <summary>
		/// Gets the shipping address city.
		/// </summary>
		public string City { get; private set; }

		/// <summary>
		/// Gets the shipping address street.
		/// </summary>
		public string Street { get; private set; }

		/// <summary>
		/// Gets the shipping address home.
		/// </summary>
		public string Home { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Address"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Address()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <inheritdoc/>
		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return Country;
			yield return Region;
			yield return District;
			yield return City;
			yield return Street;
			yield return Home;
		}

		/// <summary>
		/// Creates a new <see cref="Address"/> instance based on the specified parameters and applied validations result.
		/// </summary>
		/// <param name="country">Address country.</param>
		/// <param name="region">Address region.</param>
		/// <param name="district">Address district.</param>
		/// <param name="city">Address city.</param>
		/// <param name="street">Address street.</param>
		/// <param name="home">Address home.</param>
		/// <returns>The new <see cref="Address"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Address> Create(
			string country,
			string region,
			string district,
			string city,
			string street,
			string home)
			=> Result.Success()
				.Ensure(() => string.IsNullOrWhiteSpace(country) == false, AddressErrors.EmptyCountry())
				.Ensure(() => string.IsNullOrWhiteSpace(region) == false, AddressErrors.EmptyRegion())
				.Ensure(() => string.IsNullOrWhiteSpace(district) == false, AddressErrors.EmptyDistrict())
				.Ensure(() => string.IsNullOrWhiteSpace(city) == false, AddressErrors.EmptyCity())
				.Ensure(() => string.IsNullOrWhiteSpace(street) == false, AddressErrors.EmptyStreet())
				.Ensure(() => string.IsNullOrWhiteSpace(home) == false, AddressErrors.EmptyHome())
				.Map(() => new Address
				{
					Country = country,
					Region = region,
					District = district,
					City = city,
					Street = street,
					Home = home,
				});
	}
}
