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

using System.Security.Claims;

namespace Endpoints.Extensions
{
	/// <summary>
	/// Contains extension methods for the <see cref="ClaimsPrincipal"/> class.
	/// </summary>
	public static class ClaimsPrincipalExtensions
	{
		/// <summary>
		/// Gets the identity provider identifier of the currently authenticated user.
		/// </summary>
		/// <param name="claimsPrincipal">The claims principal.</param>
		/// <returns>The identity provider identifier of the currently authenticated user if it exists, or an empty string.</returns>
		public static string GetIdentityProviderId(this ClaimsPrincipal claimsPrincipal) =>
			claimsPrincipal.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
	}
}
