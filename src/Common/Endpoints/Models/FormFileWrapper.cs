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
using Microsoft.AspNetCore.Http;

namespace Endpoints.Models
{
	/// <summary>
	/// Represents a wrapper class for <see cref="IFormFile"/>.
	/// </summary>
	/// <param name="formFile">The file to be wrapped.</param>
	/// <remarks>
	/// Initializes a new instance of the <see cref="FormFileWrapper"/> class.
	/// </remarks>
	public sealed class FormFileWrapper(IFormFile formFile) : IFile
	{
		private readonly IFormFile formFile = formFile ?? throw new ArgumentNullException(nameof(formFile));

		/// <inheritdoc/>
		public string FileName => formFile.FileName;

		/// <inheritdoc/>
		public long SizeInBytes => formFile.Length;

		/// <inheritdoc/>
		public Guid UniqueKey { get; private init; } = Guid.NewGuid();

		/// <inheritdoc/>
		public Stream OpenReadStream()
		{
			return formFile.OpenReadStream();
		}
	}
}
