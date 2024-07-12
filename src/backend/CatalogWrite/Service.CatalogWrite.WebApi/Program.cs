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

using Serilog;
using Service.CatalogWrite.WebApi.Extensions;
using Service.CatalogWrite.WebApi.Utility;
using Infrastructure.Extensions;

LoggingUtility.Run(() =>
{
	WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

	builder.Services
		.InstallServicesFromAssemblies(
			builder.Configuration,
			Service.CatalogWrite.WebApi.AssemblyReference.Assembly,
			Authorization.AssemblyReference.Assembly,
			Persistence.AssemblyReference.Assembly)
	.InstallModulesFromAssemblies(
			builder.Configuration,
			Service.CatalogWrite.Infrastructure.AssemblyReference.Assembly);

	builder.Host.UseSerilogWithConfiguration();


	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	WebApplication webApplication = builder.Build();

	webApplication
		// TODO make swagger switchable (like add/not add) by appsettings.json, by default not add
		.UseSwagger()
		.UseSwaggerUI()
		.UseCors(corsPolicyBuilder =>
			corsPolicyBuilder
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowAnyOrigin());

	webApplication
		.UseSerilogRequestLogging() // Adding Http request logging behavior via Serilog.
		.UseHttpsRedirection()
		.UseAuthentication()
		.UseAuthorization();

	webApplication.MapControllers();

	webApplication.Run();
});
