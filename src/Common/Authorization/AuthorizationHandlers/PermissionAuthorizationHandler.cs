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

using Authorization.Extensions;
using Authorization.Requirements;
using Authorization.Services;
using Microsoft.AspNetCore.Authorization;

namespace Authorization.AuthorizationHandlers
{
	/// <summary>
	/// Represents the permission authorization handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="PermissionAuthorizationHandler"/> class.
	/// </remarks>
	/// <param name="serviceScopeFactory">The service scope factory.</param>
	internal sealed class PermissionAuthorizationHandler(IPermissionService permissionService)
		: AuthorizationHandler<PermissionRequirement>
	{
		/// <inheritdoc />
		protected override async Task HandleRequirementAsync(
			AuthorizationHandlerContext context,
			PermissionRequirement requirement)
		{
			HashSet<string> permissions = await permissionService.GetPermissionsAsync(context.User.GetIdentityProviderId());

			if (permissions.Contains(requirement.Permission))
			{
				context.Succeed(requirement);
			}
		}
	}
}
