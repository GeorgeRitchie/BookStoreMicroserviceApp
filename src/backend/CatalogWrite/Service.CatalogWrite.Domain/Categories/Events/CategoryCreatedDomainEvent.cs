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

namespace Service.CatalogWrite.Domain.Categories.Events
{
	/// <summary>
	/// Represents the domain event that is raised when a new category is created.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="CategoryId">The new category identifier.</param>
	/// <param name="Title">The new category title.</param>
	/// <param name="Description">The new category description.</param>
	/// <param name="Icon">The new category icon source.</param>
	public sealed record CategoryCreatedDomainEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		CategoryId CategoryId,
		string Title,
		string Description,
		KeyValuePair<string, string> Icon)
		: DomainEvent(Id, OccurredOnUtc);
}
