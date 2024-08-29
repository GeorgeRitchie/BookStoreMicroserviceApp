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
using Azure.Storage.Blobs;
using Infrastructure.Configuration;
using Infrastructure.EventBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Service.Catalog.Application.Common.Services;
using Service.Catalog.Infrastructure.Services;

namespace Service.Catalog.Infrastructure.ServiceInstallers
{
	/// <summary>
	/// Represents the Catalog service infrastructure service installer.
	/// </summary>
	internal sealed class InfrastructureServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration) =>
			services
				// .AddSingleton<IFileManager, LocalFileManager>()
				.ConfigureOptions<AzureBlobOptionsSetup>()
				.AddSingleton<IFileManager, AzureBlobFileManager>()
				.AddSingleton(sp =>
				{
					var options = sp.GetRequiredService<IOptions<AzureBlobOptions>>().Value;
					return new BlobServiceClient(options.ConnectionString);
				})
				.TryAddTransient<IEventBus, EventBus>();
	}
}
