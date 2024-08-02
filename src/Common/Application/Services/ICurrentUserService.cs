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

using Domain.Primitives;
using System.Security.Claims;

namespace Application.Services
{
	/// <summary>
	/// Service for accessing information about the current signed in user in the application.
	/// </summary>
	public interface ICurrentUserService
	{
		/// <summary>
		/// Gets signed in user's Id, or <see langword="null"/> if anonymous request is done.
		/// </summary>
		IEntityId? UserId { get; }

		/// <summary>
		/// Gets signed in user's claims, or empty collection if anonymous request is done.
		/// </summary>
		IEnumerable<Claim> UserClaims { get; }
	}
}
