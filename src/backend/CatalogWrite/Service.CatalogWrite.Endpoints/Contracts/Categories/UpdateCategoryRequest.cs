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

namespace Service.CatalogWrite.Endpoints.Contracts.Categories
{
	/// <summary>
	/// Represents the request to update the specified category.
	/// </summary>
	public sealed class UpdateCategoryRequest
	{
		/// <summary>
		/// Category identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Category title. (Keep <see langword="null"/> for no changes).
		/// </summary>
		public string? Title { get; set; }

		/// <summary>
		/// Category description. (Keep <see langword="null"/> for no changes).
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// Category icon image file. (Keep <see langword="null"/> for no changes).
		/// </summary>
		public IFormFile? Icon { get; set; }
	}
}