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
using Service.Analytics.WebApi.GraphQL;

namespace Service.Analytics.WebApi.ServiceInstallers.GraphQL
{
	/// <summary>
	/// Represents the GraphQL service installer.
	/// </summary>
	internal sealed class GraphQLServiceInstaller : IServiceInstaller
	{
		// For more info see https://www.youtube.com/playlist?list=PL4hR27YmlLPq55CUGb9ZwSHsh3xzPa3Yt
		// For more info see https://www.youtube.com/watch?v=qrh97hToWpM
		// For more info see https://chillicream.com/docs/hotchocolate/v13/get-started-with-graphql-in-net-core

		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
			=> services
					.AddGraphQLServer()
					.AddAuthorization()
					.AddQueryType<Queries>()
					.AddProjections()
					.AddSorting()
					.AddFiltering();
	}
}
