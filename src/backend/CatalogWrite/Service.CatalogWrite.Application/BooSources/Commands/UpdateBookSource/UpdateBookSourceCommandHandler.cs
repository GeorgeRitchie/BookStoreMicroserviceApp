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

namespace Service.CatalogWrite.Application.BooSources.Commands.UpdateBookSource
{
	/// <summary>
	/// Represents the <see cref="UpdateBookSourceCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="UpdateBookSourceCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The book source repository.</param>
	internal sealed class UpdateBookSourceCommandHandler(
		ICatalogDb db,
		IBookSourceRepository repository)
		: ICommandHandler<UpdateBookSourceCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(UpdateBookSourceCommand request, CancellationToken cancellationToken)
			=> await Result.Create(await repository.GetAll()
												.FirstOrDefaultAsync(i => i.Id == new BookSourceId(request.Id),
																		cancellationToken))
							.MapFailure(() => BookSourceErrors.NotFound(new BookSourceId(request.Id)))
							.Bind(bs => bs.Update(
										request.Url ?? bs.Url,
										request.Quantity ?? bs.StockQuantity ?? 0,
										request.Price ?? bs.Price,
										request.PreviewUrl ?? bs.PreviewUrl))
							.Tap<BookSource>(repository.Update)
							.Tap(() => db.SaveChangesAsync(cancellationToken));
	}
}
