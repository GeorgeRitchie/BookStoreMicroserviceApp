﻿/* 
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

using Service.CatalogWrite.Domain.ValueObjects;

namespace Service.CatalogWrite.Domain.Publishers.Events
{
	/// <summary>
	/// Represents the domain event that is raised when a new publisher is created.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred data and time.</param>
	/// <param name="PublisherId">The new publisher identifier.</param>
	/// <param name="Name">The new publisher name.</param>
	/// <param name="Address">The new publisher location address.</param>
	/// <param name="City">The new publisher location city.</param>
	/// <param name="Country">The new publisher location country.</param>
	/// <param name="PhoneNumber">The new publisher phone number.</param>
	/// <param name="Email">The new publisher email address.</param>
	/// <param name="Website">The new publisher website.</param>
	public sealed record PublisherCreatedDomainEvent(
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
