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

using Infrastructure.Configuration;

namespace Service.Catalog.WebApi.ServiceInstallers.DistributedCaching
{
	/// <summary>
	/// Represents the distributed cache service installer.
	/// </summary>
	internal sealed class DistributedCacheServiceInstaller : IServiceInstaller
	{
		private const string ConfigurationSectionName = "Redis";
		private const string RedisEnablePropPath = ConfigurationSectionName + ":EnableRedis";

		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
		{
			if (IsRedisEnabled(configuration))
			{
				services.AddStackExchangeRedisCache(options =>
						{
							configuration.GetSection(ConfigurationSectionName).Bind(options);
						});
			}
			else
			{
				services.AddDistributedMemoryCache();
			}
		}

		/// <summary>
		/// Determines whether Redis is enabled based on the configuration settings.
		/// </summary>
		/// <param name="configuration">The application configuration instance.</param>
		/// <returns><see langword="true"/> if Redis is enabled; otherwise, <see langword="false"/>.</returns>
		public static bool IsRedisEnabled(IConfiguration configuration)
			=> configuration.GetValue<bool>(RedisEnablePropPath);
	}
}
