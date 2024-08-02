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

using Service.Catalog.Domain.Books;

namespace Service.Catalog.Domain.BookSources.Events
{
	/// <summary>
	/// Represents the domain event that is raised when a new BookSource is created.
	/// </summary>
	/// <param name="Id">The event identifier.</param>
	/// <param name="OccurredOnUtc">The event occurred date and time.</param>
	/// <param name="BookSourceId">The new book source identifier.</param>
	/// <param name="Format">The new book source format.</param>
	/// <param name="Quantity">The new paper typed book source quantity.</param>
	/// <param name="Price">The new book source price.</param>
	/// <param name="Url">The new e-book source url.</param>
	/// <param name="PreviewUrl">The new book source preview url.</param>
	/// <param name="BookId">The book entity identifier.</param>
	public sealed record BookSourceCreatedDomainEvent(
		Guid Id,
		DateTime OccurredOnUtc,
		BookSourceId BookSourceId,
		string Format,
		uint? Quantity,
		decimal Price,
		string? Url,
		string? PreviewUrl,
		BookId BookId)
		: DomainEvent(Id, OccurredOnUtc);
}
