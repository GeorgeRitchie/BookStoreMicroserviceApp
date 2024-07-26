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

namespace Service.CatalogWrite.Endpoints.Contracts.Books
{
	/// <summary>
	/// Represents the request to set images to the specified book.
	/// </summary>
	public sealed class SetBookImageRequest
	{
		/// <summary>
		/// Book identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Book icon or <see langword="null"/> if no change required.
		/// </summary>
		public IFormFile? Icon { get; set; }

		/// <summary>
		/// Book cover photo or <see langword="null"/> if no change required.
		/// </summary>
		public IFormFile? Cover { get; set; }

		/// <summary>
		/// Book preview images or <see langword="null"/> if no change required.
		/// </summary>
		public IEnumerable<IFormFile>? Previews { get; set; }
	}
}
