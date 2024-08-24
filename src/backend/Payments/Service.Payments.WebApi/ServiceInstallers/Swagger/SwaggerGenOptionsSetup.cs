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

using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Service.Payments.WebApi.ServiceInstallers.Swagger
{
	/// <summary>
	/// Represents the <see cref="SwaggerGenOptions"/> setup.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="SwaggerGenOptionsSetup"/> class.
	/// </remarks>
	/// <param name="provider">The Api Versioning provider.</param>
	internal sealed class SwaggerGenOptionsSetup(IApiVersionDescriptionProvider provider)
		: IConfigureOptions<SwaggerGenOptions>
	{
		/// <inheritdoc />
		public void Configure(SwaggerGenOptions options)
		{
			foreach (var description in provider.ApiVersionDescriptions)
			{
				var apiVersion = description.ApiVersion.ToString();

				// configure to add xml comments into swagger documentation from required assemblies
				// Do not forget to add <GenerateDocumentationFile>true</GenerateDocumentationFile> to all these assemblies
				options.IncludeXmlComments(GetXmlDocumentationFileFor(AssemblyReference.Assembly));
				options.IncludeXmlComments(GetXmlDocumentationFileFor(Endpoints.AssemblyReference.Assembly));
				options.IncludeXmlComments(GetXmlDocumentationFileFor(Application.AssemblyReference.Assembly));

				// configure to add general info about program in swagger UI
				options.SwaggerDoc(description.GroupName,
				new OpenApiInfo
				{
					Title = $"Api {apiVersion}",
					Version = apiVersion,
					Description = "Api to interact with server.",
					TermsOfService = new Uri("https://example.com/terms"),  // TODO __##__ Make it real address to real terms of service
					Contact = new OpenApiContact                            // TODO __##__ Make it real contact to company
					{
						Name = "Example Contact",
						Email = "example@gmail.com",
						Url = new Uri("https://example.com/contact")
					},
					License = new OpenApiLicense                            // TODO __##__ Make it real license
					{
						Name = "Example License",
						Url = new Uri("https://example.com/license")
					}
				});

				// configure to enable authentication and authorization in server program by swagger UI
				// Use this if you want to use OAUTH2 (OIDC)
				options.AddSecurityDefinition($"AuthToken {apiVersion}",
				new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						Password = new OpenApiOAuthFlow
						{// TODO __##__ Replace with real url of identity server 4
							AuthorizationUrl = new Uri("https://localhost:1521/connect/authorize"),
							TokenUrl = new Uri("https://localhost:1521/connect/token"),
							RefreshUrl = new Uri("https://localhost:1521/connect/token"),
							Scopes = new Dictionary<string, string>
							{
								{ "openid", "OpenID" },
								{ "profile", "Profile" },
								{ "Template", "Web Api" },
							}
						},

						//AuthorizationCode = new OpenApiOAuthFlow
						//{// TODO __##__ Replace with real url of identity server 4
						//	AuthorizationUrl = new Uri("https://localhost:1521/connect/authorize"),
						//	TokenUrl = new Uri("https://localhost:1521/connect/token"),
						//	RefreshUrl = new Uri("https://localhost:1521/connect/token"),
						//	Scopes = new Dictionary<string, string>
						//	{
						//		{ "openid", "OpenID" },
						//		{ "profile", "Profile" },
						//		{ "Template", "Web Api" },
						//	}
						//}
					}
				});

				// Configuration to make Swagger only send jwt token, which user must manually get from jwt issuer, to endpoints.
				// configure to enable authentication and authorization in server program by swagger UI
				// Use this if you want to use custom JWT authentication/authorization
				//options.AddSecurityDefinition($"AuthToken {apiVersion}",
				//	new OpenApiSecurityScheme
				//	{
				//		In = ParameterLocation.Header,
				//		Type = SecuritySchemeType.Http,
				//		BearerFormat = "JWT",
				//		Scheme = "Bearer",
				//		Name = "Authorization",
				//		Description = "Authorization token"
				//	});

				// configure to enable authentication and authorization in server program by swagger UI
				// Use this if you use OAUTH2 (OIDC)
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = $"AuthToken {apiVersion}"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});

				// Use this if you use custom JWT authentication/authorization
				//options.AddSecurityRequirement(new OpenApiSecurityRequirement
				//{
				//	{
				//		new OpenApiSecurityScheme
				//		{
				//			Reference = new OpenApiReference
				//			{
				//				Type = ReferenceType.SecurityScheme,
				//				Id = $"AuthToken {apiVersion}"
				//			}
				//		},
				//		new string[] { }
				//	}
				//});

				// Allows Swagger to manage methods representing specific endpoints from each other (for two methods having same name the code methodInfo.DeclaringType?.Name + methodInfo.Name ensures to include class name into method name)
				options.CustomOperationIds(apiDescription =>
					apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
						? methodInfo.DeclaringType?.Name + methodInfo.Name
						: null);

				// Resolve schemaId conflict for classes with the same name in different namespaces
				options.CustomSchemaIds(type => type.FullName);

				// Adding Authentication requirement to Swagger Docs
				options.OperationFilter<AuthorizeOperationFilter>();

				// Makes Swagger get information (like summary, tag, etc) about endpoint from attribute SwaggerOperation of Swashbuckle.AspNetCore.Annotations package
				options.EnableAnnotations();

				// Makes Swagger correctly handle endpoints defined by Ardalis.ApiEndpoints package
				options.UseApiEndpoints();
			}
		}

		private static string GetXmlDocumentationFileFor(Assembly assembly)
		{
			var xmlFile = $"{assembly.GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

			return xmlPath;
		}
	}
}
