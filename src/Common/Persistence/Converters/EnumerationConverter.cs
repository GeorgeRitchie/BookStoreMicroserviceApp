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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Converters
{
	/// <summary>
	/// Converts custom enumeration types implemented from <see cref="Enumeration{TEnum, TEnumValue}"/> to and from strings for database storage.
	/// </summary>
	/// <typeparam name="T">The enumeration type that implements <see cref="Enumeration{TEnum, TEnumValue}"/>.</typeparam>
	/// <typeparam name="K">The type of value of enumeration that implements <see cref="Enumeration{TEnum, TEnumValue}"/>.</typeparam>
	public sealed class EnumerationConverter<T, K> : ValueConverter<T, string> where T : Enumeration<T, K>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EnumerationConverter{T, K}"/>.
		/// </summary>
		public EnumerationConverter() : base(v => v.ToString(),
											 v => (T)typeof(T).GetMethod("FromName")!.Invoke(null, new object[] { v })!)
		{
		}
	}
}
