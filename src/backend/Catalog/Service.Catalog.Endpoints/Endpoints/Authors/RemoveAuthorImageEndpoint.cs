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

using Service.Catalog.Application.Authors.Commands.RemoveAuthorImage;
using Service.Catalog.Domain.Authors;
using Service.Catalog.Domain.ImageSources;
using Service.Catalog.Endpoints.Contracts.Authors;

namespace Service.Catalog.Endpoints.Endpoints.Authors
{
	/// <summary>
	/// Represents author image remove endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="RemoveAuthorImageEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class RemoveAuthorImageEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<RemoveAuthorImageRequest>
		.WithActionResult
	{
		// TODO [Authorize]
		[HttpPut(AuthorRoutes.RemoveImage)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Removes images from author.",
			Description = "Removes images from author profile based on the specified request.",
			Tags = [AuthorRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] RemoveAuthorImageRequest request,
															CancellationToken cancellationToken = default) =>
			await Result.Success(request)
					.Map(r => new RemoveAuthorImageCommand
					{
						AuthorId = new AuthorId(r.AuthorId),
						ImageIds = request.ImageIds?.Select(i => new ImageSourceId(i)).ToList(),
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(NoContent, this.HandleFailure);
	}
}
