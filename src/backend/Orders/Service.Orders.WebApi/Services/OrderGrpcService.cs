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

using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using Service.Catalog.IntegrationEvents.Grpcs;
using Service.Orders.Application.Common.Interfaces;
using Service.Orders.Domain.OrderItems;
using Service.Orders.WebApi.ServiceInstallers.Grpc;
using Shared.Errors;
using Shared.Results;

namespace Service.Orders.WebApi.Services
{
	/// <summary>
	/// Represents service for GRPC request.
	/// </summary>
	internal sealed class OrderGrpcService(
		BookService.BookServiceClient grpcClient,
		ILogger<OrderGrpcService> logger,
		IOptions<GrpcOptions> options)
		: IOrderGrpcService
	{
		private Func<string, Error> RequestError => message => new("Grpc.RequestFailed", message);

		private readonly AsyncRetryPolicy _policy = Policy.Handle<Exception>().RetryAsync(options.Value.RetryCount);

		/// <inheritdoc/>
		public async Task<Result> DecreasePaperBookSourceQuantityAsync(List<OrderItem> entities, CancellationToken cancellationToken = default)
		{
			var input = new DecreaseInput();
			input.BookDtos.AddRange(entities.Select(i => new BookDto
			{
				BookId = i.BookId.Value.ToString(),
				Quantity = i.Quantity
			}));

			PolicyResult<ActionResult> result = await _policy.ExecuteAndCaptureAsync(async (ct)
					=> await grpcClient.DecreasePaperBookSourceQuantityAsync(input, cancellationToken: ct),
				cancellationToken);

			if (result.Outcome == OutcomeType.Failure)
			{
				logger.LogWarning("Grpc request failed: {@ex}", result.FinalException);

				return Result.Failure(RequestError(result.FinalException.Message));
			}
			else if (result.FinalHandledResult.Status == false)
				return Result.Failure(
					new Error(result.FinalHandledResult.Error.Key, result.FinalHandledResult.Error.Message));
			else
				return Result.Success();
		}

		/// <inheritdoc/>
		public async Task<Result> IncreasePaperBookSourceQuantityAsync(List<OrderItem> entities, CancellationToken cancellationToken = default)
		{
			var input = new IncreaseInput();
			input.BookDtos.AddRange(entities.Select(i => new BookDto
			{
				BookId = i.BookId.Value.ToString(),
				Quantity = i.Quantity
			}));

			PolicyResult<ActionResult> result = await _policy.ExecuteAndCaptureAsync(async (ct)
					=> await grpcClient.IncreasePaperBookSourceQuantityAsync(input, cancellationToken: ct),
				cancellationToken);

			if (result.Outcome == OutcomeType.Failure)
			{
				logger.LogWarning("Grpc request failed: {@ex}", result.FinalException);

				return Result.Failure(RequestError(result.FinalException.Message));
			}
			else if (result.FinalHandledResult.Status == false)
				return Result.Failure(
					new Error(result.FinalHandledResult.Error.Key, result.FinalHandledResult.Error.Message));
			else
				return Result.Success();
		}
	}
}
