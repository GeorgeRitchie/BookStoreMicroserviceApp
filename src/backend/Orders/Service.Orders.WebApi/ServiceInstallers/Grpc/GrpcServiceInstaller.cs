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
using Microsoft.Extensions.Options;
using Service.Catalog.IntegrationEvents.Grpcs;
using Service.Orders.Application.Common.Interfaces;
using Service.Orders.WebApi.Services;

namespace Service.Orders.WebApi.ServiceInstallers.Grpc
{
	/// <summary>
	/// Represents the GRPC service installer.
	/// </summary>
	internal sealed class GrpcServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration) =>
			services
				.ConfigureOptions<GrpcOptionsSetup>()
				.AddScoped<IOrderGrpcService, OrderGrpcService>()
				// TODO __##__ Add your GRPC clients here
				.AddGrpcClient<BookService.BookServiceClient>((provider, grpcClientFactoryOption) =>
				{
					grpcClientFactoryOption.Address =
						new Uri(provider.GetRequiredService<IOptions<GrpcOptions>>().Value.Url);
				});
	}
}
