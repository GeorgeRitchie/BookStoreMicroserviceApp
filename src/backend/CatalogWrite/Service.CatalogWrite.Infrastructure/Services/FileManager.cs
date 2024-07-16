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
using Application.ServiceLifetimes;
using Service.CatalogWrite.Application;

namespace Service.CatalogWrite.Infrastructure.Services
{
	/// <summary>
	/// Represents <see cref="IFileManager"/> implementation for file storing.
	/// </summary>
	internal sealed class FileManager : IFileManager, IScoped
	{
		/// <inheritdoc/>
		public Task<bool> DeleteAsync(string source, CancellationToken cancellationToken = default)
		{
			// TODO implement this class
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public bool IsPhoto(IFile? file)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<string> SaveAsync(IFile file, string prefix = "", CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}
