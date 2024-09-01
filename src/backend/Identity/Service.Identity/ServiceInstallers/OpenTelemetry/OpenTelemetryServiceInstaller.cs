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
using MassTransit.Logging;
using MassTransit.Monitoring;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Service.Identity.ServiceInstallers.OpenTelemetry
{
	/// <summary>
	/// Represents the Open Telemetry service installer.
	/// </summary>
	internal sealed class OpenTelemetryServiceInstaller : IServiceInstaller
	{
		// For more info see https://www.youtube.com/watch?v=HrRrJ5wTtdk
		// For more info see https://masstransit.io/documentation/configuration/observability
		// For more info see https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.Quartz

		// For more info see https://www.youtube.com/watch?v=SA8BainHeS0
		// For more info see https://www.youtube.com/watch?v=8aG410nmjtQ

		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
		{
			services
			.AddOpenTelemetry()
				.ConfigureResource(ConfigureResource)
				.WithMetrics(metrics =>
				{
					// This is how to add custom meters
					//metrics.AddMeter(
					//	"Microsoft.AspNetCore.Hosting",
					//	"Microsoft.AspNetCore.Server.Kestrel",
					//	"System.Net.Http");

					metrics
						.AddAspNetCoreInstrumentation()
						.AddHttpClientInstrumentation();

					metrics
						.AddMeter(InstrumentationOptions.MeterName); // MassTransit Meter

					metrics.AddOtlpExporter();
				})
				.WithTracing(tracing =>
				{
					tracing
						.AddAspNetCoreInstrumentation()
						.AddHttpClientInstrumentation()
						.AddEntityFrameworkCoreInstrumentation()
						.AddGrpcCoreInstrumentation()
						.AddRedisInstrumentation()
						.AddQuartzInstrumentation();

					tracing.AddSource(DiagnosticHeaders.DefaultListenerName); // MassTransit ActivitySource

					tracing.AddOtlpExporter();
				});
		}

		private void ConfigureResource(ResourceBuilder r)
		{
			r.AddService(AssemblyReference.ModuleName,
				serviceVersion: "1.0.0", // TODO make this app versioning more centralized
				serviceInstanceId: Environment.MachineName);
		}
	}
}
