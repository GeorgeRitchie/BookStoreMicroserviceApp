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
	/// Represents the domain event that is raised when a Book is updated.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="BookId">A book identifier.</param>
	/// <param name="Title">A book title.</param>
	/// <param name="Description">A book description.</param>
	/// <param name="ISBN">Then new book ISBN.</param>
	/// <param name="Language">A book language.</param>
	/// <param name="AgeRating">A book age rating.</param>
	/// <param name="Authors">A book authors information.</param>
	/// <param name="Categories">A book categories.</param>
	/// <param name="Publisher">A book publisher information.</param>
	/// <param name="PublishedDate">A book published date.</param>
	/// <param name="Sources">A book sources.</param>
	public sealed record BookUpdatedDomainEvent(
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
		Publisher? Publisher,
		DateOnly? PublishedDate,
		IEnumerable<BookSource> Sources)
		: DomainEvent(Id, OccurredOnUtc);
}
