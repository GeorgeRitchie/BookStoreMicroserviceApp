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

namespace Application.Models
{
	/// <summary>
	/// Represents a paginated list of items, enabling efficient navigation through large datasets.
	/// </summary>
	/// <typeparam name="T">The type of items contained in the paginated list.</typeparam>
	public class PaginatedList<T>
	{
		/// <summary>
		/// Gets the items on the current page.
		/// </summary>
		public List<T> Items { get; }

		/// <summary>
		/// Gets the current page number.
		/// </summary>
		public int PageNumber { get; }

		/// <summary>
		/// Gets the total number of pages.
		/// </summary>]
		public int TotalPages { get; }

		/// <summary>
		/// Gets the total count of items across all pages.
		/// </summary>
		public int TotalCount { get; }

		/// <summary>
		/// Determines if a previous page exists.
		/// </summary>
		public bool HasPreviousPage => PageNumber > 1;

		/// <summary>
		/// Determines if a next page exists.
		/// </summary>
		public bool HasNextPage => PageNumber < TotalPages;

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginatedList{T}"/> class with the specified items, total count of items, current page number, and each page size.
		/// </summary>
		/// <param name="items">The items on the current page.</param>
		/// <param name="totalCount">The total count of items across all pages.</param>
		/// <param name="pageNumber">The current page number.</param>
		/// <param name="pageSize">The number of items per page.</param>
		public PaginatedList(List<T> items, int totalCount, int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
			TotalCount = totalCount;
			Items = items;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginatedList{T}"/> class with the specified items, total count of items, total number of pages and current page number.
		/// </summary>
		/// <param name="totalCount">The total count of items across all pages.</param>
		/// <param name="totalPages">The total number of pages.</param>
		/// <param name="pageNumber">The current page number.</param>
		/// <param name="items">The items on the current page.</param>
		private PaginatedList(int totalCount, int totalPages, int pageNumber, List<T> items)
		{
			PageNumber = pageNumber;
			TotalPages = totalPages;
			TotalCount = totalCount;
			Items = items;
		}

		/// <summary>
		/// Creates a paginated list from a queryable data source using the specified page number and size.
		/// </summary>
		/// <param name="source">The queryable data source.</param>
		/// <param name="pageNumber">The page number to retrieve.</param>
		/// <param name="pageSize">The number of items per page.</param>
		/// <returns>A paginated list of items.</returns>
		public static PaginatedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
		{
			var totalCount = source.Count();
			var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			return new PaginatedList<T>(items, totalCount, pageNumber, pageSize);
		}

		/// <summary>
		/// Creates a paginated list from a queryable data source using <see cref="PaginationParams"/> optional pagination parameters. If pagination parameters not given, page size will be set to total items count and page number will be set to 1.
		/// </summary>
		/// <param name="source">The queryable data source.</param>
		/// <param name="paginationParams">The pagination parameters.</param>
		/// <returns>A paginated list of items.</returns>
		public static PaginatedList<T> Create(IQueryable<T> source, PaginationParams? paginationParams)
		{
			if (paginationParams != null)
				return Create(source, paginationParams.PageNumber, paginationParams.PageSize);
			else
				return Create(source.ToList());
		}

		/// <summary>
		/// Creates a paginated list from a provided list of items, assuming a single page with a page size equal to items total count.
		/// </summary>
		/// <param name="items">The list of items.</param>
		/// <returns>A paginated list of items.</returns>
		public static PaginatedList<T> Create(List<T> items)
		{
			return Create(items, items.Count, 1, 1);
		}

		/// <summary>
		/// Creates a paginated list from a provided list of items with custom total count of items, total number of pages, and current page number.
		/// </summary>
		/// <param name="items">The list of items.</param>
		/// <param name="totalCount">The total count of items.</param>
		/// <param name="totalPages">The total number of pages.</param>
		/// <param name="pageNumber">The current page number.</param>
		/// <returns>A paginated list of items.</returns>
		public static PaginatedList<T> Create(List<T> items, int totalCount, int totalPages, int pageNumber)
		{
			return new PaginatedList<T>(totalCount, totalPages, pageNumber, items);
		}

		/// <summary>
		/// Creates a paginated list by mapping a source queryable data source to the target type <typeparamref name="T"/> using a mapping function and the specified page number and size.
		/// </summary>
		/// <typeparam name="K">The source type.</typeparam>
		/// <param name="source">The source queryable data source.</param>
		/// <param name="pageNumber">The page number to retrieve.</param>
		/// <param name="pageSize">The number of items per page.</param>
		/// <param name="mappingFunc">The mapping function.</param>
		/// <returns>A paginated list of mapped items.</returns>
		public static PaginatedList<T> Create<K>(IQueryable<K> source, int pageNumber, int pageSize, Func<List<K>, List<T>> mappingFunc)
		{
			var totalCount = source.Count();
			var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			return new PaginatedList<T>(mappingFunc(items), totalCount, pageNumber, pageSize);
		}

		/// <summary>
		/// Creates a paginated list with mapped items using <see cref="PaginationParams"/> optional pagination parameters and a mapping function. If pagination parameters not given, page size will be set to total items count and page number will be set to 1.
		/// </summary>
		/// <typeparam name="K">The source type.</typeparam>
		/// <param name="source">The source queryable data source.</param>
		/// <param name="paginationParams">The pagination parameters.</param>
		/// <param name="mappingFunc">The mapping function.</param>
		/// <returns>A paginated list of mapped items.</returns>
		public static PaginatedList<T> Create<K>(IQueryable<K> source, PaginationParams? paginationParams, Func<List<K>, List<T>> mappingFunc)
		{
			if (paginationParams != null)
				return Create(source, paginationParams.PageNumber, paginationParams.PageSize, mappingFunc);
			else
				return Create(mappingFunc(source.ToList()));
		}

		/// <summary>
		/// Creates a paginated list by mapping a provided list of source items to the target type <typeparamref name="T"/> using a mapping function, and assuming a single page with a page size equal to items total count.
		/// </summary>
		/// <typeparam name="K">The source type.</typeparam>
		/// <param name="items">The list of source items.</param>
		/// <param name="mappingFunc">The mapping function.</param>
		/// <returns>A paginated list of mapped items.</returns>
		public static PaginatedList<T> Create<K>(List<K> items, Func<List<K>, List<T>> mappingFunc)
		{
			return Create(mappingFunc(items));
		}

		/// <summary>
		/// Creates a paginated list with mapped items by mapping function, custom total count of items, total number of pages, and current page number.
		/// </summary>
		/// <typeparam name="K">The source type.</typeparam>
		/// <param name="items">The list of source items.</param>
		/// <param name="totalCount">The total count of items.</param>
		/// <param name="totalPages">The total number of pages.</param>
		/// <param name="pageNumber">The current page number.</param>
		/// <param name="mappingFunc">The mapping function.</param>
		/// <returns>A paginated list of mapped items.</returns>
		public static PaginatedList<T> Create<K>(List<K> items, int totalCount, int totalPages, int pageNumber, Func<List<K>, List<T>> mappingFunc)
		{
			return Create(mappingFunc(items), totalCount, totalPages, pageNumber);
		}
	}
}
