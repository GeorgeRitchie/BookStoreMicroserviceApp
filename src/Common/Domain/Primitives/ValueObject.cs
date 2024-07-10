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
	/// Represents an abstract base class for value objects in the system, providing common functionality for all value objects types.
	/// </summary>
	/// <remarks>
	/// Value objects are immutable, self-contained objects that do not have an identity. They are used to represent
	/// attributes or components of entities and are compared based on the equality of their atomic values.
	/// </remarks>
	public abstract class ValueObject : IEquatable<ValueObject>
	{
		/// <summary>
		/// Gets the atomic values that compose the value object.
		/// </summary>
		/// <returns>An enumerable of atomic values that make up the value object.</returns>
		protected abstract IEnumerable<object> GetAtomicValues();

		/// <summary>
		/// Determines whether two value objects are equal by comparing their atomic values.
		/// </summary>
		/// <param name="first">The first value object to compare.</param>
		/// <param name="second">The second value object to compare.</param>
		/// <returns><see langword="true"/> if the value objects are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(ValueObject? first, ValueObject? second)
		{
			if (first is null && second is null)
				return true;

			if (first is null || second is null)
				return false;

			return first.Equals(second);
		}

		/// <summary>
		/// Determines whether two value objects are not equal by comparing their atomic values.
		/// </summary>
		/// <param name="first">The first value object to compare.</param>
		/// <param name="second">The second value object to compare.</param>
		/// <returns><see langword="true"/> if the value objects are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(ValueObject? first, ValueObject? second)
		{
			return !(first == second);
		}

		/// <summary>
		/// Determines whether the current value object is equal to another object by comparing their atomic values.
		/// </summary>
		/// <param name="obj">The object to compare with the current value object.</param>
		/// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/>.</returns>
		public override bool Equals(object? obj)
		{
			return obj is ValueObject other && ValuesAreEqual(other);
		}

		/// <summary>
		/// Calculates a hash code for the value object based on its atomic values.
		/// </summary>
		/// <returns>A hash code for the value object.</returns>
		public override int GetHashCode()
		{
			return GetAtomicValues().Aggregate(default(int), HashCode.Combine);
		}

		/// <summary>
		/// Determines whether the current value object is equal to another value object by comparing their atomic values.
		/// </summary>
		/// <param name="other">The value object to compare with the current value object.</param>
		/// <returns><see langword="true"/> if the value objects are equal; otherwise, <see langword="false"/>.</returns>
		public virtual bool Equals(ValueObject? other)
		{
			return other is not null && ValuesAreEqual(other);
		}

		private bool ValuesAreEqual(ValueObject other) => GetAtomicValues().SequenceEqual(other.GetAtomicValues());
	}
}
