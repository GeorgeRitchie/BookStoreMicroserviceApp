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
using Microsoft.Extensions.Logging;
using Service.CatalogWrite.Application;
using System;

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
	internal sealed class FileManager(ILogger<FileManager> logger, IWebHostEnvironment environment)
		: IFileManager, IScoped
	{
		private readonly HashSet<string> _validImageExtensions = new(StringComparer.OrdinalIgnoreCase)
		{
			".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".svg", ".webp"
		};

		/// <inheritdoc/>
		public Task<bool> DeleteAsync(string source, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(source) || File.Exists(source) == false)
				return Task.FromResult(true);

			try
			{
				File.Delete(source);
				return Task.FromResult(true);
			}
			catch (Exception ex)
			{
				logger.LogFormattedDebug(AssemblyReference.ModuleName,
										$"Something went wrong while removing file with path {source}",
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
			if (subfolder != null)
				baseDirectory = Path.Combine(baseDirectory, subfolder);

			var extension = Path.GetExtension(file.FileName);

			var absolutePath = Path.Combine(baseDirectory, $"{prefix}{file.UniqueKey}{extension}");

			if (Directory.Exists(baseDirectory) == false)
				Directory.CreateDirectory(baseDirectory);

			using (var fileStream = new FileStream(absolutePath, FileMode.Create, FileAccess.Write))
			{
				await file.OpenReadStream().CopyToAsync(fileStream, cancellationToken);
			}

			return absolutePath;
		}
	}
}
