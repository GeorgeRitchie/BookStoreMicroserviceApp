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

using Service.Catalog.Domain.ValueObjects;

namespace Service.Catalog.Domain.Publishers.Events
{

	/// <summary>
	/// Represents the domain event that is raised when a publisher is updated.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred data and time.</param>
	/// <param name="PublisherId">A publisher identifier.</param>
	/// <param name="Name">A publisher name.</param>
	/// <param name="Address">A publisher location address.</param>
	/// <param name="City">A publisher location city.</param>
	/// <param name="Country">A publisher location country.</param>
	/// <param name="PhoneNumber">A publisher phone number.</param>
	/// <param name="Email">A publisher email address.</param>
	/// <param name="Website">A publisher website.</param>
	public sealed record PublisherUpdatedDomainEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		PublisherId PublisherId,
		string Name,
		string Address,
		string City,
		string Country,
		PhoneNumber? PhoneNumber,
		Email? Email,
		Website? Website)
		: DomainEvent(Id, OccurredOnUtc);
}
