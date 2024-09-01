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

using Endpoints.Models;
using Service.Catalog.Application.Categories.Commands.CreateCategory;
using Service.Catalog.Endpoints.Contracts.Categories;

namespace Service.Catalog.Endpoints.Endpoints.Categories
{
	/// <summary>
	/// Represents create category endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="CreateCategoryEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class CreateCategoryEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<CreateCategoryRequest>
		.WithActionResult<Guid>
	{
		[Authorize]
		[HasPermission(CatalogPermissions.EditCategories)]
		[HttpPost(CategoryRoutes.Create)]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Creates a new category.",
			Description = "Creates a new category based on the specified request.",
			Tags = [CategoryRoutes.Tag])]
		public override async Task<ActionResult<Guid>> HandleAsync([FromForm] CreateCategoryRequest request,
																	CancellationToken cancellationToken = default) =>
			await Result.Create(request)
				.Map(r => new CreateCategoryCommand
				{
					Title = r.Title,
					Description = r.Description,
					Icon = r.Icon != null ? new FormFileWrapper(r.Icon) : null,
				})
				.Bind(command => sender.Send(command, cancellationToken))
				.Match(categoryId => CreatedAtRoute(nameof(GetCategoryByIdEndpoint), new { categoryId }, categoryId),
									 this.HandleFailure);
	}
}
