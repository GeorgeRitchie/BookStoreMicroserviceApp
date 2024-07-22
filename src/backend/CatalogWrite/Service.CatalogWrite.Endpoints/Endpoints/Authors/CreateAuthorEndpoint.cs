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
using Service.CatalogWrite.Application.Authors.Commands.CreateAuthor;
using Service.CatalogWrite.Endpoints.Contracts.Authors;

namespace Service.CatalogWrite.Endpoints.Endpoints.Authors
{
	/// <summary>
	/// Represents create author endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="CreateAuthorEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class CreateAuthorEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<CreateAuthorRequest>
		.WithActionResult<Guid>
	{
		// TODO [Authorize]
		[HttpPost(AuthorRoutes.Create)]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Creates a new author.",
			Description = "Creates a new author based on the specified request.",
			Tags = [AuthorRoutes.Tag])]
		public override async Task<ActionResult<Guid>> HandleAsync([FromForm] CreateAuthorRequest request,
																	CancellationToken cancellationToken = default) =>
			await Result.Create(request)
				.Map(r => new CreateAuthorCommand
				{
					FirstName = r.FirstName,
					LastName = r.LastName,
					Description = r.Description,
					Email = r.Email,
					Site = r.Site,
				})
				.Bind(command => sender.Send(command, cancellationToken))
				.Match(authorId => CreatedAtRoute(nameof(GetAuthorByIdEndpoint), new { authorId }, authorId),
									 this.HandleFailure);
	}
}
