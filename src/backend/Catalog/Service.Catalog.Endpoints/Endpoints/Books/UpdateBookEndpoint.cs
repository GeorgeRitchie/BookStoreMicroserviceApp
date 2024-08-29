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

using Service.Catalog.Application.Books.Commands.UpdateBook;
using Service.Catalog.Domain.Books;
using Service.Catalog.Domain.Publishers;
using Service.Catalog.Endpoints.Contracts.Books;

namespace Service.Catalog.Endpoints.Endpoints.Books
{
	/// <summary>
	/// Represents book update endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="UpdateBookEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class UpdateBookEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<UpdateBookRequest>
		.WithActionResult
	{
		// TODO [Authorize]
		[HttpPut(BookRoutes.Update)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Updates book information.",
			Description = "Updates book information based on the specified request.",
			Tags = [BookRoutes.Tag])]
		public override async Task<ActionResult> HandleAsync([FromBody] UpdateBookRequest request,
															CancellationToken cancellationToken = default) =>
			await Result.Success(request)
					.Map(r => new UpdateBookCommand
					{
						Id = new BookId(r.Id),
						Title = r.Title,
						Description = r.Description,
						ISBN = r.ISBN,
						PublishedDate = r.PublishedDate,
						AgeRating = r.AgeRating,
						Language = r.Language,
						PublisherId = r.PublisherId is null ? null : new PublisherId((Guid)r.PublisherId),
					})
					.Bind(command => sender.Send(command, cancellationToken))
					.Match(NoContent, this.HandleFailure);
	}
}
