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

namespace Service.Catalog.Endpoints
{
	/// <summary>
	/// Represents the Catalog module permissions.
	/// </summary>
	internal static class CatalogPermissions
	{
		internal const string EditAuthors = "EditAuthors";

		internal const string EditBooks = "EditBooks";

		internal const string EditBookSources = "EditBookSources";

		internal const string EditCategories = "EditCategories";

		internal const string EditPublishers = "EditPublishers";
	}
}
