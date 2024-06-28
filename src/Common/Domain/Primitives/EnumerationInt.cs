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
	// TODO __##__ This is an illustration of creating and utilizing strongly-typed enumerations with custom value types - int. Create your own strongly-typed enumerations with custom value types, add summary comments and create test for them if needed.

	/// <summary>
	/// Represents a base class for creating strongly-typed enumerations with integer values.
	/// </summary>
	/// <inheritdoc/>
	public abstract class Enumeration<TEnum> : Enumeration<TEnum, int> where TEnum : Enumeration<TEnum>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Enumeration{TEnum}"/> class with a name and value.
		/// </summary>
		/// <inheritdoc/>
		protected Enumeration(string name, int value) : base(name, value)
		{
		}

		/// <inheritdoc/>
		protected sealed override bool IsValueEqual(int first, int second)
		{
			return first == second;
		}
	}
}
