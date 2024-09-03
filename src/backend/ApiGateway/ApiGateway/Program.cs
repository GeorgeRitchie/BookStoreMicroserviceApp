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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// For more info https://www.youtube.com/watch?v=0RaH9hhOF4g
// For more info https://www.milanjovanovic.tech/blog/implementing-an-api-gateway-for-microservices-with-yarp
// For more info https://microsoft.github.io/reverse-proxy/articles/index.html
builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddHealthChecks();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
	{
		// TODO __##__ Configure JWT settings here
	})
	.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
	{
		// TODO __##__ Configure cookie authentication settings
	});
builder.Services.AddAuthorizationBuilder()
	.AddPolicy("JwtPolicy", policy =>
		policy.RequireAuthenticatedUser().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme))
	.AddPolicy("CookiePolicy", policy =>
		policy.RequireAuthenticatedUser().AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme))
	.AddPolicy("JwtOrCookiePolicy", policy =>
		policy.RequireAuthenticatedUser().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, CookieAuthenticationDefaults.AuthenticationScheme));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.MapHealthChecks("health", new HealthCheckOptions()
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.Run();
