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
using Service.Catalog.Application.Books.Commands.SetBookImage;
using Service.Catalog.Domain.Books;
using Service.Catalog.Endpoints.Contracts.Books;

namespace Service.Catalog.Endpoints.Endpoints.Books
{
	/// <summary>
	/// Represents book image setup endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="SetBookImageEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class SetBookImageEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<SetBookImageRequest>
		.WithActionResult
	{
		[Authorize]
		[HasPermission(CatalogPermissions.EditBooks)]
		[HttpPut(BookRoutes.SetImage)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Sets images to book.",
			Description = "Sets images to book based on the specified request.",
			Tags = [BookRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromForm] SetBookImageRequest request,
															CancellationToken cancellationToken = default) =>
			await Result.Success(request)
					.Map(r => new SetBookImageCommand
					{
						Id = new BookId(r.Id),
						Icon = r.Icon != null ? new FormFileWrapper(r.Icon) : null,
						Cover = r.Cover != null ? new FormFileWrapper(r.Cover) : null,
						Previews = r.Previews?.Any() == true ? r.Previews.Select(f => new FormFileWrapper(f)) : null,
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(NoContent, this.HandleFailure);
	}
}
