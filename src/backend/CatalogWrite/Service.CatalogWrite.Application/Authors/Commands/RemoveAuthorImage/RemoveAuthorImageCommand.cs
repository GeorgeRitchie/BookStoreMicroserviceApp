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
using Service.CatalogWrite.Domain.ImageSources;

namespace Service.CatalogWrite.Application.Authors.Commands.RemoveAuthorImage
{
	/// <summary>
	/// Represents command to remove specified images from specified author.
	/// </summary>
	public sealed class RemoveAuthorImageCommand : ICommand
	{
		/// <summary>
		/// Author identifier.
		/// </summary>
		public AuthorId AuthorId { get; set; }

		/// <summary>
		/// List of images' ids to remove from author.
		/// </summary>
		public List<ImageSourceId>? ImageIds { get; set; }
	}
}
