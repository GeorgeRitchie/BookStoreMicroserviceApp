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

using Service.Catalog.Domain;
using Service.Catalog.Domain.Authors;
using Service.Catalog.Domain.ValueObjects;

namespace Service.Catalog.Application.Authors.Commands.UpdateAuthor
{
	/// <summary>
	/// Represents the <see cref="UpdateAuthorCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="UpdateAuthorCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="authorRepository">The author repository.</param>
	internal sealed class UpdateAuthorCommandHandler(
		ICatalogDb db,
		IRepository<Author, AuthorId> authorRepository)
		: ICommandHandler<UpdateAuthorCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
		{
			var author = await authorRepository.GetAll()
											.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

			if (author == null)
				return Result.Failure(AuthorErrors.NotFound(request.Id));

			var firstName = request.FirstName ?? author.FirstName;
			var lastName = request.LastName ?? author.LastName;
			var description = request.Description ?? author.Description;

			var emailCreateResult = string.IsNullOrEmpty(request.Email) ? null : Email.Create(request.Email);
			var websiteCreateResult = string.IsNullOrEmpty(request.Site) ? null : Website.Create(request.Site);

			var result = await Result.Combine(
					emailCreateResult ?? Result.Success(),
					websiteCreateResult ?? Result.Success()
				)
				.Bind(() => author.Change(firstName,
										lastName,
										description,
										request.Email is null ? author.Email : emailCreateResult?.Value,
										request.Site is null ? author.Website : websiteCreateResult?.Value))
				.Tap<Author>(authorRepository.Update)
				.Tap(() => db.SaveChangesAsync(cancellationToken));

			return result;
		}
	}
}
