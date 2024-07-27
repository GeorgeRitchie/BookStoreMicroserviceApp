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

namespace Service.CatalogWrite.Application.Authors.Commands.CreateAuthor
{
	/// <summary>
	/// Represents the command for creating a new author.
	/// </summary>
	public sealed class CreateAuthorCommand : ICommand<Guid>
	{
		/// <summary>
		/// Author first name.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Author last name.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Author description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Author email.
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// Author web site.
		/// </summary>
		public string? Site { get; set; }
	}
}
