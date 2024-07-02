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

namespace Persistence.Outbox
{
	/// <summary>
	/// Represents the outbox message.
	/// </summary>
	public sealed class OutboxMessage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OutboxMessage"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="occurredOnUtc">The occurred on date and time.</param>
		/// <param name="type">The type.</param>
		/// <param name="content">The content.</param>
		public OutboxMessage(Guid id, DateTime occurredOnUtc, string type, string content)
		{
			Id = id;
			OccurredOnUtc = occurredOnUtc;
			Content = content;
			Type = type;
		}

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		public Guid Id { get; private set; }

		/// <summary>
		/// Gets the occurred on date and time.
		/// </summary>
		public DateTime OccurredOnUtc { get; private set; }

		/// <summary>
		/// Gets the type.
		/// </summary>
		public string Type { get; private set; }

		/// <summary>
		/// Gets the content.
		/// </summary>
		public string Content { get; private set; }

		/// <summary>
		/// Gets the processed on date and time, if it exists.
		/// </summary>
		public DateTime? ProcessedOnUtc { get; private set; }

		/// <summary>
		/// Gets the error, if it exists.
		/// </summary>
		public string? Error { get; private set; }
	}
}
