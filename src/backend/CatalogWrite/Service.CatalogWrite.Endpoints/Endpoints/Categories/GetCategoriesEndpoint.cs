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

using Service.Catalog.Application.Categories.Queries.GetCategories;

namespace Service.Catalog.Endpoints.Endpoints.Categories
{
	/// <summary>
	/// Represents get categories endpoint.
	/// </summary>
	/// <remarks>
	/// Initiates a new instance of the <see cref="GetCategoriesEndpoint"/> class.
	/// </remarks>
	/// <param name="sender">Mediator request sender.</param>
	public sealed class GetCategoriesEndpoint(ISender sender) : EndpointBaseAsync
		.WithRequest<GetCategoriesQuery>
		.WithActionResult<IEnumerable<CategoryDto>>
	{
		[AllowAnonymous]
		[HttpGet(CategoryRoutes.GetAll)]
		[ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
		[ApiVersion("1.0")]
		[SwaggerOperation(
			Summary = "Gets categories.",
			Description = "Gets categories.",
			Tags = new[] { CategoryRoutes.Tag })]
		public override async Task<ActionResult<IEnumerable<CategoryDto>>> HandleAsync(
			[FromQuery] GetCategoriesQuery request,
			CancellationToken cancellationToken = default) =>
			await sender.Send(request, cancellationToken).Match(Ok, this.HandleFailure);
	}
}
