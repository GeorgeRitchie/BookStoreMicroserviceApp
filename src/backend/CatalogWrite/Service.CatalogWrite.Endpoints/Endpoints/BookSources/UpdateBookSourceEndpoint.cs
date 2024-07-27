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

using Service.CatalogWrite.Application.BooSources.Commands.UpdateBookSource;

namespace Service.CatalogWrite.Endpoints.Endpoints.BookSources
{
	/// <summary>
	/// Represents book source update endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="UpdateBookSourceEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class UpdateBookSourceEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<UpdateBookSourceCommand>
		.WithActionResult
	{
		// TODO [Authorize]
		[HttpPut(BookSourceRoutes.Update)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Updates book source information.",
			Description = "Updates book source information based on the specified request.",
			Tags = [BookSourceRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] UpdateBookSourceCommand command,
															CancellationToken cancellationToken = default) =>
			await sender.Send(command, cancellationToken)
					.Match(NoContent, this.HandleFailure);
	}
}
