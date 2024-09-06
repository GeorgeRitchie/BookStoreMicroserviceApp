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

using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Service.Identity.ServiceInstallers.IDS
{
	public static class IdsConfig
	{
		// TODO add CRUD UI for IdentityServer.IdentityResource class
		public static IEnumerable<IdentityResource> IdentityResources =>
			[
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email(),
				new IdentityResources.Address(),
				new IdentityResources.Phone(),
			];

		// TODO add CRUD UI for IdentityServer.ApiScope class
		public static IEnumerable<ApiScope> ApiScopes =>
			[
				new ApiScope("common_scope", "Common scope", [ "role", "name" ]),
			];

		// TODO add CRUD UI for IdentityServer.ApiResource class
		public static IEnumerable<ApiResource> ApiResources =>
			[
				new ApiResource("book_program", "Book program")
				{
					Scopes = { "common_scope" }
				},
			];

		// TODO add CRUD UI for IdentityServer.Client class
		public static IEnumerable<Client> Clients =>
			[
				new Client
				{
					ClientId = "swagger-client-3F9610DD-0032-41FA-92F5-397E6B66AE15",
					ClientName = "Swagger UI",

					RequireClientSecret = true,
					ClientSecrets = { new Secret("swagger-ui-DF669678-66B8-4982-890A-E52F7632A3BA".Sha256()) },

					Enabled = true,

					AllowedGrantTypes = GrantTypes.Code,

					RedirectUris = {
						"https://localhost:7133/swagger/oauth2-redirect.html",
						"https://localhost:7145/swagger/oauth2-redirect.html",
						"https://localhost:7101/swagger/oauth2-redirect.html",
						"https://localhost:7115/swagger/oauth2-redirect.html",
						"https://localhost:7202/swagger/oauth2-redirect.html",
						"https://localhost:7048/swagger/oauth2-redirect.html",

						"http://localhost:5142/swagger/oauth2-redirect.html",
						"http://localhost:5084/swagger/oauth2-redirect.html",
						"http://localhost:5081/swagger/oauth2-redirect.html",
						"http://localhost:5162/swagger/oauth2-redirect.html",
						"http://localhost:5105/swagger/oauth2-redirect.html",
						"http://localhost:5202/swagger/oauth2-redirect.html",

					},
					PostLogoutRedirectUris = {
						"https://localhost:7133/swagger/signout-callback-oidc",
						"https://localhost:7145/swagger/signout-callback-oidc",
						"https://localhost:7101/swagger/signout-callback-oidc",
						"https://localhost:7115/swagger/signout-callback-oidc",
						"https://localhost:7202/swagger/signout-callback-oidc",
						"https://localhost:7048/swagger/signout-callback-oidc",

						"http://localhost:5142/swagger/signout-callback-oidc",
						"http://localhost:5084/swagger/signout-callback-oidc",
						"http://localhost:5081/swagger/signout-callback-oidc",
						"http://localhost:5162/swagger/signout-callback-oidc",
						"http://localhost:5105/swagger/signout-callback-oidc",
						"http://localhost:5202/swagger/signout-callback-oidc",
					},

					AllowedScopes =
					[
						"common_scope",
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
					],

					AllowAccessTokensViaBrowser = true,

					AlwaysIncludeUserClaimsInIdToken = true,

					AccessTokenLifetime = 60 * 60,

					AllowedCorsOrigins =
					{
						"https://localhost:7133",
						"https://localhost:7145",
						"https://localhost:7101",
						"https://localhost:7115",
						"https://localhost:7202",
						"https://localhost:7048",

						"http://localhost:5142",
						"http://localhost:5084",
						"http://localhost:5081",
						"http://localhost:5162",
						"http://localhost:5105",
						"http://localhost:5202",
					},

					RequireConsent = false,

					RequirePkce = true,
				}
			];
	}
}
