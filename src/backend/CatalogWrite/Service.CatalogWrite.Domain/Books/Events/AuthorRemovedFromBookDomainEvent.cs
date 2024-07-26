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

using Service.CatalogWrite.Domain.Authors;

namespace Service.CatalogWrite.Domain.Books.Events
{
	/// <summary>
	/// Represents the domain event that is raised when an Author removed from a Book.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="BookId">A book identifier.</param>
	/// <param name="AuthorId">An author identifier.</param>
	public sealed record AuthorRemovedFromBookDomainEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		BookId BookId,
		AuthorId AuthorId)
		: DomainEvent(Id, OccurredOnUtc);
}
