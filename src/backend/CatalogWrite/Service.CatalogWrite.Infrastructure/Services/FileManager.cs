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

using Application.Extensions;
using Application.Models;
using Application.ServiceLifetimes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Service.CatalogWrite.Application.Common.Services;

namespace Service.CatalogWrite.Infrastructure.Services
{
    /// <summary>
    /// Represents <see cref="IFileManager"/> implementation for file storing.
    /// </summary>
    /// <remarks>
    /// Current implementation is for local file system storing.
    /// </remarks>
    /// <remarks>
    /// Initializes new instance of the <see cref="FileManager"/> class.
    /// </remarks>
    /// <param name="logger">The logger.</param>
    /// <param name="environment">The application environment.</param>
    /// <param name="httpContextAccessor">The HttpContext accessor.</param>
    internal sealed class FileManager(
		ILogger<FileManager> logger,
		IWebHostEnvironment environment,
		IHttpContextAccessor httpContextAccessor)
		: IFileManager, IScoped
	{
		// TODO ## Assuming for real production deployment, this implementation will be replaced with one that stores files in cloud based storages

		private readonly HashSet<string> _validImageExtensions = new(StringComparer.OrdinalIgnoreCase)
		{
			".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".svg", ".webp"
		};

		/// <inheritdoc/>
		public Task<bool> DeleteAsync(string source, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(source))
				return Task.FromResult(true);

			var absolutePath = Path.Combine(environment.WebRootPath, GetPathFromUrl(source) ?? "");

			try
			{
				if (File.Exists(absolutePath) == true)
					File.Delete(absolutePath);

				return Task.FromResult(true);
			}
			catch (Exception ex)
			{
				logger.LogFormattedDebug(AssemblyReference.ModuleName,
										$"Something went wrong while removing file with path {absolutePath}",
										ex);

				return Task.FromResult(false);
			}
		}

		/// <inheritdoc/>
		public bool IsPhoto(IFile? file)
		{
			if (file == null || string.IsNullOrWhiteSpace(file.FileName))
				return false;

			string extension = Path.GetExtension(file.FileName);
			return _validImageExtensions.Contains(extension);
		}

		/// <inheritdoc/>
		public async Task<string> SaveAsync(IFile file, string? subfolder = null, string prefix = "", CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(file);

			string baseDirectory = environment.WebRootPath;

			var extension = Path.GetExtension(file.FileName);
			var relativePath = Path.Combine(subfolder ?? string.Empty, $"{prefix}{file.UniqueKey}{extension}");
			var absolutePath = Path.Combine(baseDirectory, relativePath);

			if (Directory.Exists(Path.GetDirectoryName(absolutePath)) == false)
				Directory.CreateDirectory(Path.GetDirectoryName(absolutePath)!);

			using (var fileStream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write))
			{
				await file.OpenReadStream().CopyToAsync(fileStream, cancellationToken);
			}

			return GetUrlFromPath(relativePath);
		}

		private string GetUrlFromPath(string relativePath)
		{
			var request = (httpContextAccessor.HttpContext?.Request) ?? throw new InvalidOperationException("No active HTTP request.");
			var scheme = request.Scheme;
			var host = request.Host.Value;

			if (request.Headers.TryGetValue("X-Forwarded-Proto", out var schemeHeader))
				scheme = schemeHeader;

			if (request.Headers.TryGetValue("X-Forwarded-Host", out var hostHeader))
				host = hostHeader;

			var baseUri = $"{scheme}://{host}/";
			var url = $"{baseUri}{relativePath.Replace("\\", "/")}";

			return url;
		}

		private static string? GetPathFromUrl(string? url)
		{
			if (string.IsNullOrWhiteSpace(url))
				return url;

			if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
			{
				throw new ArgumentException("Invalid URL");
			}

			// Extract the path portion of the URL
			var relativePath = uri.AbsolutePath.TrimStart('/');

			return relativePath.Replace("/", "\\");
		}
	}
}
