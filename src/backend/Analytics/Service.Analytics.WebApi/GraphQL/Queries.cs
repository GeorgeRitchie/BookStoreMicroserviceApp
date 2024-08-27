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

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Service.Analytics.Domain.Orders;
using Service.Analytics.WebApi.GraphQL.Vms;

namespace Service.Analytics.WebApi.GraphQL
{
	/// <summary>
	/// Represents the GraphQL queries class.
	/// </summary>
	public class Queries
	{
		/// <summary>
		/// Represents GraphQL method to get Order related data.
		/// </summary>
		/// <param name="orderRepository">The order repository.</param>
		/// <returns><see cref="IQueryable{T}"/> to retrieve data from db.</returns>
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<OrderVm> Read([Service] IOrderRepository orderRepository, [Service] IMapper mapper)
			=> orderRepository.GetAllAsNoTracking().ProjectTo<OrderVm>(mapper.ConfigurationProvider);
	}
}
