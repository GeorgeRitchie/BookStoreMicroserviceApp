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

using Application.EventBus;

namespace Service.Catalog.IntegrationEvents
{
	/// <summary>
	/// Represents the book created integration event.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="BookId">The new book identifier.</param>
	/// <param name="Title">The new book title.</param>
	/// <param name="Description">The new book description.</param>
	/// <param name="ISBN">Then new book ISBN.</param>
	/// <param name="Language">The new book language.</param>
	/// <param name="AgeRating">The new book age rating.</param>
	/// <param name="Authors">The new book authors' identifiers.</param>
	/// <param name="Categories">The new book categories' identifiers.</param>
	/// <param name="PublisherId">The new book publisher's identifier.</param>
	/// <param name="PublishedDate">The new book published date.</param>
	public sealed record BookCreatedIntegrationEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		Guid BookId,
		string Title,
		string Description,
		string? ISBN,
		string Language,
		uint AgeRating,
		IEnumerable<Guid> AuthorIds,
		IEnumerable<Guid> CategoryIds,
		Guid? PublisherId,
		DateOnly? PublishedDate) : IntegrationEvent(Id, OccurredOnUtc);
}
