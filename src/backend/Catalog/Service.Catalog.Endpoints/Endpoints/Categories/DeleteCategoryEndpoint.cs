﻿/* 
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

using Service.Catalog.Application.Categories.Commands.DeleteCategory;
using Service.Catalog.Domain.Categories;

namespace Service.Catalog.Endpoints.Endpoints.Categories
{
	/// <summary>
	/// Represents category delete endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="DeleteCategoryEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class DeleteCategoryEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<Guid>
		.WithActionResult
	{
		[Authorize]
		[HasPermission(CatalogPermissions.EditCategories)]
		[HttpDelete(CategoryRoutes.Delete)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Deletes category.",
			Description = "Deletes category based on the specified request.",
			Tags = [CategoryRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] Guid request,
															CancellationToken cancellationToken = default) =>
			await Result.Success(request)
					.Map(r => new DeleteCategoryCommand
					{
						CategoryId = new CategoryId(r),
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(NoContent, this.HandleFailure);
	}
}
