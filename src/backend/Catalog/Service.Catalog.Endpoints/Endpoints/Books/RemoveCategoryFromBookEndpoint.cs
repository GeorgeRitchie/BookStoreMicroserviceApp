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

using Service.Catalog.Application.Books.Commands.RemoveCategory;

namespace Service.Catalog.Endpoints.Endpoints.Books
{
	/// <summary>
	/// Represents remove category from book endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="RemoveCategoryFromBookEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class RemoveCategoryFromBookEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<RemoveCategoryCommand>
		.WithActionResult
	{
		[Authorize]
		[HasPermission(CatalogPermissions.EditBooks)]
		[HttpPut(BookRoutes.RemoveCategory)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Removes category from book.",
			Description = "Removes specified category from specified book.",
			Tags = [BookRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] RemoveCategoryCommand command,
															CancellationToken cancellationToken = default) =>
			await sender.Send(command, cancellationToken)
					.Match(NoContent, this.HandleFailure);
	}
}
