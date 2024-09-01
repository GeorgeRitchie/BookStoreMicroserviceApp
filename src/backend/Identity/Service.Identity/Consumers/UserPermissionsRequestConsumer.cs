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
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Identity.Data;

namespace Service.Identity.Consumers;

/// <summary>
/// Represents the <see cref="IUserPermissionsRequest"/> consumer.
/// </summary>
internal sealed class UserPermissionsRequestConsumer(
	UserManager<User> userManager,
	RoleManager<Role> roleManager)
	: IConsumer<IUserPermissionsRequest>
{
	/// <inheritdoc />
	public async Task Consume(ConsumeContext<IUserPermissionsRequest> context)
	{
		var user = await userManager.FindByIdAsync(context.Message.UserIdentityProviderId);
		var roleNames = await userManager.GetRolesAsync(user);
		var permissions = roleManager.Roles.Include(i => i.Permissions)
									.Where(i => roleNames.Contains(i.Name))
									.SelectMany(o => o.Permissions)
									.Select(o => o.Name).ToHashSet();

		var response = new UserPermissionsResponse
		{
			Permissions = permissions,
		};

		await context.RespondAsync<IUserPermissionsResponse>(response);
	}

	private sealed class UserPermissionsResponse : IUserPermissionsResponse
	{
		public HashSet<string> Permissions { get; init; } = [];
	}
}
