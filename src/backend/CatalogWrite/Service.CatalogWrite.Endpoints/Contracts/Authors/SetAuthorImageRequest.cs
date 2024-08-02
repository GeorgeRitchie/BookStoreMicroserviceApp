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

namespace Service.Catalog.Endpoints.Contracts.Authors
{
	/// <summary>
	/// Represents the request to set images to the specified author.
	/// </summary>
	public sealed class SetAuthorImageRequest
	{
		/// <summary>
		/// Author identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Author profile icon or <see langword="null"/> if no change required.
		/// </summary>
		public IFormFile? Icon { get; set; }

		/// <summary>
		/// Author profile photo or <see langword="null"/> if no change required.
		/// </summary>
		public IFormFile? Photo { get; set; }

		/// <summary>
		/// Author other images or <see langword="null"/> if no change required.
		/// </summary>
		public IEnumerable<IFormFile>? OtherImages { get; set; }
	}
}
