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
using Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using Serilog;
using Service.CatalogWrite.WebApi.Extensions;
using Service.CatalogWrite.WebApi.Options;
using Service.CatalogWrite.WebApi.Utility;

LoggingUtility.Run(() =>
{
	WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

	builder.Services
		.ConfigureOptions<WebApiOptionsSetup>()
		.InstallServicesFromAssemblies(
			builder.Configuration,
			Service.CatalogWrite.WebApi.AssemblyReference.Assembly,
			Authorization.AssemblyReference.Assembly,
			Persistence.AssemblyReference.Assembly)
	.InstallModulesFromAssemblies(
			builder.Configuration,
			Service.CatalogWrite.Infrastructure.AssemblyReference.Assembly);

	builder.Host.UseSerilogWithConfiguration();

	WebApplication webApplication = builder.Build();

	var webApiOptions = webApplication.Services.GetRequiredService<IOptions<WebApiOptions>>().Value;

	if (webApiOptions.EnableSwaggerUI)
	{
		var apiVersionDescriptionProvider = webApplication.Services.GetRequiredService<IApiVersionDescriptionProvider>();

		webApplication.UseSwagger();
		webApplication.UseSwaggerUI(opt =>
		{
			// build a swagger endpoint for each discovered API version
			foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
			{
				opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());

				// Use this if you use OAUTH2 (OIDC)
				// TODO __##__ set swagger client credentials of OIDC
				opt.OAuth2RedirectUrl("https://localhost:1501/swagger/index.html");
				opt.OAuthClientId("swagger-ui-4206B798-EE73-4CEB-B0F1-7A5B827EDA61");
				opt.OAuthAppName("Swagger UI");
				opt.OAuthClientSecret("swagger-ui-secret-string");
				opt.OAuthUsePkce();
			}
			opt.InjectStylesheet("/swagger-ui/SwaggerDark.css");
		});
	}

	webApplication.UseCors(corsPolicyBuilder =>
								corsPolicyBuilder
									.AllowAnyHeader()
									.AllowAnyMethod()
									.AllowAnyOrigin());

	// Adding Http request logging behavior via Serilog.
	webApplication.UseSerilogRequestLogging();

	webApplication.UseHttpsRedirection();

	webApplication.UseStaticFiles();

	webApplication.UseAuthentication();
	webApplication.UseAuthorization();

	webApplication.MapControllers();

	webApplication.Run();
});
