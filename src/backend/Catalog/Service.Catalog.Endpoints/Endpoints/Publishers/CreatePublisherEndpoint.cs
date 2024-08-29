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

using Service.Catalog.Application.Publishers.Commands.CreatePublisher;

namespace Service.Catalog.Endpoints.Endpoints.Publishers
{
	/// <summary>
	/// Represents create publisher endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="CreatePublisherEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class CreatePublisherEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<CreatePublisherCommand>
		.WithActionResult<Guid>
	{
		// TODO [Authorize]
		[HttpPost(PublisherRoutes.Create)]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Creates a new publisher.",
			Description = "Creates a new publisher based on the specified request.",
			Tags = [PublisherRoutes.Tag])]
		public override async Task<ActionResult<Guid>> HandleAsync([FromBody] CreatePublisherCommand command,
																CancellationToken cancellationToken = default) =>
			await sender.Send(command, cancellationToken)
				.Match(publisherId => CreatedAtRoute(nameof(GetPublisherByIdEndpoint),
													new { publisherId },
													publisherId),
									 this.HandleFailure);
	}
}
