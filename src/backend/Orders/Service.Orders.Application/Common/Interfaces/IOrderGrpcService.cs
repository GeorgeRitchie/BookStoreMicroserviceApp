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

using Service.Orders.Domain.OrderItems;

namespace Service.Orders.Application.Common.Interfaces
{
	/// <summary>
	/// Represents the abstraction for GRPC requests.
	/// </summary>
	public interface IOrderGrpcService
	{
		/// <summary>
		/// Sends decrease amount request for passed entities.
		/// </summary>
		/// <param name="entities">Entities to decrease amount.</param>
		/// <returns>Action result.</returns>
		Task<Result> DecreasePaperBookSourceQuantityAsync(List<OrderItem> entities, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sends increase amount request for passed entities.
		/// </summary>
		/// <param name="entities">Entities to increase amount.</param>
		/// <returns>Action result.</returns>
		Task<Result> IncreasePaperBookSourceQuantityAsync(List<OrderItem> entities, CancellationToken cancellationToken = default);
	}
}
