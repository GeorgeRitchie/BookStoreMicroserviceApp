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

using Service.CatalogWrite.Domain.ImageSources;
using Service.CatalogWrite.Domain.Publishers;

namespace Service.CatalogWrite.Application.Publishers
{
	/// <summary>
	/// Contains the publisher errors.
	/// </summary>
	internal static class PublisherErrors
	{
		/// <summary>
		/// Gets invalid photo file error. Requires file name.
		/// </summary>
		internal static Func<string, Error> OnlyPhotoFileIsAllowed
			=> fileName => new("Publisher.OnlyPhotoFileIsAllowed",
								$"Passed file '{fileName}' is not a valid image file.");

		/// <summary>
		/// Gets publisher update operation failed error.
		/// </summary>
		internal static Error UpdateOperationFailed
			=> new("Publisher.UpdateOperationFailed", "Publisher update operation failed.");

		/// <summary>
		/// Gets publisher set image operation failed error. Requires file name.
		/// </summary>
		internal static Func<string, Error> SetImageOperationFailed
			=> fileName => new("Publisher.SetImageOperationFailed",
								$"Setting file '{fileName}' to publisher failed.");

		/// <summary>
		/// Gets publisher not found error.
		/// </summary>
		internal static Func<PublisherId, NotFoundError> NotFound
			=> publisherId => new NotFoundError("Publisher.NotFound",
								$"Publisher with the identifier {publisherId.Value} was not found.");

		/// <summary>
		/// Gets publisher image not found error.
		/// </summary>
		internal static Func<PublisherId, ImageSourceId, NotFoundError> PublisherImageNotFound
			=> (publisherId, imageId) => new NotFoundError(
				"Publisher.ImageNotFound",
				$"Publisher with the identifier {publisherId.Value} does not have image with identifier '{imageId.Value}'.");
	}
}
