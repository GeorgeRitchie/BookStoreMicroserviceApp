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

using Service.Carts.Domain;
using Service.Carts.Domain.Carts;

namespace Service.Carts.Application.Carts.ClearCart
{
	/// <summary>
	/// Represents the <see cref="ClearCartCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="ClearCartCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The cart repository.</param>
	internal sealed class ClearCartCommandHandler(
		ICartDb db,
		ICartRepository repository)
		: ICommandHandler<ClearCartCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
		{
			Cart? cart = await repository.GetCartByCustomerId(request.CustomerId, cancellationToken);

			if (cart is not null)
			{
				repository.Delete(cart);
				await db.SaveChangesAsync(cancellationToken);
			}

			return Result.Success();
		}
	}
}
