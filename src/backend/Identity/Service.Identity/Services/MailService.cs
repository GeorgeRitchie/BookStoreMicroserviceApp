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

using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Service.Identity.Services
{
	public class MailService(SmtpClient smtpClient, IOptions<SmtpOptions> options) : IMailService
	{
		public async Task SendEmailAsync(string email, string subject, string htmlMessage, CancellationToken cancellationToken = default)
		{
			MailMessage mailMessage = new()
			{
				From = new MailAddress(options.Value.From),
				Subject = subject,
				Body = htmlMessage,
				IsBodyHtml = true
			};
			mailMessage.To.Add(email);

			await smtpClient.SendMailAsync(mailMessage, cancellationToken);
		}
	}
}
