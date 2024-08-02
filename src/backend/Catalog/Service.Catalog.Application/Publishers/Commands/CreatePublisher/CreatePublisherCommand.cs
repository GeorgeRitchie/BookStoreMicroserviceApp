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

namespace Service.Catalog.Application.Publishers.Commands.CreatePublisher
{
	/// <summary>
	/// Represents the command to create a new publisher.
	/// </summary>
	public sealed class CreatePublisherCommand : ICommand<Guid>
	{
		/// <summary>
		/// Publisher name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Publisher location address.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Publisher location city.
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Publisher location country.
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// Publisher phone number.
		/// </summary>
		public string? PhoneNumber { get; set; }

		/// <summary>
		/// Publisher email address.
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// Publisher website.
		/// </summary>
		public string? Website { get; set; }
	}
}
