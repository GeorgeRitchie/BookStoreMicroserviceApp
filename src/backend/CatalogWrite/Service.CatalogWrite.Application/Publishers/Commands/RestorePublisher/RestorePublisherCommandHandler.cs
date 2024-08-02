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
using Service.Catalog.Domain.Publishers;

namespace Service.Catalog.Application.Publishers.Commands.RestorePublisher
{
	/// <summary>
	/// Represents the <see cref="RestorePublisherCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RestorePublisherCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The publisher repository.</param>
	internal sealed class RestorePublisherCommandHandler(
		ICatalogDb db,
		IRepository<Publisher, PublisherId> repository)
		: ICommandHandler<RestorePublisherCommand>
	{
		public async Task<Result> Handle(RestorePublisherCommand request, CancellationToken cancellationToken) =>
			await Result.Create(
					await repository.GetAllIgnoringQueryFilters()
									.FirstOrDefaultAsync(i => i.Id == request.PublisherId, cancellationToken))
				.MapFailure(() => PublisherErrors.NotFound(request.PublisherId))
				.Tap(c => c.RestoreDeleted())
				.Tap(() => db.SaveChangesAsync(cancellationToken));
	}
}
