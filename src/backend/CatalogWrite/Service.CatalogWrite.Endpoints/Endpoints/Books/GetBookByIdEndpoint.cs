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

using Service.CatalogWrite.Application.Books.Queries.GetBookById;
using Service.CatalogWrite.Domain.Books;

namespace Service.CatalogWrite.Endpoints.Endpoints.Books
{
	/// <summary>
	/// Represents get book by identifier endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetBookByIdEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetBookByIdEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<Guid>
		.WithActionResult<BookDto>
	{
		[AllowAnonymous]
		[HttpGet(BookRoutes.GetById, Name = nameof(GetBookByIdEndpoint))]
		[ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets the book by id.",
			Description = "Gets the book with the specified identifier.",
			Tags = new[] { BookRoutes.Tag })]
		public override async Task<ActionResult<BookDto>> HandleAsync([FromQuery] Guid bookId,
															CancellationToken cancellationToken = default)
			=> await sender.Send(new GetBookByIdQuery(new BookId(bookId)), cancellationToken)
				.Match(Ok, this.HandleFailure);
	}
}
