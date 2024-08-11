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

using AutoMapper.QueryableExtensions;
using Service.Orders.Domain.Orders;

namespace Service.Orders.Application.Orders.Queries.GetOrders
{
	/// <summary>
	/// Represents the <see cref="GetOrdersQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetOrdersQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The order repository.</param>
	/// <param name="mapper">The auto mapper.</param>
	internal sealed class GetOrdersQueryHandler(IOrderRepository repository, IMapper mapper)
		: IQueryHandler<GetOrdersQuery, IEnumerable<OrderDto>>
	{
		public async Task<Result<IEnumerable<OrderDto>>> Handle(GetOrdersQuery request,
																CancellationToken cancellationToken)
			=> await repository.GetAllAsNoTracking()
								.Include(i => i.Payment)
								.Include(i => i.Shipment)
								.Include(i => i.Items)
								.Where(i => i.CustomerId == request.CustomerId)
								.ProjectTo<OrderDto>(mapper.ConfigurationProvider)
								.ToListAsync(cancellationToken);
	}
}
