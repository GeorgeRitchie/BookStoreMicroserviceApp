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

namespace Service.Catalog.Endpoints.Routes
{
	/// <summary>
	/// Contains values used for book endpoints routing stuff.
	/// </summary>
	internal static class BookRoutes
	{
		internal const string Tag = "Books";

		internal const string BaseUri = "api/v{version:apiVersion}/books";

		internal const string Create = $"{BaseUri}/create";

		internal const string Update = $"{BaseUri}/update";

		internal const string GetById = $"{BaseUri}/getbyid";

		internal const string GetAll = $"{BaseUri}/getall";

		internal const string Delete = $"{BaseUri}/delete";

		internal const string Restore = $"{BaseUri}/restore";

		internal const string SetImage = $"{BaseUri}/setimage";

		internal const string RemoveImage = $"{BaseUri}/removeimage";

		internal const string AddAuthor = $"{BaseUri}/addauthor";

		internal const string RemoveAuthor = $"{BaseUri}/removeauthor";

		internal const string AddCategory = $"{BaseUri}/addcategory";

		internal const string RemoveCategory = $"{BaseUri}/removecategory";
	}
}
