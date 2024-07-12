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

namespace Service.CatalogWrite.WebApi.Options
{
	/// <summary>
	/// Represents the web api option.
	/// </summary>
	internal sealed class WebApiOptions
	{
		/// <summary>
		/// Gets the flag to enable or disable Swagger UI.
		/// </summary>
		public bool EnableSwaggerUI { get; init; } = false;
	}
}
