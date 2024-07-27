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

using Service.CatalogWrite.Domain.BookSources;

namespace Service.CatalogWrite.Application.BooSources.Queries.GetBookSourceById
{
	/// <summary>
	/// Represents a query to get book source by identifier.
	/// </summary>
	/// <param name="BooSourceId">The book source identifier.</param>
	/// <param name="IncludeUrl">The flag indicating whether include E-Book source url or not.</param>
	public sealed record GetBookSourceByIdQuery(BookSourceId BooSourceId, bool IncludeUrl = false)
		: IQuery<BookSourceDto>;
}
