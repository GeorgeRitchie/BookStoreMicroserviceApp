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

using Service.Catalog.Domain.Publishers;

namespace Service.Catalog.Application.Publishers.Commands.SetPublisherImage
{
	/// <summary>
	/// Represents the command for updating the specified publisher's images.
	/// </summary>
	public sealed class SetPublisherImageCommand : ICommand
	{
		/// <summary>
		/// The publisher identifier.
		/// </summary>
		public PublisherId Id { get; set; }

		/// <summary>
		/// Publisher profile icon or <see langword="null"/> if no change required.
		/// </summary>
		public IFile? Icon { get; set; }

		/// <summary>
		/// Publisher profile photo or <see langword="null"/> if no change required.
		/// </summary>
		public IFile? Photo { get; set; }

		/// <summary>
		/// Publisher other images or <see langword="null"/> if no change required.
		/// </summary>
		public IEnumerable<IFile>? Others { get; set; }
	}
}
