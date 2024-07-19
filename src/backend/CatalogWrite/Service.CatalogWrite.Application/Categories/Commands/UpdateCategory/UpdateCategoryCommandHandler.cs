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

using Microsoft.Extensions.Logging;
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.ImageSources;

namespace Service.CatalogWrite.Application.Categories.Commands.UpdateCategory
{
	/// <summary>
	/// Represents the <see cref="UpdateCategoryCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="UpdateCategoryCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="catalogRepository">The category repository.</param>
	/// <param name="imgRepository">The image repository.</param>
	/// <param name="fileManager">The file manager.</param>
	/// <param name="logger">The logger.</param>
	internal sealed class UpdateCategoryCommandHandler(
		ICatalogDb db,
		IRepository<Category, CategoryId> catalogRepository,
		IRepository<ImageSource<CategoryImageType>, ImageSourceId> imgRepository,
		IFileManager fileManager,
		ILogger<UpdateCategoryCommandHandler> logger)
		: ICommandHandler<UpdateCategoryCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			var category = await catalogRepository.GetAll()
											.Include(c => c.Icon)
											.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

			if (category == null)
				return Result.Failure(CategoryErrors.NotFound(request.Id));

			var title = request.Title ?? category.Title;
			var description = request.Description ?? category.Description;
			var icon = category.Icon;
			ImageSource<CategoryImageType>? oldIcon = null;

			if (request.Icon is not null)
			{
				oldIcon = category.Icon;
				var imageCreateResult = await CreateImage(request.Icon, cancellationToken).Tap(c => icon = c);

				if (imageCreateResult.IsFailure)
					return imageCreateResult;
			}

			var result = await category.Change(title, icon, description)
				.Tap<Category>(catalogRepository.Update)
				.Tap(() => oldIcon?.Tap(() => imgRepository.Delete(oldIcon)))
				.Tap(() => db.SaveChangesAsync(cancellationToken))
				.Tap(() => oldIcon?.Tap(() => fileManager.DeleteAsync(oldIcon.Source).FireAndForget()))
				.OnFailure(e => oldIcon?.Tap(() => fileManager.DeleteAsync(icon.Source).FireAndForget()));

			return result;
		}

		private async Task<Result<ImageSource<CategoryImageType>>> CreateImage(
			IFile file,
			CancellationToken cancellationToken = default)
		{
			string? fileSource = null;

			try
			{
				fileSource = await fileManager.SaveAsync(file,
														 ConstantValues.ImagesSubFolder,
														 CategoryImageType.Icon.Name,
														 cancellationToken);

				return ImageSource<CategoryImageType>.Create(fileSource, CategoryImageType.Icon);
			}
			catch (Exception ex)
			{
				logger.LogFormattedError(AssemblyReference.ModuleName, "Failed to create new category image source.", ex);

				if (fileSource is not null)
					await fileManager.DeleteAsync(fileSource, cancellationToken);

				return Result.Failure<ImageSource<CategoryImageType>>(CategoryErrors.UpdateOperationFailed);
			}
		}
	}
}
