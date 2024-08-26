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
using Service.Shipments.Domain.Shipments;
using Service.Shipments.IntegrationEvents;

namespace Service.Shipments.Application.Shipments.GetShipments
{
	/// <summary>
	/// Represents the <see cref="GetShipmentsQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetShipmentsQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The shipment repository.</param>
	/// <param name="mapper">The mapper.</param>
	internal sealed class GetShipmentsQueryHandler(IShipmentRepository repository, IMapper mapper)
		: IQueryHandler<GetShipmentsQuery, List<ShipmentDto>>
	{
		public async Task<Result<List<ShipmentDto>>> Handle(GetShipmentsQuery request, CancellationToken cancellationToken)
		{
			var query = repository.GetAllAsNoTracking();

			var shipmentStatus = ShipmentStatus.FromName(request.FilterByStatusName ?? "");
			if (shipmentStatus != null)
				query.Where(i => i.Status == shipmentStatus);

			var shipments = await query.ProjectTo<ShipmentDto>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);

			return Result.Success(shipments);
		}
	}
}
