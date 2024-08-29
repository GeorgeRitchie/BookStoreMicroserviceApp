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

namespace Domain.Primitives
{
	/// <summary>
	/// Represents the abstract domain event primitive.
	/// </summary>
	public abstract record DomainEvent : IDomainEvent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DomainEvent"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="occurredOnUtc">The occurred on date and time.</param>
		protected DomainEvent(Guid id, DateTime occurredOnUtc)
			: this()
		{
			Id = id;
			OccurredOnUtc = occurredOnUtc;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DomainEvent"/> class.
		/// </summary>
		private DomainEvent()
		{
		}

		/// <inheritdoc />
		public Guid Id { get; private set; }

		/// <inheritdoc />
		public DateTime OccurredOnUtc { get; private set; }
	}
}
