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

namespace Authorization.Requirements
{
	/// <summary>
	/// Represents the permission authorization requirement.
	/// </summary>
	internal sealed class PermissionRequirement : IAuthorizationRequirement
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionRequirement"/> class.
		/// </summary>
		/// <param name="permission">The permission.</param>
		internal PermissionRequirement(string permission) => Permission = permission;

		/// <summary>
		/// Gets the permission.
		/// </summary>
		internal string Permission { get; }
	}
}
