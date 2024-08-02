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

using Service.Catalog.Domain.Categories;

namespace Service.Catalog.Application.Categories.Commands.UpdateCategory
{
	/// <summary>
	/// Represents the command for updating the specified category.
	/// </summary>
	public sealed class UpdateCategoryCommand : ICommand
	{
		/// <summary>
		/// The category identifier.
		/// </summary>
		public CategoryId Id { get; set; }

		/// <summary>
		/// New title or <see langword="null"/> if no change required.
		/// </summary>
		public string? Title { get; set; }

		/// <summary>
		/// New description or <see langword="null"/> if no change required.
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// New icon or <see langword="null"/> if no change required.
		/// </summary>
		public IFile? Icon { get; set; }
	}
}
