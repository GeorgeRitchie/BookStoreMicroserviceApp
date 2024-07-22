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

using Service.CatalogWrite.Domain.ImageSources;
using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.Authors;
using Service.CatalogWrite.Domain.ValueObjects;

namespace Service.CatalogWrite.Application.Authors.Commands.CreateAuthor
{
	/// <summary>
	/// Represents the <see cref="CreateAuthorCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="CreateAuthorCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The author repository.</param>
	internal sealed class CreateAuthorCommandHandler(
		ICatalogDb db,
		IRepository<Author, AuthorId> repository)
		: ICommandHandler<CreateAuthorCommand, Guid>
	{
		/// <inheritdoc/>
		public async Task<Result<Guid>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
		{
			var email = request.Email is null ? null : Email.Create(request.Email);
			var website = request.Site is null ? null : Website.Create(request.Site);

			return await Result.Combine(
							email ?? Result.Success(),
							website ?? Result.Success())
						.Bind(() => Author.Create(request.FirstName,
												request.LastName,
												request.Description,
												email?.Value,
												website?.Value))
						.Tap<Author>(author => repository.Create(author))
						.Tap(() => db.SaveChangesAsync(cancellationToken))
						.Map(author => author.Id.Value);
		}
	}
}
