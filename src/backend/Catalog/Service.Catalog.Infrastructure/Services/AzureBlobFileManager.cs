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

using Application.Models;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Catalog.Application.Common.Services;

namespace Service.Catalog.Infrastructure.Services
{
	/// <summary>
	/// Represents <see cref="IFileManager"/> implementation for file storing.
	/// </summary>
	/// <remarks>
	/// Current implementation is for Azure Blob Storage storing.
	/// </remarks>
	/// <param name="logger">The logger.</param>
	/// <param name="blobServiceClient">The Azure Service client.</param>
	/// <param name="options">The Azure Blob options.</param>
	internal sealed class AzureBlobFileManager(
		ILogger<AzureBlobFileManager> logger,
		BlobServiceClient blobServiceClient,
		IOptions<AzureBlobOptions> options) : IFileManager
	{
		// For more info see https://www.youtube.com/watch?v=Ft4SJgQETAk
		// For more info see https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?toc=%2Fazure%2Fstorage%2Fblobs%2Ftoc.json&bc=%2Fazure%2Fstorage%2Fblobs%2Fbreadcrumb%2Ftoc.json&tabs=visual-studio%2Cblob-storage

		// To run via Visual Studio installation:
		//	Run cmd as Admin then run these commands
		//		cd "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\Extensions\Microsoft\Azure Storage Emulator"
		//		.\azurite.exe --skipApiVersionCheck

		private readonly HashSet<string> _validImageExtensions = new(StringComparer.OrdinalIgnoreCase)
		{
			".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".svg", ".webp"
		};

		/// <inheritdoc/>
		public async Task<bool> DeleteAsync(string source, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(source))
				return true;

			try
			{
				BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(options.Value.ContainerName);
				var blobName = source.Replace(containerClient.Uri.AbsoluteUri, "").TrimStart('/');
				blobName = Uri.UnescapeDataString(blobName);
				BlobClient blobClient = containerClient.GetBlobClient(blobName);
				var result = await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);

				return result.Value;
			}
			catch (Exception ex)
			{
				logger.LogDebug("Something went wrong while removing file with path {source} with exception {@ex}",
										source,
										ex);

				return false;
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

			var extension = Path.GetExtension(file.FileName);
			var relativePath = Path.Combine(subfolder ?? string.Empty, $"{prefix}{file.UniqueKey}{extension}");

			BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(options.Value.ContainerName);
			BlobClient blobClient = containerClient.GetBlobClient(relativePath);

			await blobClient.UploadAsync(file.OpenReadStream(), cancellationToken);

			string blobUrl = blobClient.Uri.ToString();

			return blobUrl;
		}
	}
}
