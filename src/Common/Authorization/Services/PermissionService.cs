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
using Authorization.Options;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Shared.Extensions;

namespace Authorization.Services
{
	// TODO __##__ Don't forget to setup Distributed cache. For in memory distributed cache implementation setup call: builder.Services.AddDistributedMemoryCache();

	/// <summary>
	/// Represents the permission service.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="PermissionService"/> class.
	/// </remarks>
	/// <param name="userPermissionsRequestClient">The user permissions request client.</param>
	/// <param name="distributedCache">The distributed cache.</param>
	/// <param name="options">The options.</param>
	internal sealed class PermissionService(
		IRequestClient<IUserPermissionsRequest> userPermissionsRequestClient,
		IDistributedCache distributedCache,
		IOptions<PermissionAuthorizationOptions> options) : IPermissionService
	{
		/// <inheritdoc />
		public async Task<HashSet<string>> GetPermissionsAsync(
			string identityProviderId,
			CancellationToken cancellationToken = default)
			=> await distributedCache.GetOrCreateAsync(CreateCacheKey(identityProviderId),
				async token => (await GetPermissionsInternalAsync(identityProviderId, cancellationToken)).Permissions,
				new DistributedCacheEntryOptions()
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(options.Value.CacheTimeInSeconds)
				},
				cancellationToken) ?? [];

		private string CreateCacheKey(string identityProviderId) => $"{options.Value.CacheKeyPrefix}{identityProviderId}";

		private async Task<IUserPermissionsResponse> GetPermissionsInternalAsync(string identityProviderId,
																		CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(identityProviderId))
			{
				return new UserPermissionsResponse();
			}

			var request = new UserPermissionsRequest
			{
				UserIdentityProviderId = identityProviderId
			};

			Response<IUserPermissionsResponse> response = await userPermissionsRequestClient
				.GetResponse<IUserPermissionsResponse>(request, cancellationToken);

			return response.Message;
		}

		private sealed class UserPermissionsRequest : IUserPermissionsRequest
		{
			public string UserIdentityProviderId { get; init; } = string.Empty;
		}

		private sealed class UserPermissionsResponse : IUserPermissionsResponse
		{
			public HashSet<string> Permissions { get; set; } = [];
		}
	}
}
