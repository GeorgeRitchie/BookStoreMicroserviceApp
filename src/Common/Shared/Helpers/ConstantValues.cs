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

namespace Shared.Helpers
{
	/// <summary>
	/// Contains const values used in shared project.
	/// </summary>
	public static class ConstantValues
	{
		/// <summary>
		/// Gets timeout for semaphore in milliseconds.
		/// </summary>
		internal const int SemaphoreMillisecondsTimeOut = 1000 * 5;
	}
}
