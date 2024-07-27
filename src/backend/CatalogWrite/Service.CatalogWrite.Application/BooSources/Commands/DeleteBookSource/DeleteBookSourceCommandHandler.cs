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

using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.BookSources;

namespace Service.CatalogWrite.Application.BooSources.Commands.DeleteBookSource
{
	/// <summary>
	/// Represents the <see cref="DeleteBookSourceCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="DeleteBookSourceCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The book source repository.</param>
	internal sealed class DeleteBookSourceCommandHandler(
		ICatalogDb db,
		IBookSourceRepository repository)
		: ICommandHandler<DeleteBookSourceCommand>
	{
		public async Task<Result> Handle(DeleteBookSourceCommand request, CancellationToken cancellationToken) =>
			await Result.Create(
					await repository.GetAll().FirstOrDefaultAsync(i => i.Id == request.BookSourceId, cancellationToken))
				.MapFailure(() => BookSourceErrors.NotFound(request.BookSourceId))
				.Tap<BookSource>(repository.Delete)
				.Tap(() => db.SaveChangesAsync(cancellationToken));
	}
}
