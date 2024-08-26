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

using Service.Catalog.IntegrationEvents;
using Service.Shipments.Domain.Shipments;

namespace Service.Shipments.Application.Shipments.GetBookSourceUrl
{
	/// <summary>
	/// Represents the <see cref="GetBookSourceUrlQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetBookSourceUrlQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The shipment repository.</param>
	internal sealed class GetBookSourceUrlQueryHandler(IShipmentRepository repository)
		: IQueryHandler<GetBookSourceUrlQuery, string>
	{
		public async Task<Result<string>> Handle(GetBookSourceUrlQuery request, CancellationToken cancellationToken)
		{
			var bookSource = await repository.GetAll()
											.Where(i => i.CustomerId == request.CustomerId
														&& i.OrderId == request.OrderId)
											.SelectMany(i => i.Items)
											.Where(i => i.BookSourceId == request.BookSourceId)
											.Select(o => o.BookSource)
											.FirstOrDefaultAsync(cancellationToken);

			if (bookSource == null)
				return Result.Failure<string>(ShipmentErrors.BookSourceNotFound());

			if (bookSource.Format == BookFormat.Paper)
				return Result.Failure<string>(ShipmentErrors.NotEBookError());

			return Result.Failure(bookSource.Url);
		}
	}
}
