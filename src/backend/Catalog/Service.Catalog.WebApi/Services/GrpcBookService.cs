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

using Grpc.Core;
using MediatR;
using Service.Catalog.Application.BooSources.Commands.DecreaseQuantity;
using Service.Catalog.Application.BooSources.Commands.IncreaseQuantity;
using Service.Catalog.Domain.BookSources;
using Service.Catalog.IntegrationEvents.Grpcs;

namespace Service.Catalog.WebApi.Services
{
	/// <summary>
	/// Represent Grpc handler for <see cref="BookService"/>.
	/// </summary>
	/// <param name="mediator">The mediator.</param>
	internal sealed class GrpcBookService(IMediator mediator) : BookService.BookServiceBase
	{
		public override async Task<ActionResult> IncreasePaperBookSourceQuantity(IncreaseInput request, ServerCallContext context)
		{
			var commandResult = await mediator.Send(new IncreaseQuantityCommand()
			{
				BookSources = request.BookDtos
									.Select(i => KeyValuePair.Create(
																new BookSourceId(Guid.Parse(i.BookId)),
																i.Quantity))
									.ToList(),
			}, context.CancellationToken);

			if (commandResult.IsSuccess)
			{
				return new ActionResult()
				{
					Status = true,
				};
			}
			else
			{
				return new ActionResult()
				{
					Status = false,
					Error = new ErrorMessage
					{
						Key = commandResult.Errors.FirstOrDefault()?.Code,
						Message = commandResult.Errors.FirstOrDefault()?.Message
					}
				};
			}
		}

		public override async Task<ActionResult> DecreasePaperBookSourceQuantity(DecreaseInput request, ServerCallContext context)
		{
			var commandResult = await mediator.Send(new DecreaseQuantityCommand()
			{
				BookSources = request.BookDtos
									.Select(i => KeyValuePair.Create(
																new BookSourceId(Guid.Parse(i.BookId)),
																i.Quantity))
									.ToList(),
			}, context.CancellationToken);

			if (commandResult.IsSuccess)
			{
				return new ActionResult()
				{
					Status = true,
				};
			}
			else
			{
				return new ActionResult()
				{
					Status = false,
					Error = new ErrorMessage
					{
						Key = commandResult.Errors.FirstOrDefault()?.Code,
						Message = commandResult.Errors.FirstOrDefault()?.Message
					}
				};
			}
		}
	}
}
