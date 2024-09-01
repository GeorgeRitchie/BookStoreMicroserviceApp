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

namespace Authorization.Services
{
	/// <summary>
	/// Represents the permission service interface.
	/// </summary>
	public interface IPermissionService
	{
		/// <summary>
		/// Gets the permissions for the user with the specified user identifier.
		/// </summary>
		/// <param name="identityProviderId">The identity provider identifier.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The permission for the user with the specified identifier.</returns>
		Task<HashSet<string>> GetPermissionsAsync(string identityProviderId, CancellationToken cancellationToken = default);
	}
}
