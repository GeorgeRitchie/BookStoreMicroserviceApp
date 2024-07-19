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
using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.Authors;
using Service.CatalogWrite.Domain.ValueObjects;

namespace Service.CatalogWrite.Application.Authors.Commands.CreateAuthor
{
	/// <summary>
	/// Represents the <see cref="CreateAuthorCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="CreateAuthorCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The author repository.</param>
	/// <param name="fileManager">The file manager.</param>
	/// <param name="logger">The logger.</param>
	internal sealed class CreateAuthorCommandHandler(
		ICatalogDb db,
		IRepository<Author, AuthorId> repository,
		IFileManager fileManager,
		ILogger<CreateAuthorCommandHandler> logger)
		: ICommandHandler<CreateAuthorCommand, Guid>
	{
		/// <inheritdoc/>
		public async Task<Result<Guid>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
		{
			string? iconFileSource = null;
			string? photoFileSource = null;

			try
			{
				iconFileSource = await fileManager.SaveAsync(request.Icon,
														 ConstantValues.ImagesSubFolder,
														 AuthorImageType.ProfileIcon.Name,
														 cancellationToken);
				photoFileSource = await fileManager.SaveAsync(request.Photo,
														 ConstantValues.ImagesSubFolder,
														 AuthorImageType.AuthorPhoto.Name,
														 cancellationToken);

				var icon = ImageSource<AuthorImageType>.Create(iconFileSource, AuthorImageType.ProfileIcon);
				var photo = ImageSource<AuthorImageType>.Create(photoFileSource, AuthorImageType.AuthorPhoto);
				var email = request.Email is null ? null : Email.Create(request.Email);
				var website = request.Site is null ? null : Website.Create(request.Site);

				return await Result.Combine(
								icon,
								photo,
								email ?? Result.Success(),
								website ?? Result.Success())
							.Bind(() => Author.Create(request.FirstName,
													request.LastName,
													request.Description,
													email?.Value,
													website?.Value,
													[icon.Value!, photo.Value!]))
							.Tap<Author>(author => repository.Create(author))
							.Tap(() => db.SaveChangesAsync(cancellationToken))
							.Map(author => author.Id.Value)
							.OnFailure(e => RemoveFiles(iconFileSource, photoFileSource, cancellationToken));
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create new author.", ex);

				await RemoveFiles(iconFileSource, photoFileSource, cancellationToken);

				return Result.Failure<Guid>(AuthorErrors.CreateOperationFailed);
			}
		}

		private async Task RemoveFiles(string? icon, string? photo, CancellationToken cancellationToken = default)
		{
			if (icon is not null)
				await fileManager.DeleteAsync(icon, cancellationToken);
			if (photo is not null)
				await fileManager.DeleteAsync(photo, cancellationToken);
		}
	}
}
