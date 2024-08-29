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

using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace Shared.Helpers
{
	/// <summary>
	/// Custom JSON contract resolver that enables deserialization of properties with private setters.
	/// </summary>
	public sealed class PrivateSetterResolver : DefaultContractResolver
	{
		/// <summary>
		/// Overrides property creation to make properties with private setters writable.
		/// </summary>
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var prop = base.CreateProperty(member, memberSerialization);
			if (!prop.Writable)
			{
				var property = member as PropertyInfo;
				var hasPrivateSetter = property?.GetSetMethod(true) != null;
				prop.Writable = hasPrivateSetter;
			}
			return prop;
		}
	}
}
