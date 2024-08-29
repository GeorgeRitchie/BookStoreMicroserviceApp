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
using Service.Catalog.Domain.BookSources;

namespace Service.Catalog.Application.BooSources.Commands.IncreaseQuantity
{
	/// <summary>
	/// Represents the <see cref="IncreaseQuantityCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="IncreaseQuantityCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="sourceRepository">The book source repository.</param>
	internal sealed class IncreaseQuantityCommandHandler(
		ICatalogDb db,
		IBookSourceRepository sourceRepository)
		: ICommandHandler<IncreaseQuantityCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(IncreaseQuantityCommand request, CancellationToken cancellationToken)
		{
			var sources = sourceRepository.GetAll().Where(i => request.BookSources.Any(o => o.Key == i.Id)).ToList();

			foreach (var item in request.BookSources)
			{
				var source = sources.FirstOrDefault(i => i.Id == item.Key);

				if (source != null)
				{
					source.Update(source.Url, (uint)source.StockQuantity + item.Value, source.Price, source.PreviewUrl);
					sourceRepository.Update(source);
				}
			}

			await db.SaveChangesAsync(cancellationToken);
			return Result.Success();
		}
	}
}
