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

using Service.Orders.Domain;
using Service.Orders.Domain.OrderItems;
using Service.Orders.Domain.Orders;

namespace Service.Orders.Application.Orders.Commands.CreateOrder
{
	/// <summary>
	/// Represents the <see cref="CreateOrderCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="CreateOrderCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The order repository.</param>
	internal sealed class CreateOrderCommandHandler(
		IOrderDb db,
		IOrderRepository repository)
		: ICommandHandler<CreateOrderCommand, Guid>
	{
		/// <inheritdoc/>
		public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var orderItemsResults = request.Items
				.Select(i => OrderItem.Create(new BookId(i.BookId),
												i.Title,
												i.Language,
												new BookSourceId(i.SourceId),
												i.Format!,
												i.UnitPrice,
												i.Quantity,
												i.ISBN,
												i.Cover))
				.ToArray();

			return await Result.Combine(orderItemsResults)
				.Bind(() => Order.Create(new CustomerId(request.CustomerId), orderItemsResults.Select(i => i.Value)!)
				.Tap<Order>(order => repository.Create(order)))
				.Tap(() => db.SaveChangesAsync(cancellationToken))
				.Map(o => o.Id.Value);
		}
	}
}
