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

using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Identity.Data;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Routing
{
	internal static class IdentityComponentsEndpointRouteBuilderExtensions
	{
		// These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
		public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
		{
			ArgumentNullException.ThrowIfNull(endpoints);

			var accountGroup = endpoints.MapGroup("/Account");

			accountGroup.MapPost("/Logout", async (
				ClaimsPrincipal user,
				SignInManager<User> signInManager,
				IIdentityServerInteractionService interactionService,
				[FromQuery] string? logoutId) =>
			{
				await signInManager.SignOutAsync();
				var logoutResult = await interactionService.GetLogoutContextAsync(logoutId);

				if (string.IsNullOrEmpty(logoutResult.PostLogoutRedirectUri))
				{
					return TypedResults.LocalRedirect($"~/");
				}

				return Results.Redirect(logoutResult.PostLogoutRedirectUri);
			});

			return accountGroup;
		}
	}
}
