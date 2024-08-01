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

using Service.CatalogWrite.Application.Authors.Commands.UpdateAuthor;
using Service.CatalogWrite.Domain.Authors;
using Service.CatalogWrite.Endpoints.Contracts.Authors;

namespace Service.CatalogWrite.Endpoints.Endpoints.Authors
{
	/// <summary>
	/// Represents author update endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="UpdateAuthorEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class UpdateAuthorEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<UpdateAuthorRequest>
		.WithActionResult
	{
		// TODO [Authorize]
		[HttpPut(AuthorRoutes.Update)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Updates author information.",
			Description = "Updates author information based on the specified request.",
			Tags = [AuthorRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] UpdateAuthorRequest request,
															CancellationToken cancellationToken = default) =>
			await Result.Success(request)
					.Map(r => new UpdateAuthorCommand
					{
						Id = new AuthorId(r.Id),
						FirstName = r.FirstName,
						LastName = r.LastName,
						Description = r.Description,
						Email = r.Email,
						Site = r.Site,
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(NoContent, this.HandleFailure);
	}
}
