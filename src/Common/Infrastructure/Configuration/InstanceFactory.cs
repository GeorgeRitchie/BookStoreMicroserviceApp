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

using System.Reflection;

namespace Infrastructure.Configuration
{
	/// <summary>
	/// Represents the instance factory.
	/// </summary>
	internal static class InstanceFactory
	{
		/// <summary>
		/// Creates the instances of the specified type defined in the specified assemblies.
		/// </summary>
		/// <typeparam name="T">The type to instantiate.</typeparam>
		/// <param name="assemblies">The assemblies to scan for the instance of the specified type.</param>
		/// <returns>The enumerable collection of the instances of the specified type defined in the specified assemblies.</returns>
		internal static IEnumerable<T> CreateFromAssemblies<T>(params Assembly[] assemblies) =>
			assemblies
				.SelectMany(assembly => assembly.DefinedTypes)
				.Where(IsAssignableToType<T>)
				.Select(Activator.CreateInstance)
				.Cast<T>();

		private static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
			typeof(T).IsAssignableFrom(typeInfo) && !typeInfo.IsInterface && !typeInfo.IsAbstract;
	}
}
