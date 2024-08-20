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

using Shared.Repositories;

namespace Service.Carts.Domain.Carts
{
	/// <summary>
	/// Represents the Cart repository interface.
	/// </summary>
	public interface ICartRepository : IRepository<Cart, CartId>
	{
		/// <summary>
		/// Returns the cart by customer identifier if exists, otherwise <see langword="null"/>.
		/// </summary>
		/// <param name="customerId">The customer identifier to search by.</param>
		/// <param name="cancellationToken">The cancelation token.</param>
		/// <returns>The cart entity.</returns>
		/// <remarks>
		/// The <see cref="Cart.Items"/> is included.
		/// </remarks>
		Task<Cart?> GetCartByCustomerId(CustomerId customerId, CancellationToken cancellationToken = default);
	}
}
