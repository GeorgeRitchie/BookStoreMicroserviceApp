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

namespace Service.Catalog.WebApi.Helpers
{
	/// <summary>
	/// Contains const values used in web api.
	/// </summary>
	public static class ConstantValues
	{
		/// <summary>
		/// Gets the correlation token header name.
		/// </summary>
		// !!! WARNING !!! While changing this constant value, ensure that logger output formatter contains same placeholder name (for serilog see appsettings.json or serilog configurator) !!!
		public const string CorrelationTokenHeaderName = "CorrelationToken";
	}
}
