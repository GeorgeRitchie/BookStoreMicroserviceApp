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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Persistence.Inbox;
using Polly;
using Polly.Retry;
using Quartz;
using Service.Catalog.Domain;
using Shared.Helpers;
using Shared.Repositories;

namespace Service.Catalog.Infrastructure.BackgroundJobs.ProcessInboxMessages
{
	/// <summary>
	/// Represents the background job for processing inbox messages.
	/// </summary>
	[DisallowConcurrentExecution]
	internal sealed class ProcessInboxMessagesJob : IJob
	{
		private static readonly JsonSerializerSettings JsonSerializerSettings = new()
		{
			TypeNameHandling = TypeNameHandling.All,
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
			ContractResolver = new PrivateSetterResolver(),
		};

		private readonly IServiceProvider _serviceProvider;
		private readonly ProcessInboxMessagesOptions _options;
		private readonly AsyncRetryPolicy _policy;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessInboxMessagesJob"/> class.
		/// </summary>
		/// <param name="serviceProvider">The service provider.</param>
		/// <param name="options">The options.</param>
		public ProcessInboxMessagesJob(
			IServiceProvider serviceProvider,
			IOptions<ProcessInboxMessagesOptions> options)
		{
			_serviceProvider = serviceProvider;
			_options = options.Value;
			_policy = Policy.Handle<Exception>().RetryAsync(_options.RetryCount);
		}

		/// <inheritdoc />
		public async Task Execute(IJobExecutionContext context)
		{
			using var scope = _serviceProvider.CreateScope();
			IRepository<InboxMessage, Guid> inboxRepository = scope.ServiceProvider
																	.GetRequiredService<IRepository<InboxMessage, Guid>>();
			ICatalogDb db = scope.ServiceProvider.GetRequiredService<ICatalogDb>();

			List<InboxMessage> inboxMessagesList = await inboxRepository.GetAll()
				.Where(im => im.ProcessedOnUtc == null)
				.OrderBy(im => im.OccurredOnUtc)
				.Take(_options.BatchSize).ToListAsync();

			if (inboxMessagesList.Count == 0)
			{
				return;
			}

			foreach (InboxMessage inboxMessage in inboxMessagesList)
			{
				IIntegrationEvent integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(
					inboxMessage.Content,
					JsonSerializerSettings)!;

				IEnumerable<IIntegrationEventHandler> handlers = IntegrationEventHandlerFactory.GetHandlers(
					integrationEvent.GetType(),
					_serviceProvider);

				PolicyResult result = await _policy.ExecuteAndCaptureAsync(async () =>
				{
					foreach (IIntegrationEventHandler handler in handlers)
					{
						await handler.Handle(integrationEvent, context.CancellationToken);
					}
				});

				inboxMessage.ProcessedOnUtc = DateTime.UtcNow;
				inboxMessage.Error = result.FinalException?.ToString();

				// TODO ** here will be good to add code to send notification to admin that some event was not handled (has error), at least for critical events

				inboxRepository.Update(inboxMessage);
				await db.SaveChangesAsync();
			}
		}
	}
}
