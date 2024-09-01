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
using Service.Catalog.Application.Authors.Commands.SetAuthorImage;
using Service.Catalog.Domain.Authors;
using Service.Catalog.Endpoints.Contracts.Authors;

namespace Service.Catalog.Endpoints.Endpoints.Authors
{
	/// <summary>
	/// Represents author image setup endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="SetAuthorImageEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class SetAuthorImageEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<SetAuthorImageRequest>
		.WithActionResult
	{
		[Authorize]
		[HasPermission(CatalogPermissions.EditAuthors)]
		[HttpPut(AuthorRoutes.SetImage)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Sets images to author.",
			Description = "Sets images to author profile based on the specified request.",
			Tags = [AuthorRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromForm] SetAuthorImageRequest request,
															CancellationToken cancellationToken = default) =>
			await Result.Success(request)
					.Map(r => new SetAuthorImageCommand
					{
						Id = new AuthorId(r.Id),
						Icon = r.Icon != null ? new FormFileWrapper(r.Icon) : null,
						Photo = r.Photo != null ? new FormFileWrapper(r.Photo) : null,
						Others = r.OtherImages?.Any() == true ? r.OtherImages.Select(f => new FormFileWrapper(f)) : null,
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(NoContent, this.HandleFailure);
	}
}
