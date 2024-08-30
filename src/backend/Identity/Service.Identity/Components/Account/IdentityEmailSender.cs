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

using Microsoft.AspNetCore.Identity;
using Service.Identity.Data;
using Service.Identity.Services;

namespace Service.Identity.Components.Account
{
	// Remove the "else if (EmailSender is IdentityNoOpEmailSender)" block from RegisterConfirmation.razor after updating with a real implementation.
	internal sealed class IdentityEmailSender(IMailService smtpClient) : IEmailSender<User>
	{
		public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink) =>
			smtpClient.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

		public Task SendPasswordResetLinkAsync(User user, string email, string resetLink) =>
			smtpClient.SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

		public Task SendPasswordResetCodeAsync(User user, string email, string resetCode) =>
			smtpClient.SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
	}
}
