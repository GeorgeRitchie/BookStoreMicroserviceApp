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
	/// Represents the abstract entity primitive.
	/// </summary>
	/// <typeparam name="TEntityId">The entity identifier type.</typeparam>
	public abstract class Entity<TEntityId> : IEquatable<Entity<TEntityId>>, IEntity, IBaseClass<TEntityId>
		where TEntityId : class, IEntityId
	{
		private readonly List<IDomainEvent> _domainEvents = [];

		/// <summary>
		/// Gets the entity identifier.
		/// </summary>
		public TEntityId Id { get; private init; }

		/// <inheritdoc/>
		public bool IsDeleted { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
		/// </summary>
		/// <param name="id">The entity identifier.</param>
		protected Entity(TEntityId id, bool isDeleted = false)
			: this()
		{
			Id = id ?? throw new ArgumentException("The entity identifier is required.", nameof(id));
			IsDeleted = isDeleted;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Entity{TEntityId}"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		protected Entity()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}

		public static bool operator ==(Entity<TEntityId>? a, Entity<TEntityId>? b)
		{
			if (a is null && b is null)
				return true;

			if (a is null || b is null)
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(Entity<TEntityId>? a, Entity<TEntityId>? b) => !(a == b);

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return obj is Entity<TEntityId> entity && Equals(entity);
		}

		/// <inheritdoc />
		public override int GetHashCode() => Id.GetHashCode() * 41;

		/// <inheritdoc />
		public virtual bool Equals(Entity<TEntityId>? other)
		{
			if (other is null)
				return false;

			if (other.GetType() != GetType())
				return false;

			return ReferenceEquals(this, other) || Id == other.Id;
		}

		/// <summary>
		/// Raises the specified domain event.
		/// </summary>
		/// <param name="domainEvent">The domain event.</param>
		protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

		/// <inheritdoc />
		public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

		/// <inheritdoc />
		public void ClearDomainEvents() => _domainEvents.Clear();

		/// <inheritdoc/>
		public virtual void MarkAsDeleted()
		{
			IsDeleted = true;
		}

		/// <inheritdoc/>
		public virtual void RestoreDeleted()
		{
			IsDeleted = false;
		}
	}
}
