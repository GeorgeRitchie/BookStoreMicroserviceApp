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

namespace Service.CatalogWrite.Application.Authors.Commands.UpdateAuthor
{
	/// <summary>
	/// Represents the command for updating the specified author.
	/// </summary>
	public sealed class UpdateAuthorCommand : ICommand
	{
		/// <summary>
		/// The author identifier.
		/// </summary>
		public AuthorId Id { get; set; }

		/// <summary>
		/// New first name or <see langword="null"/> if no change required.
		/// </summary>
		public string? FirstName { get; set; }

		/// <summary>
		/// New last name or <see langword="null"/> if no change required.
		/// </summary>
		public string? LastName { get; set; }

		/// <summary>
		/// New description or <see langword="null"/> if no change required.
		/// </summary>
		public string? Description { get; set; }

		/// <summary>
		/// New email, <see cref="string.Empty"/> to remove old one or <see langword="null"/> if no change required.
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// New website, <see cref="string.Empty"/> to remove old one or <see langword="null"/> if no change required.
		/// </summary>
		public string? Site { get; set; }
	}
}
