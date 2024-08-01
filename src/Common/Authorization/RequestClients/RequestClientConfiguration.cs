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

using Authorization.Contracts;
using Infrastructure.EventBus;
using MassTransit;

namespace Authorization.RequestClients
{
	/// <summary>
	/// Represents the request client configuration.
	/// </summary>
	internal sealed class RequestClientConfiguration : IRequestClientConfiguration
	{
		/// <inheritdoc />
		public void AddRequestClients(IRegistrationConfigurator registrationConfigurator) =>
			registrationConfigurator.AddRequestClient<IUserPermissionsRequest>();

		// TODO __##__ Don't forget to setup MassTransit to configure request clients by calling: bussConfigurator.AddRequestClientsFromAssemblies(Authorization.AssemblyReference.Assembly);
	}
}
