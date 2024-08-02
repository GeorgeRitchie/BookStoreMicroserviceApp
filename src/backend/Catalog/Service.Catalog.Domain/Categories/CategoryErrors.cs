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

namespace Service.Catalog.Domain.Categories
{
	/// <summary>
	/// Contains the Category errors.
	/// </summary>
	public static class CategoryErrors
	{
		/// <summary>
		/// Gets category's title required error.
		/// </summary>
		public static Error TitleIsRequired => new("Category.TitleIsRequired", "Category's title is required.");

		/// <summary>
		/// Gets category's icon required error.
		/// </summary>
		public static Error IconIsRequired => new("Category.IconIsRequired", "Category's icon is required.");
	}
}
