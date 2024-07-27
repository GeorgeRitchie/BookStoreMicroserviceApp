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

namespace Service.CatalogWrite.Application.BooSources.Commands.CreateBookSource
{
    /// <summary>
    /// Represents the command to create a new book source.
    /// </summary>
    public sealed class CreateBookSourceCommand : ICommand<Guid>
    {
        /// <summary>
        /// Book format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Books quantity in storage. (Used only for paper format book sources).
        /// </summary>
        public uint Quantity { get; set; }

        /// <summary>
        /// Book price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Book source url. For paper format book source url is <see langword="null"/>.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Book's preview source url of if available.
        /// </summary>
        public string? PreviewUrl { get; set; }

        /// <summary>
        /// Related book entity's identifier.
        /// </summary>
        public Guid BookId { get; set; }
    }
}
