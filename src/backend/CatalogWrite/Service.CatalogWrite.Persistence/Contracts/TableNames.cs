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

namespace Service.Catalog.Persistence.Contracts
{
	/// <summary>
	/// Represents the table names in current module.
	/// </summary>
	internal static class TableNames
	{
		/// <summary>
		/// The books table.
		/// </summary>
		internal const string Books = "books";

		/// <summary>
		/// The categories table.
		/// </summary>
		internal const string Categories = "categories";

		/// <summary>
		/// The authors table.
		/// </summary>
		internal const string Authors = "authors";

		/// <summary>
		/// The publishers table.
		/// </summary>
		internal const string Publishers = "publishers";

		/// <summary>
		/// The book sources table.
		/// </summary>
		internal const string BookSources = "book_sources";

		/// <summary>
		/// The image sources table.
		/// </summary>
		internal const string ImageSources = "image_sources";

		/// <summary>
		/// The inbox messages table.
		/// </summary>
		internal const string InboxMessages = "inbox_messages";

		/// <summary>
		/// The inbox message consumers table.
		/// </summary>
		internal const string InboxMessageConsumers = "inbox_message_consumers";

		/// <summary>
		/// The outbox messages table.
		/// </summary>
		internal const string OutboxMessages = "outbox_messages";

		/// <summary>
		/// The outbox message consumers table.
		/// </summary>
		internal const string OutboxMessageConsumers = "outbox_message_consumers";
	}
}
