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
using Application.ServiceLifetimes;
using MassTransit;

namespace Infrastructure.EventBus
{
	/// <summary>
	/// Represents the event bus.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="EventBus"/> class.
	/// </remarks>
	/// <param name="bus">The bus.</param>
	public sealed class EventBus(IBus bus) : IEventBus, ITransient
	{
		/// <inheritdoc />
		public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
			where TIntegrationEvent : IIntegrationEvent =>
			await bus.Publish(integrationEvent, cancellationToken);
	}
}
