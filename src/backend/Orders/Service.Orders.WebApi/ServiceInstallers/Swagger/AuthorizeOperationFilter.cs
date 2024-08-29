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

using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Globalization;
using System.Net;

namespace Service.Orders.WebApi.ServiceInstallers.Swagger
{
	/// <summary>
	/// Represents the authorize operation filter.
	/// </summary>
	internal sealed class AuthorizeOperationFilter : IOperationFilter
	{
		private static readonly HttpStatusCode[] ResponseStatusCodes =
		[
			HttpStatusCode.Unauthorized,
			HttpStatusCode.Forbidden
		];

		/// <inheritdoc />
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			object[] customAttributes = context.MethodInfo.GetCustomAttributes(true);

			object[] declaringTypeCustomAttributes = context.MethodInfo.DeclaringType!.GetCustomAttributes(true);

			bool isAuthorized = declaringTypeCustomAttributes.OfType<AuthorizeAttribute>().Any() ||
								customAttributes.OfType<AuthorizeAttribute>().Any();

			bool isAnonymousAllowed = declaringTypeCustomAttributes.OfType<AllowAnonymousAttribute>().Any() ||
									  customAttributes.OfType<AllowAnonymousAttribute>().Any();

			if (!isAuthorized || isAnonymousAllowed)
			{
				return;
			}

			foreach (HttpStatusCode statusCode in ResponseStatusCodes)
			{
				operation.Responses.TryAdd(
					((int)statusCode).ToString(CultureInfo.InvariantCulture),
					new OpenApiResponse
					{
						Description = statusCode.ToString()
					});
			}
		}
	}
}
