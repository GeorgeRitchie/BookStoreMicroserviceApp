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
using Service.CatalogWrite.Application.Publishers.Commands.SetPublisherImage;
using Service.CatalogWrite.Domain.Publishers;
using Service.CatalogWrite.Endpoints.Contracts.Publishers;

namespace Service.CatalogWrite.Endpoints.Endpoints.Publishers
{
	/// <summary>
	/// Represents publisher image setup endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="SetPublisherImageEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class SetPublisherImageEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<SetPublisherImageRequest>
		.WithActionResult
	{
		// TODO [Authorize]
		[HttpPut(PublisherRoutes.SetImage)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Sets images to publisher.",
			Description = "Sets images to publisher profile based on the specified request.",
			Tags = [PublisherRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromForm] SetPublisherImageRequest request,
															CancellationToken cancellationToken = default) =>
			await Result.Success(request)
					.Map(r => new SetPublisherImageCommand
					{
						Id = new PublisherId(r.Id),
						Icon = r.Icon != null ? new FormFileWrapper(r.Icon) : null,
						Photo = r.Photo != null ? new FormFileWrapper(r.Photo) : null,
						Others = r.OtherImages?.Any() == true ? r.OtherImages.Select(f => new FormFileWrapper(f)) : null,
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(NoContent, this.HandleFailure);
	}
}
