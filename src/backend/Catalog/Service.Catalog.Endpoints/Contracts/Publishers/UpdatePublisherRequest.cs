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

namespace Service.Catalog.Endpoints.Contracts.Publishers
{
	/// <summary>
	/// Represents the request to update the specified publisher.
	/// </summary>
	public sealed class UpdatePublisherRequest
	{
		/// <summary>
		/// The publisher identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// New name or <see langword="null"/> if no change required.
		/// </summary>
		public string? Name { get; set; }

		/// <summary>
		/// New location address or <see langword="null"/> if no change required.
		/// </summary>
		public string? Address { get; set; }

		/// <summary>
		/// New location city or <see langword="null"/> if no change required.
		/// </summary>
		public string? City { get; set; }

		/// <summary>
		/// New location country or <see langword="null"/> if no change required.
		/// </summary>
		public string? Country { get; set; }

		/// <summary>
		/// New phone number, <see cref="string.Empty"/> to remove old one or <see langword="null"/> if no change required.
		/// </summary>
		public string? PhoneNumber { get; set; }

		/// <summary>
		/// New email, <see cref="string.Empty"/> to remove old one or <see langword="null"/> if no change required.
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// New website, <see cref="string.Empty"/> to remove old one or <see langword="null"/> if no change required.
		/// </summary>
		public string? Website { get; set; }
	}
}
