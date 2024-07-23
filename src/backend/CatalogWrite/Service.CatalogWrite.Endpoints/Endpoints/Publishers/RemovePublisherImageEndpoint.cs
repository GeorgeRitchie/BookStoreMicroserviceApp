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

using Service.CatalogWrite.Application.Publishers.Commands.RemovePublisherImage;
using Service.CatalogWrite.Domain.ImageSources;
using Service.CatalogWrite.Domain.Publishers;
using Service.CatalogWrite.Endpoints.Contracts.Publishers;

namespace Service.CatalogWrite.Endpoints.Endpoints.Publishers
{
	/// <summary>
	/// Represents publisher image remove endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="RemovePublisherImageEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class RemovePublisherImageEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<RemovePublisherImageRequest>
		.WithActionResult
	{
		// TODO [Authorize]
		[HttpPut(PublisherRoutes.RemoveImage)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Removes images from publisher.",
			Description = "Removes images from publisher profile based on the specified request.",
			Tags = [PublisherRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] RemovePublisherImageRequest request,
															CancellationToken cancellationToken = default) =>
			await Result.Success(request)
					.Map(r => new RemovePublisherImageCommand
					{
						PublisherId = new PublisherId(r.PublisherId),
						ImageIds = request.ImageIds?.Select(i => new ImageSourceId(i)).ToList(),
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(NoContent, this.HandleFailure);
	}
}
