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

using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain;

namespace Service.CatalogWrite.Application.Categories.Commands.RestoreCategory
{
	/// <summary>
	/// Represents the <see cref="RestoreCategoryCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RestoreCategoryCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The category repository.</param>
	internal sealed class RestoreCategoryCommandHandler(
		ICatalogDb db,
		IRepository<Category, CategoryId> repository)
		: ICommandHandler<RestoreCategoryCommand>
	{
		public async Task<Result> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken) =>
			await Result.Create(
					await repository.GetAllIgnoringQueryFilters()
									.FirstOrDefaultAsync(i => i.Id == request.CategoryId, cancellationToken))
				.MapFailure(() => CategoryErrors.NotFound(request.CategoryId))
				.Tap(c => c.RestoreDeleted())
				.Tap(() => db.SaveChangesAsync(cancellationToken));
	}
}
