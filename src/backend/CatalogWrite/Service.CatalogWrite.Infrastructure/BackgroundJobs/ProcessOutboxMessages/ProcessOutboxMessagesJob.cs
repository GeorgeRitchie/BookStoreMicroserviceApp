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

using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Persistence.Outbox;
using Polly;
using Polly.Retry;
using Quartz;
using Service.Catalog.Domain;
using Shared.Repositories;

namespace Service.Catalog.Infrastructure.BackgroundJobs.ProcessOutboxMessages
{
	/// <summary>
	/// Represents the background job for processing outbox messages.
	/// </summary>
	[DisallowConcurrentExecution]
	internal sealed class ProcessOutboxMessagesJob : IJob
	{
		private static readonly JsonSerializerSettings JsonSerializerSettings = new()
		{
			TypeNameHandling = TypeNameHandling.All,
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
		};

		private readonly IPublisher _publisher;
		private readonly IServiceProvider _serviceProvider;
		private readonly ProcessOutboxMessagesOptions _options;
		private readonly AsyncRetryPolicy _policy;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessOutboxMessagesJob"/> class.
		/// </summary>
		/// <param name="publisher">The publisher.</param>
		/// <param name="options">The options.</param>
		/// <param name="serviceProvider">The service provider.</param>
		public ProcessOutboxMessagesJob(
			IPublisher publisher,
			IOptions<ProcessOutboxMessagesOptions> options,
			IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			_publisher = publisher;
			_options = options.Value;
			_policy = Policy.Handle<Exception>().RetryAsync(_options.RetryCount);
		}

		/// <inheritdoc />
		public async Task Execute(IJobExecutionContext context)
		{
			using var scope = _serviceProvider.CreateScope();
			IRepository<OutboxMessage, Guid> outboxRepository = scope.ServiceProvider
																	.GetRequiredService<IRepository<OutboxMessage, Guid>>();
			ICatalogDb db = scope.ServiceProvider.GetRequiredService<ICatalogDb>();

			List<OutboxMessage> outboxMessagesList = await outboxRepository.GetAll()
				.Where(om => om.ProcessedOnUtc == null)
				.OrderBy(om => om.OccurredOnUtc)
				.Take(_options.BatchSize)
				.ToListAsync();

			if (outboxMessagesList.Count == 0)
			{
				return;
			}

			foreach (OutboxMessage outboxMessage in outboxMessagesList)
			{
				IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content,
																						JsonSerializerSettings)!
											?? throw new NotImplementedException();

				PolicyResult result = await _policy.ExecuteAndCaptureAsync(
					() => _publisher.Publish(domainEvent, context.CancellationToken));

				outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
				outboxMessage.Error = result.FinalException?.ToString();

				outboxRepository.Update(outboxMessage);
				await db.SaveChangesAsync();
			}
		}
	}
}
