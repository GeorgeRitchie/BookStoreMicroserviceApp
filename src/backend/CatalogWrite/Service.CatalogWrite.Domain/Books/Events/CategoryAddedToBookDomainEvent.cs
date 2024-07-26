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

using Service.CatalogWrite.Domain.Categories;

namespace Service.CatalogWrite.Domain.Books.Events
{
	/// <summary>
	/// Represents the domain event that is raised when a Category added to a Book.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="BookId">A book identifier.</param>
	/// <param name="CategoryId">A category identifier.</param>
	public sealed record CategoryAddedToBookDomainEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		BookId BookId,
		CategoryId CategoryId)
		: DomainEvent(Id, OccurredOnUtc);
}
