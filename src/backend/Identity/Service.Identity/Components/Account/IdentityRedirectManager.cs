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

using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Service.Identity.Components.Account
{
	internal sealed class IdentityRedirectManager(NavigationManager navigationManager)
	{
		public const string StatusCookieName = "Identity.StatusMessage";

		private static readonly CookieBuilder StatusCookieBuilder = new()
		{
			SameSite = SameSiteMode.Strict,
			HttpOnly = true,
			IsEssential = true,
			MaxAge = TimeSpan.FromSeconds(5),
		};

		[DoesNotReturn]
		public void RedirectTo(string? uri)
		{
			uri ??= "";

			// Prevent open redirects.
			if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
			{
				uri = navigationManager.ToBaseRelativePath(uri);
			}

			// During static rendering, NavigateTo throws a NavigationException which is handled by the framework as a redirect.
			// So as long as this is called from a statically rendered Identity component, the InvalidOperationException is never thrown.
			navigationManager.NavigateTo(uri);
			throw new InvalidOperationException($"{nameof(IdentityRedirectManager)} can only be used during static rendering.");
		}

		[DoesNotReturn]
		public void RedirectTo(string uri, Dictionary<string, object?> queryParameters)
		{
			var uriWithoutQuery = navigationManager.ToAbsoluteUri(uri).GetLeftPart(UriPartial.Path);
			var newUri = navigationManager.GetUriWithQueryParameters(uriWithoutQuery, queryParameters);
			RedirectTo(newUri);
		}

		[DoesNotReturn]
		public void RedirectToWithStatus(string uri, string message, HttpContext context)
		{
			context.Response.Cookies.Append(StatusCookieName, message, StatusCookieBuilder.Build(context));
			RedirectTo(uri);
		}

		private string CurrentPath => navigationManager.ToAbsoluteUri(navigationManager.Uri).GetLeftPart(UriPartial.Path);

		[DoesNotReturn]
		public void RedirectToCurrentPage() => RedirectTo(CurrentPath);

		[DoesNotReturn]
		public void RedirectToCurrentPageWithStatus(string message, HttpContext context)
			=> RedirectToWithStatus(CurrentPath, message, context);
	}
}
