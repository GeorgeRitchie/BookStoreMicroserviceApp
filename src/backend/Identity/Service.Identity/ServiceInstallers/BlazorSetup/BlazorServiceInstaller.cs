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

using Application.EventBus;
using Application.Mapper;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Service.Identity.Components.Account;
using Service.Identity.Data;
using Service.Identity.Services;
using System.Net.Mail;

namespace Service.Identity.ServiceInstallers.BlazorSetup
{
	/// <summary>
	/// Represents the all other Identity Blazor project services installer.
	/// </summary>
	internal sealed class BlazorServiceInstaller : IServiceInstaller
	{
		/// <inheritdoc />
		public void Install(IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddRazorComponents()
				.AddInteractiveServerComponents();

			services
				.AddHttpContextAccessor()
				.TryAddTransient<IEventBus, Infrastructure.EventBus.EventBus>(); ;

			// TODO __##__ Add here your AutoMapper mappers, or assemblies that contain such mappers
			services.AddAutoMapper(config => config.AddProfile(new AssemblyMappingProfile(AssemblyReference.Assembly)));

			services
				.AddScoped<IEmailSender<User>, IdentityEmailSender>()
				.ConfigureOptions<SmtpOptionsSetup>()
				.AddScoped(provider =>
				{
					var smtpOptions = provider.GetRequiredService<IOptions<SmtpOptions>>().Value;

					var smtpClient = new SmtpClient
					{
						Host = smtpOptions.Host,
						Port = smtpOptions.Port,
						EnableSsl = smtpOptions.EnableSsl,
						Credentials = new System.Net.NetworkCredential(smtpOptions.Username, smtpOptions.Password)
					};

					return smtpClient;
				})
				.AddScoped<IMailService, MailService>();
		}
	}
}
