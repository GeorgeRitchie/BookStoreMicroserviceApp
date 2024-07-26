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

using Service.CatalogWrite.Application.Books.Commands.AddAuthor;

namespace Service.CatalogWrite.Endpoints.Endpoints.Books
{
	/// <summary>
	/// Represents add author to book endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="AddAuthorToBookEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class AddAuthorToBookEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<AddAuthorCommand>
		.WithActionResult
	{
		// TODO [Authorize]
		[HttpPut(BookRoutes.AddAuthor)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Adds author to book.",
			Description = "Adds specified author to specified book.",
			Tags = [BookRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] AddAuthorCommand command,
															CancellationToken cancellationToken = default) =>
			await sender.Send(command, cancellationToken)
					.Match(NoContent, this.HandleFailure);
	}
}
