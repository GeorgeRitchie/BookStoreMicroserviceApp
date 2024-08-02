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

namespace Service.Catalog.Application.Publishers.Commands.DeletePublisher
{
	/// <summary>
	/// Represents the <see cref="DeletePublisherCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="DeletePublisherCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The publisher repository.</param>
	internal sealed class DeletePublisherCommandHandler(
		ICatalogDb db,
		IRepository<Publisher, PublisherId> repository)
		: ICommandHandler<DeletePublisherCommand>
	{
		public async Task<Result> Handle(DeletePublisherCommand request, CancellationToken cancellationToken) =>
			await Result.Create(
					await repository.GetAll().FirstOrDefaultAsync(i => i.Id == request.PublisherId, cancellationToken))
				.MapFailure(() => PublisherErrors.NotFound(request.PublisherId))
				.Tap<Publisher>(repository.Delete)
				.Tap(() => db.SaveChangesAsync(cancellationToken));
	}
}
