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
using Service.CatalogWrite.Domain.Books;
using Service.CatalogWrite.Domain.Publishers;

namespace Service.CatalogWrite.Application.Books.Commands.UpdateBook
{
	/// <summary>
	/// Represents the <see cref="UpdateBookCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="UpdateBookCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="bookRepository">The book repository.</param>
	/// <param name="publisherRepository">The publisher repository.</param>
	internal sealed class UpdateBookCommandHandler(
		ICatalogDb db,
		IBookRepository bookRepository,
		IRepository<Publisher, PublisherId> publisherRepository)
		: ICommandHandler<UpdateBookCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
		{
			var book = await bookRepository.GetAll()
											.Include(x => x.Publisher)
											.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

			if (book == null)
				return Result.Failure(BookErrors.NotFound(request.Id));

			var publisher = book.Publisher;
			if (request.PublisherId is not null)
			{
				if (request.PublisherId.Value != Guid.Empty)
				{
					publisher = await publisherRepository.GetAll()
										.FirstOrDefaultAsync(c => c.Id == request.PublisherId, cancellationToken);

					if (publisher == null)
						return Result.Failure(Publishers.PublisherErrors.NotFound(request.PublisherId));
				}
				else
					publisher = null;
			}

			var title = request.Title ?? book.Title;
			var isbn = request.ISBN ?? book.ISBN;
			var description = request.Description ?? book.Description;
			var language = request.Language ?? book.Language;
			var ageRating = request.AgeRating ?? book.AgeRating;
			var publishedDate = request.PublishedDate ?? book.PublishedDate;

			var result = await book.UpdateAsync(title,
												isbn,
												language,
												ageRating,
												bookRepository,
												description,
												publisher,
												publishedDate,
												cancellationToken)
									.Tap(bookRepository.Update)
									.Tap(() => db.SaveChangesAsync(cancellationToken));

			return result;
		}
	}
}
