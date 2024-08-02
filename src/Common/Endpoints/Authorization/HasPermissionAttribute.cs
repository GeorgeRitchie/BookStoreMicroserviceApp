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

using Microsoft.AspNetCore.Authorization;

namespace Endpoints.Authorization
{
	/// <summary>
	/// Specifies that the method that this attribute is applied to requires the specified permission.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class HasPermissionAttribute : AuthorizeAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HasPermissionAttribute"/> class.
		/// </summary>
		/// <param name="permission">The permission.</param>
		public HasPermissionAttribute(string permission)
			: base(permission) =>
			Permission = permission;

		/// <summary>
		/// Gets the permission.
		/// </summary>
		public string Permission { get; }
	}
}
