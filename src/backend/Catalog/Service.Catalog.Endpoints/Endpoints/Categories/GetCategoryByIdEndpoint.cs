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

using Service.Catalog.Application.Categories.Queries.GetCategoryById;
using Service.Catalog.Domain.Categories;

namespace Service.Catalog.Endpoints.Endpoints.Categories
{
	/// <summary>
	/// Represents get category by identifier endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetCategoryByIdEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetCategoryByIdEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<Guid>
		.WithActionResult<CategoryDto>
	{
		[AllowAnonymous]
		[HttpGet(CategoryRoutes.GetById, Name = nameof(GetCategoryByIdEndpoint))]
		[ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets the category by id.",
			Description = "Gets the category with the specified identifier.",
			Tags = new[] { CategoryRoutes.Tag })]
		public override async Task<ActionResult<CategoryDto>> HandleAsync([FromQuery] Guid categoryId, CancellationToken cancellationToken = default)
		=> await sender.Send(new GetCategoryByIdQuery(new CategoryId(categoryId)), cancellationToken)
			.Match(Ok, this.HandleFailure);
	}
}
