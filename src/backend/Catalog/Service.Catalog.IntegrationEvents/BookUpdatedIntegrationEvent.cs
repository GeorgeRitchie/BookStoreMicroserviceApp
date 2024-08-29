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
	/// Represents the book updated integration event.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="BookId">A book identifier.</param>
	/// <param name="Title">A book title.</param>
	/// <param name="Description">A book description.</param>
	/// <param name="ISBN">Then new book ISBN.</param>
	/// <param name="Language">A book language.</param>
	/// <param name="AgeRating">A book age rating.</param>
	/// <param name="PublisherId">A book publisher's identifier.</param>
	/// <param name="PublishedDate">A book published date.</param>
	public sealed record BookUpdatedIntegrationEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		Guid BookId,
		string Title,
		string Description,
		string? ISBN,
		string Language,
		uint AgeRating,
		Guid? PublisherId,
		DateOnly? PublishedDate) : IntegrationEvent(Id, OccurredOnUtc);
}
