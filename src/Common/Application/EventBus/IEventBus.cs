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

namespace Application.EventBus
{
	/// <summary>
	/// Represents the event bus interface.
	/// </summary>
	public interface IEventBus
	{
		/// <summary>
		/// Publishes the specified integration event to the event bus.
		/// </summary>
		/// <typeparam name="TIntegrationEvent">The integration event type.</typeparam>
		/// <param name="integrationEvent">The integration event.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The result of the publish operation.</returns>
		Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
			where TIntegrationEvent : IIntegrationEvent;
	}
}
