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

using Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Authorization.AuthorizationPolicyProviders
{
	/// <summary>
	/// Represents the permission authorization policy provider.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="PermissionAuthorizationPolicyProvider"/> class.
	/// </remarks>
	/// <param name="options">The authorization options.</param>
	internal sealed class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
		: DefaultAuthorizationPolicyProvider(options)
	{
		/// <inheritdoc />
		public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
		{
			AuthorizationPolicy? authorizationPolicy = await base.GetPolicyAsync(policyName);

			if (authorizationPolicy is not null)
			{
				return authorizationPolicy;
			}

			return new AuthorizationPolicyBuilder()
				.AddRequirements(new PermissionRequirement(policyName))
				.Build();
		}
	}
}
