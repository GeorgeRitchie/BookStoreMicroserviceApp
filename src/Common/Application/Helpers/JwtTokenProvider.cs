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

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Helpers
{
	/// <summary>
	/// Provides methods for generating and working with JWT tokens.
	/// </summary>
	public static class JwtTokenProvider
	{
		/// <summary>
		/// Generates a JWT token as a string.
		/// </summary>
		/// <param name="tokenLifeTime_Min">The lifetime of the token in minutes.</param>
		/// <param name="jwtTokenOptions">The <see cref="JwtTokenOptions"/> instance for token configuration.</param>
		/// <param name="claims">Optional claims to include in the token.</param>
		/// <returns>The generated JWT token as a string.</returns>
		public static string GenerateJwtTokenAsString(int tokenLifeTime_Min, JwtTokenOptions jwtTokenOptions, IEnumerable<Claim>? claims = null)
		{
			return ConvertJwtTokenToString(GenerateJwtToken(tokenLifeTime_Min, jwtTokenOptions, claims));
		}

		/// <summary>
		/// Converts a <see cref="JwtSecurityToken"/> instance to its string representation.
		/// </summary>
		/// <param name="token">The <see cref="JwtSecurityToken"/> instance to convert.</param>
		/// <returns>The JWT token as a string.</returns>
		public static string ConvertJwtTokenToString(JwtSecurityToken token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(token);
		}

		/// <summary>
		/// Generates a <see cref="JwtSecurityToken"/> instance.
		/// </summary>
		/// <param name="tokenLifeTime_Min">The lifetime of the token in minutes.</param>
		/// <param name="jwtTokenOptions">The <see cref="JwtTokenOptions"/> instance for token configuration.</param>
		/// <param name="claims">Optional claims to include in the token.</param>
		/// <returns>The generated <see cref="JwtSecurityToken"/> instance.</returns>
		public static JwtSecurityToken GenerateJwtToken(int tokenLifeTime_Min, JwtTokenOptions jwtTokenOptions, IEnumerable<Claim>? claims = null)
		{
			return new JwtSecurityToken(
				issuer: jwtTokenOptions.ISSUER,
				audience: jwtTokenOptions.AUDIENCE,
				claims: claims,
				notBefore: DateTime.UtcNow,
				expires: DateTime.UtcNow.AddMinutes(tokenLifeTime_Min),
				signingCredentials: new SigningCredentials(jwtTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature));
		}

		/// <summary>
		/// Extracts claims from a JWT token string.
		/// </summary>
		/// <param name="token">The JWT token string.</param>
		/// <param name="jwtTokenOptions">The <see cref="JwtTokenOptions"/> instance for token validation.</param>
		/// <returns>An IEnumerable of Claim instances extracted from the token, or <see langword="null"/> if extraction fails.</returns>
		public static IEnumerable<Claim>? ExtractClaims(string token, JwtTokenOptions jwtTokenOptions)
		{
			try
			{
				JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

				var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, jwtTokenOptions.GetTokenValidationParameters(), out SecurityToken _);

				return claimsPrincipal.Claims;
			}
			catch
			{
				return null;
			}
		}
	}
}
