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

namespace Service.Catalog.Application.BooSources.Commands.UpdateBookSource
{
	/// <summary>
	/// Represents the command for updating the specified book source.
	/// </summary>
	public sealed class UpdateBookSourceCommand : ICommand
	{
		/// <summary>
		/// The book source identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// New quantity or <see langword="null"/> if no change required.
		/// </summary>
		public uint? Quantity { get; set; }

		/// <summary>
		/// New price or <see langword="null"/> if no change required.
		/// </summary>
		public decimal? Price { get; set; }

		/// <summary>
		/// New url or <see langword="null"/> if no change required.
		/// </summary>
		public string? Url { get; set; }

		/// <summary>
		/// New preview url or <see langword="null"/> if no change required.
		/// </summary>
		public string? PreviewUrl { get; set; }
	}
}
