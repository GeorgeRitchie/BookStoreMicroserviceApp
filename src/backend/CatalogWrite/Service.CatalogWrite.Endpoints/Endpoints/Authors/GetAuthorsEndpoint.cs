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

using Service.CatalogWrite.Application.Authors.Queries.GetAuthors;

namespace Service.CatalogWrite.Endpoints.Endpoints.Authors
{
	/// <summary>
	/// Represents get authors endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetAuthorsEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetAuthorsEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<GetAuthorsQuery>
		.WithActionResult<IEnumerable<AuthorDto>>
	{
		[AllowAnonymous]
		[HttpGet(AuthorRoutes.GetAll)]
		[ProducesResponseType(typeof(IEnumerable<AuthorDto>), StatusCodes.Status200OK)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets authors.",
			Description = "Gets authors.",
			Tags = new[] { AuthorRoutes.Tag })]
		public override async Task<ActionResult<IEnumerable<AuthorDto>>> HandleAsync(
			[FromQuery] GetAuthorsQuery request,
			CancellationToken cancellationToken = default) =>
			await sender.Send(request, cancellationToken).Match(Ok, this.HandleFailure);
	}
}
