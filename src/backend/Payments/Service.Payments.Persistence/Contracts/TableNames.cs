﻿/* 
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

namespace Service.Payments.Persistence.Contracts
{
	/// <summary>
	/// Represents the table names in current module.
	/// </summary>
	internal static class TableNames
	{
		/// <summary>
		/// The payments table.
		/// </summary>
		internal const string Payments = "payments";

		/// <summary>
		/// The purchase items table.
		/// </summary>
		internal const string PurchaseItems = "purchase_items";

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
