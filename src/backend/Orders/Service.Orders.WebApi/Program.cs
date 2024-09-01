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
using HealthChecks.UI.Client;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using OpenTelemetry.Logs;
using Serilog;
using Service.Orders.WebApi.Extensions;
using Service.Orders.WebApi.Middlewares;
using Service.Orders.WebApi.Options;
using Service.Orders.WebApi.Utility;

LoggingUtility.Run(() =>
{
	WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

	builder.Services
		.ConfigureOptions<WebApiOptionsSetup>()
		.InstallServicesFromAssemblies(
			builder.Configuration,
			Service.Orders.WebApi.AssemblyReference.Assembly,
			Authorization.AssemblyReference.Assembly,
			Persistence.AssemblyReference.Assembly)
		.InstallModulesFromAssemblies(
				builder.Configuration,
				Service.Orders.Infrastructure.AssemblyReference.Assembly);

	builder.Host.UseSerilogWithConfiguration();
	builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());

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
				opt.OAuthClientId("swagger-client-3F9610DD-0032-41FA-92F5-397E6B66AE15");
				opt.OAuthAppName("Swagger UI");
				opt.OAuthClientSecret("swagger-ui-DF669678-66B8-4982-890A-E52F7632A3BA");
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

	webApplication.UseGlobalExceptionHandlerMiddleware()
					.UseSerilogRequestLogging();

	webApplication.MapHealthChecks("/health", new HealthCheckOptions()
	{
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
	});
	// TODO add authorization and also add health check policy that allows access to specific type of users
	//.RequireAuthorization();

	webApplication.UseHttpsRedirection();

	webApplication.UseStaticFiles();

	webApplication.UseRouting();

	webApplication.UseAuthentication();
	webApplication.UseAuthorization();

	webApplication.MapControllers();

	webApplication.Run();
});
