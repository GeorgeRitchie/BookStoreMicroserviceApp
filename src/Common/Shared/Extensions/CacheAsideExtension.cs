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

using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Shared.Helpers;

namespace Shared.Extensions
{
	/// <summary>
	/// Contains extension methods for <see cref="IDistributedCache"/> that implements Cache-Aside pattern.
	/// </summary>
	public static class CacheAsideExtension
	{
		private static readonly DistributedCacheEntryOptions DefaultOptions = new()
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
		};

		private static readonly SemaphoreSlim Semaphore = new(1, 1);

		/// <summary>
		/// Gets a cached value by the specified key. If the value is not present in the cache,
		/// it uses the provided factory function to create the value, caches it, and then returns it.
		/// </summary>
		/// <typeparam name="T">The type of the value to be cached and returned.</typeparam>
		/// <param name="distributedCache">The <see cref="IDistributedCache"/> instance.</param>
		/// <param name="key">The cache key.</param>
		/// <param name="factory">A function to create the value if it is not found in the cache.</param>
		/// <param name="options">The cache entry options to use. If null, default options will be used.</param>
		/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
		/// <returns>The cached value, or the value created by the factory function if the cache is empty.</returns>
		/// <remarks>
		/// The default cache entry options set the absolute expiration to 2 minutes from the current time.
		/// </remarks>
		public static async Task<T?> GetOrCreateAsync<T>(
			this IDistributedCache distributedCache,
			string key,
			Func<CancellationToken, Task<T>> factory,
			DistributedCacheEntryOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			var cashedValue = await distributedCache.GetStringAsync(key, cancellationToken);

			T? value;
			if (!string.IsNullOrWhiteSpace(cashedValue))
			{
				value = JsonConvert.DeserializeObject<T>(cashedValue);

				if (value is not null)
					return value;
			}

			var hasLock = await Semaphore.WaitAsync(ConstantValues.SemaphoreMillisecondsTimeOut, cancellationToken);

			if (!hasLock)
				return default;

			try
			{
				// TODO ** change this implementation of cache stampede protection when .Net 9 or later version released with new hybrid cache build in interface
				// For more info see https://www.youtube.com/watch?v=CVQz0E33ft4
				cashedValue = await distributedCache.GetStringAsync(key, cancellationToken);

				if (!string.IsNullOrWhiteSpace(cashedValue))
				{
					value = JsonConvert.DeserializeObject<T>(cashedValue);

					if (value is not null)
						return value;
				}

				value = await factory(cancellationToken);

				if (value is null)
					return default;

				await distributedCache.SetStringAsync(key,
												JsonConvert.SerializeObject(value),
												options ?? DefaultOptions,
												cancellationToken);
			}
			finally
			{
				Semaphore.Release();
			}

			return value;
		}
	}
}
