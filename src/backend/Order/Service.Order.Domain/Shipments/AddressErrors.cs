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

namespace Service.Order.Domain.Shipments
{
	/// <summary>
	/// Contains the Shipment Address errors.
	/// </summary>
	public static class AddressErrors
	{
		/// <summary>
		/// Gets the invalid country error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error EmptyCountry()
			=> new("Address.EmptyCountry",
					"The shipping address country cannot be null, empty or white-space string.");

		/// <summary>
		/// Gets the invalid region error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error EmptyRegion()
			=> new("Address.EmptyRegion",
					"The shipping address region cannot be null, empty or white-space string.");

		/// <summary>
		/// Gets the invalid district error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error EmptyDistrict()
			=> new("Address.EmptyDistrict",
					"The shipping address district cannot be null, empty or white-space string.");

		/// <summary>
		/// Gets the invalid city error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error EmptyCity()
			=> new("Address.EmptyCity",
					"The shipping address city cannot be null, empty or white-space string.");

		/// <summary>
		/// Gets the invalid street error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error EmptyStreet()
			=> new("Address.EmptyStreet",
					"The shipping address street cannot be null, empty or white-space string.");

		/// <summary>
		/// Gets the invalid home error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error EmptyHome()
			=> new("Address.EmptyHome",
					"The shipping address home cannot be null, empty or white-space string.");
	}
}
