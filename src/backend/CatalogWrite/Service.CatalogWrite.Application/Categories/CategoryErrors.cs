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

using Service.CatalogWrite.Domain.Categories;

namespace Service.CatalogWrite.Application.Categories
{
	/// <summary>
	/// Contains the category errors.
	/// </summary>
	internal static class CategoryErrors
	{
		/// <summary>
		/// Gets property required error.
		/// </summary>
		/// <remarks>
		/// Property name is serialized to error with key 'Category.{PropName}IsRequired'
		/// and value 'The category {PropName} is required.'
		/// </remarks>
		internal static Func<string, Error> PropertyIsRequired
			=> propName => new($"Category.{propName}IsRequired", $"The category {propName} is required.");

		/// <summary>
		/// Gets invalid photo file error.
		/// </summary>
		internal static Error OnlyPhotoFileIsAllowed
			=> new("Category.OnlyPhotoFileIsAllowed", "Category icon require valid photo file.");

		/// <summary>
		/// Gets category create operation failed error.
		/// </summary>
		internal static Error CreateOperationFailed
			=> new("Category.CreateOperationFailed", "Category create operation failed.");

		/// <summary>
		/// Gets category not found error.
		/// </summary>
		internal static Func<CategoryId, Error> NotFound
			=> categoryId => new("Category.NotFound", $"Category with the identifier {categoryId.Value} was not found.");
	}
}
