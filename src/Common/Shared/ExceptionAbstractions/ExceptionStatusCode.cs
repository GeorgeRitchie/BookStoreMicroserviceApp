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

using Domain.Primitives;

namespace Shared.ExceptionAbstractions
{
	/// <summary>
	/// Represents a base enumeration class for exception status codes enums.
	/// </summary>
	/// <inheritdoc/>
	public abstract class ExceptionStatusCode : Enumeration<ExceptionStatusCode, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionStatusCode"/> class with a name and value.
		/// </summary>
		/// <inheritdoc/>
		protected ExceptionStatusCode(string name, string value) : base(name, value)
		{
		}

		/// <inheritdoc/>
		protected sealed override bool IsValueEqual(string? first, string? second)
		{
			return first == second;
		}
	}
}
