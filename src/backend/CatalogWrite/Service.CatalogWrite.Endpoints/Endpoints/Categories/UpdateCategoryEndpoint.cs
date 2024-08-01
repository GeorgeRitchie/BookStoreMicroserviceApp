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
using Service.CatalogWrite.Application.Categories.Commands.UpdateCategory;
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Endpoints.Contracts.Categories;

namespace Service.CatalogWrite.Endpoints.Endpoints.Categories
{
	/// <summary>
	/// Represents category update endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="UpdateCategoryEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class UpdateCategoryEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<UpdateCategoryRequest>
		.WithActionResult
	{
		// TODO [Authorize]
		[HttpPut(CategoryRoutes.Update)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Updates category information.",
			Description = "Updates category information based on the specified request.",
			Tags = [CategoryRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromForm] UpdateCategoryRequest request,
															CancellationToken cancellationToken = default) =>
			await Result.Success(request)
					.Map(r => new UpdateCategoryCommand
					{
						Id = new CategoryId(r.Id),
						Title = r.Title,
						Description = r.Description,
						Icon = r.Icon != null ? new FormFileWrapper(r.Icon) : null,
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(NoContent, this.HandleFailure);
	}
}
