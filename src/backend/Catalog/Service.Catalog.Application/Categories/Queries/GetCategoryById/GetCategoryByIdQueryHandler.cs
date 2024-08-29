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

using Service.Catalog.Domain.Categories;

namespace Service.Catalog.Application.Categories.Queries.GetCategoryById
{
	/// <summary>
	/// Represents the <see cref="GetCategoryByIdQuery"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes new instance of the <see cref="GetCategoryByIdQueryHandler"/> class.
	/// </remarks>
	/// <param name="repository">The category repository.</param>
	/// <param name="mapper">The auto mapper.</param>
	internal sealed class GetCategoryByIdQueryHandler(IRepository<Category, CategoryId> repository, IMapper mapper)
		: IQueryHandler<GetCategoryByIdQuery, CategoryDto>
	{
		public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken) =>
			Result.Create(
					await repository.GetAll()
									.Include(c => c.Icon)
									.FirstOrDefaultAsync(i => i.Id == request.CategoryId, cancellationToken))
				.Map(mapper.Map<CategoryDto>)
				.MapFailure(() => CategoryErrors.NotFound(request.CategoryId));
	}
}
