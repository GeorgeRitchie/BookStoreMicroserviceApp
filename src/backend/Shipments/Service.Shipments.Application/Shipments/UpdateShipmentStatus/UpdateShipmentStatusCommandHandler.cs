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

using Application.EventBus;
using Service.Shipments.Domain;
using Service.Shipments.Domain.Shipments;
using Service.Shipments.IntegrationEvents;

namespace Service.Shipments.Application.Shipments.UpdateShipmentStatus
{
	/// <summary>
	/// Represents the <see cref="UpdateShipmentStatusCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="UpdateShipmentStatusCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The shipment repository.</param>
	/// <param name="eventBus">The event bus.</param>
	internal sealed class UpdateShipmentStatusCommandHandler(
		IShipmentDb db,
		IShipmentRepository repository,
		IEventBus eventBus)
		: ICommandHandler<UpdateShipmentStatusCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(UpdateShipmentStatusCommand request, CancellationToken cancellationToken)
		{
			var shipment = await repository.GetAll()
							.FirstOrDefaultAsync(i => i.Id == new ShipmentId(request.ShipmentId), cancellationToken);

			if (shipment == null)
				return Result.Failure(ShipmentErrors.NotFound(new ShipmentId(request.ShipmentId)));

			var newStatus = ShipmentStatus.FromName(request.StatusName);

			if (newStatus == null)
				return Result.Failure(ShipmentErrors.InvalidShipmentStatusName(request.StatusName));

			shipment.Status = newStatus;

			repository.Update(shipment);
			await db.SaveChangesAsync(cancellationToken);

			await eventBus.PublishAsync(new ShipmentProcessedIntegrationEvent(
											Guid.NewGuid(),
											DateTime.UtcNow,
											shipment.OrderId.Value,
											shipment.Status.Name,
											null),
										cancellationToken);

			return Result.Success();
		}
	}
}
