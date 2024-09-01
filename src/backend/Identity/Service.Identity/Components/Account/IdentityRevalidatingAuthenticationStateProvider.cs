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

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Identity.Data;
using System.Security.Claims;

namespace Service.Identity.Components.Account
{
	// This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
	// every 30 minutes an interactive circuit is connected.
	internal sealed class IdentityRevalidatingAuthenticationStateProvider(
			ILoggerFactory loggerFactory,
			IServiceScopeFactory scopeFactory,
			IOptions<IdentityOptions> options)
		: RevalidatingServerAuthenticationStateProvider(loggerFactory)
	{
		protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

		protected override async Task<bool> ValidateAuthenticationStateAsync(
			AuthenticationState authenticationState, CancellationToken cancellationToken)
		{
			// Get the user manager from a new scope to ensure it fetches fresh data
			await using var scope = scopeFactory.CreateAsyncScope();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
			return await ValidateSecurityStampAsync(userManager, authenticationState.User);
		}

		private async Task<bool> ValidateSecurityStampAsync(UserManager<User> userManager, ClaimsPrincipal principal)
		{
			var user = await userManager.GetUserAsync(principal);
			if (user is null)
			{
				return false;
			}
			else if (!userManager.SupportsUserSecurityStamp)
			{
				return true;
			}
			else
			{
				var principalStamp = principal.FindFirstValue(options.Value.ClaimsIdentity.SecurityStampClaimType);
				var userStamp = await userManager.GetSecurityStampAsync(user);
				return principalStamp == userStamp;
			}
		}
	}
}
