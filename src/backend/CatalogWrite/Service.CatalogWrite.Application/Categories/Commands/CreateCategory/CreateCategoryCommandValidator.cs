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

using Service.Catalog.Application.Common.Services;
using Service.Catalog.Domain.Categories;

namespace Service.Catalog.Application.Categories.Commands.CreateCategory
{
	/// <summary>
	/// Represents the <see cref="CreateCategoryCommand"/> validator.
	/// </summary>
	internal sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CreateCategoryCommandValidator"/> class.
		/// </summary>
		/// <param name="fileManager">The file manager.</param>
		/// <param name="repository">The category repository.</param>
		public CreateCategoryCommandValidator(IFileManager fileManager, IRepository<Category, CategoryId> repository)
		{
			RuleFor(c => c.Title)
				.NotEmpty()
					.WithError(CategoryErrors.PropertyIsRequired(nameof(CreateCategoryCommand.Title)))
				.CustomAsync(
					async (title, context, cancelationToken) =>
					{
						var exists = await repository.GetAllIgnoringQueryFiltersAsNoTracking()
										.AnyAsync(category => category.Title == title, cancelationToken);

						if (exists == true)
						{
							var error = CategoryErrors.TitleIsNotUnique(title);
							context.AddFailure(new ValidationFailure(context.PropertyName, error.Message)
							{
								ErrorCode = error.Code,
							});
						}
					});

			RuleFor(c => c.Icon)
				.NotEmpty()
					.WithError(CategoryErrors.PropertyIsRequired(nameof(CreateCategoryCommand.Icon)))
				.Must(fileManager.IsPhoto)
					.WithError(CategoryErrors.OnlyPhotoFileIsAllowed);
		}
	}
}
