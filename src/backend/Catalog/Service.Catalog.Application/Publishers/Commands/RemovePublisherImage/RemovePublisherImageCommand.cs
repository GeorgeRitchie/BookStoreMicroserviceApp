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

using Service.Catalog.Domain.ImageSources;
using Service.Catalog.Domain.Publishers;

namespace Service.Catalog.Application.Publishers.Commands.RemovePublisherImage
{
	/// <summary>
	/// Represents command to remove specified images from specified publisher.
	/// </summary>
	public sealed class RemovePublisherImageCommand : ICommand
	{
		/// <summary>
		/// Publisher identifier.
		/// </summary>
		public PublisherId PublisherId { get; set; }

		/// <summary>
		/// List of images' ids to remove from publisher.
		/// </summary>
		public List<ImageSourceId>? ImageIds { get; set; }
	}
}
