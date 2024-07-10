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

using Service.CatalogWrite.Domain.ValueObjects;

namespace Service.CatalogWrite.Domain.Authors.Events
{
	/// <summary>
	/// Represents the domain event that is raised when an author is updated.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="AuthorId">The author identifier.</param>
	/// <param name="FirstName">The author first name.</param>
	/// <param name="LastName">The author last name.</param>
	/// <param name="Description">The author information.</param>
	/// <param name="Email">The author email address.</param>
	/// <param name="Website">The author website address.</param>
	/// <param name="Images">The author images.</param>
	public sealed record AuthorUpdatedDomainEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		AuthorId AuthorId,
		string FirstName,
		string LastName,
		string Description,
		Email? Email,
		Website? Website)
		: DomainEvent(Id, OccurredOnUtc);
}
