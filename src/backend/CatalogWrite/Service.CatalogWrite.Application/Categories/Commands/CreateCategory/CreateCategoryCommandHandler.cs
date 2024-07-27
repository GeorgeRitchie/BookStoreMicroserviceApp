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

using Service.CatalogWrite.Application.Common.Helpers;
using Service.CatalogWrite.Application.Common.Services;
using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain.ImageSources;

namespace Service.CatalogWrite.Application.Categories.Commands.CreateCategory
{
	/// <summary>
	/// Represents the <see cref="CreateCategoryCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="CreateCategoryCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The category repository.</param>
	/// <param name="fileManager">The file manager.</param>
	/// <param name="logger">The logger.</param>
	internal sealed class CreateCategoryCommandHandler(
		ICatalogDb db,
		IRepository<Category, CategoryId> repository,
		IFileManager fileManager,
		ILogger<CreateCategoryCommandHandler> logger)
		: ICommandHandler<CreateCategoryCommand, Guid>
	{
		/// <inheritdoc/>
		public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			string? fileSource = null;

			try
			{
				fileSource = await fileManager.SaveAsync(request.Icon,
														 ConstantValues.ImagesSubFolder,
														 CategoryImageType.Icon.Name,
														 cancellationToken);

				return await ImageSource<CategoryImageType>.Create(fileSource, CategoryImageType.Icon)
								.Bind(imageSource => Category.Create(request.Title, imageSource, request.Description))
								.Tap<Category>(category => repository.Create(category))
								.Tap(() => db.SaveChangesAsync(cancellationToken))
								.Map(category => category.Id.Value)
								.OnFailure(e => RemoveFiles(fileSource, cancellationToken));
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create new category.", ex);

				await RemoveFiles(fileSource, cancellationToken);

				return Result.Failure<Guid>(CategoryErrors.CreateOperationFailed);
			}
		}

		private async Task RemoveFiles(string? icon, CancellationToken cancellationToken = default)
		{
			if (icon is not null)
				await fileManager.DeleteAsync(icon, cancellationToken);
		}
	}
}
