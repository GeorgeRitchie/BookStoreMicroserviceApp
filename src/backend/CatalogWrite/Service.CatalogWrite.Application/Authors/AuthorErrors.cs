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

namespace Service.CatalogWrite.Application.Authors
{
	/// <summary>
	/// Contains the author errors.
	/// </summary>
	internal static class AuthorErrors
	{
		/// <summary>
		/// Gets property required error.
		/// </summary>
		/// <remarks>
		/// Property name is serialized to error with key 'Author.{PropName}IsRequired'
		/// and value 'The author {PropName} is required.'
		/// </remarks>
		internal static Func<string, Error> PropertyIsRequired
			=> propName => new($"Author.{propName}IsRequired", $"The author {propName} is required.");

		/// <summary>
		/// Gets invalid photo file error. Requires file name.
		/// </summary>
		internal static Func<string, Error> OnlyPhotoFileIsAllowed
			=> fileName => new("Author.OnlyPhotoFileIsAllowed", $"Passed file '{fileName}' is not a valid image file.");

		/// <summary>
		/// Gets author create operation failed error.
		/// </summary>
		internal static Error CreateOperationFailed
			=> new("Author.CreateOperationFailed", "Author create operation failed.");

		/// <summary>
		/// Gets author update operation failed error.
		/// </summary>
		internal static Error UpdateOperationFailed
			=> new("Author.UpdateOperationFailed", "Author update operation failed.");

		/// <summary>
		/// Gets author not found error.
		/// </summary>
		internal static Func<AuthorId, Error> NotFound
			=> authorId => new("Author.NotFound", $"Author with the identifier {authorId.Value} was not found.");
	}
}
