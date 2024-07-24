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
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain.Publishers;

namespace Service.CatalogWrite.Domain.Books.Events
{
	/// <summary>
	/// Represents the domain event that is raised when a new Book is created.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="BookId">The new book identifier.</param>
	/// <param name="Title">The new book title.</param>
	/// <param name="Description">The new book description.</param>
	/// <param name="ISBN">Then new book ISBN.</param>
	/// <param name="Language">The new book language.</param>
	/// <param name="AgeRating">The new book age rating.</param>
	/// <param name="Authors">The new book authors information.</param>
	/// <param name="Categories">The new book categories.</param>
	/// <param name="PublisherId">The new book publisher's identifier.</param>
	/// <param name="PublishedDate">The new book published date.</param>
	public sealed record BookCreatedDomainEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		BookId BookId,
		string Title,
		string Description,
		string? ISBN,
		string Language,
		uint AgeRating,
		IEnumerable<Author> Authors,
		IEnumerable<Category> Categories,
		PublisherId? PublisherId,
		DateOnly? PublishedDate)
		: DomainEvent(Id, OccurredOnUtc);
}
