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

using Service.Catalog.Application.Authors.Queries.GetAuthorById;
using Service.Catalog.Domain.Authors;

namespace Service.Catalog.Endpoints.Endpoints.Authors
{
	/// <summary>
	/// Represents get author by identifier endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetAuthorByIdEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetAuthorByIdEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<Guid>
		.WithActionResult<AuthorDto>
	{
		[AllowAnonymous]
		[HttpGet(AuthorRoutes.GetById, Name = nameof(GetAuthorByIdEndpoint))]
		[ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets the author by id.",
			Description = "Gets the author with the specified identifier.",
			Tags = new[] { AuthorRoutes.Tag })]
		public override async Task<ActionResult<AuthorDto>> HandleAsync([FromQuery] Guid authorId,
																	CancellationToken cancellationToken = default) =>
			await sender.Send(new GetAuthorByIdQuery(new AuthorId(authorId)), cancellationToken)
				.Match(Ok, this.HandleFailure);
	}
}
