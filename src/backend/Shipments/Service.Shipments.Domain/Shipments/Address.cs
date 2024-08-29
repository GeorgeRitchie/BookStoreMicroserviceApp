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

namespace Service.Shipments.Domain.Shipments
{
	/// <summary>
	/// Represents the Shipment Address value object.
	/// </summary>
	public sealed class Address : ValueObject
	{
		/// <summary>
		/// Gets or sets the shipping address country.
		/// </summary>
		public string Country { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the shipping address region.
		/// </summary>
		public string Region { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the shipping address district.
		/// </summary>
		public string District { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the shipping address city.
		/// </summary>
		public string City { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the shipping address street.
		/// </summary>
		public string Street { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the shipping address home.
		/// </summary>
		public string Home { get; set; } = string.Empty;

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
	}
}
