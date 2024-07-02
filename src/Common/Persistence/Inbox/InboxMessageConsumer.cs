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

namespace Persistence.Inbox
{
	/// <summary>
	/// Represents the inbox message consumer.
	/// </summary>
	public sealed class InboxMessageConsumer
	{
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		public Guid Id { get; private set; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		public string Name { get; private set; } = string.Empty;
	}
}
