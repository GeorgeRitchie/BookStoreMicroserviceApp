﻿/* 
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

using Authorization.AuthorizationHandlers;
using Authorization.AuthorizationPolicyProviders;
using Authorization.Options;
using Authorization.Services;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization
{
	/// <summary>
	/// Represents the authorization service installer.
	/// </summary>
	internal sealed class AuthorizationServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration) =>
			services
				.AddAuthorization()
				.ConfigureOptions<PermissionAuthorizationOptionsSetup>()
				.AddScoped<IPermissionService, PermissionService>()
				.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>()
				.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
	}
}
