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

namespace Application.Models
{
	/// <summary>
	/// Represents parameters for pagination.
	/// </summary>
	public class PaginationParams
	{
		/// <summary>
		/// Gets or sets the page number.
		/// </summary>
		/// <example>1</example>
		public int PageNumber { get; set; } = 1;

		/// <summary>
		/// Gets or sets the page size.
		/// </summary>
		/// <example>10</example>
		public int PageSize { get; set; } = 10;
	}
}
