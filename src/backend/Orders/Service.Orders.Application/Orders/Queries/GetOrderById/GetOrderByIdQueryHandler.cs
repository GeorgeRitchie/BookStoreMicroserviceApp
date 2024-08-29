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

using Service.Orders.Domain.Orders;

namespace Service.Orders.Application.Orders.Queries.GetOrderById
{
	/// <summary>
	/// Represents the <see cref="GetOrderByIdQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetOrderByIdQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The order repository.</param>
	/// <param name="mapper">The auto mapper.</param>
	internal sealed class GetOrderByIdQueryHandler(IOrderRepository repository, IMapper mapper)
		: IQueryHandler<GetOrderByIdQuery, OrderDto>
	{
		public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
			=> Result.Create(
					await repository.GetAll()
									.Include(i => i.Payment)
									.Include(i => i.Shipment)
									.Include(i => i.Items)
									.FirstOrDefaultAsync(i => i.Id == request.OrderId
																&& i.CustomerId == request.CustomerId,
														cancellationToken))
				.Map(mapper.Map<OrderDto>)
				.MapFailure(() => OrderErrors.NotFound(request.OrderId));
	}
}
