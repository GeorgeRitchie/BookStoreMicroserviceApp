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
using System.Text;

namespace Application.Helpers
{
	/// <summary>
	/// Represents options for configuring and generating JWT tokens.
	/// </summary>
	public class JwtTokenOptions
	{
		/// <summary>
		/// The issuer of the JWT token.
		/// </summary>
		public string ISSUER { get; private set; }

		/// <summary>
		/// The audience of the JWT token.
		/// </summary>
		public string AUDIENCE { get; private set; }

		/// <summary>
		/// The secret key used for signing the JWT token.
		/// </summary>
		public string KEY { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JwtTokenOptions"/> class with the specified key, issuer, and audience.
		/// If the key is <see langword="null"/>, empty, or whitespace, a new random key is generated.
		/// </summary>
		/// <param name="key">The secret key for signing the JWT token. If <see langword="null"/>, empty, or whitespace, a new random key is generated.</param>
		/// <param name="issuer">The issuer of the JWT token. Cannot be <see langword="null"/> or empty.</param>
		/// <param name="audience">The audience of the JWT token. Cannot be <see langword="null"/> or empty.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="issuer"/> or <paramref name="audience"/> is <see langword="null"/> or empty.</exception>
		public JwtTokenOptions(string? key, string issuer, string audience)
		{
			if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
				KEY = Guid.NewGuid().ToString() + Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
			else
				KEY = key;

			if (string.IsNullOrEmpty(issuer))
			{
				throw new ArgumentNullException(nameof(issuer));
			}

			if (string.IsNullOrEmpty(audience))
			{
				throw new ArgumentNullException(nameof(audience));
			}

			ISSUER = issuer;
			AUDIENCE = audience;
		}

		/// <summary>
		/// Gets a <see cref="SymmetricSecurityKey"/> instance based on the secret key.
		/// </summary>
		/// <returns>The <see cref="SymmetricSecurityKey"/> instance.</returns>
		public virtual SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
		}

		/// <summary>
		/// Gets <see cref="TokenValidationParameters"/> for validating JWT tokens.
		/// </summary>
		/// <returns>The <see cref="TokenValidationParameters"/> instance.</returns>
		public virtual TokenValidationParameters GetTokenValidationParameters()
		{
			return new()
			{
				ClockSkew = TimeSpan.Zero,
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				RequireExpirationTime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = ISSUER,
				ValidAudience = AUDIENCE,
				IssuerSigningKey = GetSymmetricSecurityKey(),
			};
		}
	}
}
