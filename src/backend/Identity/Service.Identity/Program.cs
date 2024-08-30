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

using HealthChecks.UI.Client;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Logs;
using Serilog;
using Service.Identity.Components;
using Service.Identity.Data;
using Service.Identity.Extensions;
using Service.Identity.Options;
using Service.Identity.Utility;

LoggingUtility.Run(() =>
{
	WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

	builder.Services
		.ConfigureOptions<BlazorOptionsSetup>()
		.InstallServicesFromAssemblies(
			builder.Configuration,
			Service.Identity.AssemblyReference.Assembly,
			Authorization.AssemblyReference.Assembly,
			Persistence.AssemblyReference.Assembly)
		.InstallModulesFromAssemblies(
				builder.Configuration,
				Service.Identity.AssemblyReference.Assembly);

	builder.Host.UseSerilogWithConfiguration();
	builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());

	WebApplication app = builder.Build();

	SeedData.EnsureSeedData(app);

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseMigrationsEndPoint();
	}
	else
	{
		app.UseExceptionHandler("/Error", createScopeForErrors: true);
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

	app.UseCors(corsPolicyBuilder =>
								corsPolicyBuilder
									.AllowAnyHeader()
									.AllowAnyMethod()
									.AllowAnyOrigin());

	app.UseSerilogRequestLogging();

	app.MapHealthChecks("/health", new HealthCheckOptions()
	{
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
	})
	.RequireAuthorization(policy => policy.RequireRole(Role.Admin));

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseIdentityServer();
	app.UseAuthentication();
	app.UseAuthorization();

	app.UseStaticFiles();
	app.UseAntiforgery();

	app.MapRazorComponents<App>()
		.AddInteractiveServerRenderMode();

	// Add additional endpoints required by the Identity /Account Razor components.
	app.MapAdditionalIdentityEndpoints();

	app.Run();

});
