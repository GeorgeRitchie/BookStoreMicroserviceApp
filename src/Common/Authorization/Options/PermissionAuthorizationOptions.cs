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

namespace Authorization.Options
{
	/// <summary>
	/// Represents the permission authorization options.
	/// </summary>
	internal sealed class PermissionAuthorizationOptions
	{
		/// <summary>
		/// Gets the cache key prefix.
		/// </summary>
		public string CacheKeyPrefix { get; init; }

		/// <summary>
		/// Gets the cache time in seconds.
		/// </summary>
		public int CacheTimeInSeconds { get; init; } = 180;
	}
}
