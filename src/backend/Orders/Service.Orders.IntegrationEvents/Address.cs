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

namespace Service.Orders.IntegrationEvents
{
	/// <summary>
	/// Represents the address to deliver the purchased items.
	/// </summary>
	/// <param name="Country">The delivery address country.</param>
	/// <param name="Region">The delivery address region.</param>
	/// <param name="District">The delivery address district.</param>
	/// <param name="City">The delivery address city.</param>
	/// <param name="Street">The delivery address street.</param>
	/// <param name="Home">The delivery address home.</param>
	public sealed record DeliveryAddress(
		string Country,
		string Region,
		string District,
		string City,
		string Street,
		string Home);
}
