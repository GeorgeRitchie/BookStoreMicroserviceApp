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

namespace Service.CatalogWrite.Application.Common.Services
{
	/// <summary>
	/// Represents the abstraction to manage files.
	/// </summary>
	public interface IFileManager
	{
		/// <summary>
		/// Checks if the file is photo (e. g. file extension is .png, .jpg, .jpeg, etc.).
		/// </summary>
		/// <param name="file">The file to validate.</param>
		/// <returns><see langword="true"/> if photo is an image, otherwise <see langword="false"/>.</returns>
		bool IsPhoto(IFile? file);

		/// <summary>
		/// Saves the file and returns the string source to get to file (e. g. url, full or relative path).
		/// </summary>
		/// <param name="file">The file to save.</param>
		/// <param name="subfolder">The subfolder used in directory hierarchy.</param>
		/// <param name="prefix">The prefix to add to file name.</param>
		/// <param name="cancellationToken">The cancelation token.</param>
		/// <returns>The source to saved file.</returns>
		Task<string> SaveAsync(IFile file, string? subfolder = null, string prefix = "", CancellationToken cancellationToken = default);

		/// <summary>
		/// Deletes the file by provided source.
		/// </summary>
		/// <param name="source">The source to file.</param>
		/// <param name="cancellationToken">The cancelation token.</param>
		/// <returns>The deletion status.</returns>
		Task<bool> DeleteAsync(string source, CancellationToken cancellationToken = default);
	}
}
