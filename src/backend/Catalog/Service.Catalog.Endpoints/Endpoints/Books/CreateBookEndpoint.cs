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

using Service.Catalog.Application.Books.Commands.CreateBook;
using Service.Catalog.Domain.Authors;
using Service.Catalog.Domain.Categories;
using Service.Catalog.Domain.Publishers;
using Service.Catalog.Endpoints.Contracts.Books;

namespace Service.Catalog.Endpoints.Endpoints.Books
{
	/// <summary>
	/// Represents create book endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="CreateBookEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class CreateBookEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<CreateBookRequest>
		.WithActionResult<Guid>
	{
		// TODO [Authorize]
		[HttpPost(BookRoutes.Create)]
		[ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Creates a new book.",
			Description = "Creates a new book based on the specified request.",
			Tags = [BookRoutes.Tag])]
		public override async Task<ActionResult<Guid>> HandleAsync([FromBody] CreateBookRequest request,
																CancellationToken cancellationToken = default) =>
			await Result.Create(request)
				.Map(r => new CreateBookCommand
				{
					Title = r.Title,
					ISBN = r.ISBN,
					AgeRating = r.AgeRating,
					Language = r.Language,
					Description = r.Description,
					PublishedDate = r.PublishedDate,
					PublisherId = r.PublisherId is null ? null : new PublisherId((Guid)r.PublisherId),
					AuthorIds = r.AuthorIds.Select(id => new AuthorId(id)).ToList(),
					CategoryIds = r.CategoryIds.Select(id => new CategoryId(id)).ToList(),
				})
				.Bind(command => sender.Send(command, cancellationToken))
				.Match(bookId => CreatedAtRoute(nameof(GetBookByIdEndpoint), new { bookId }, bookId),
									 this.HandleFailure);
	}
}
