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
using Service.CatalogWrite.Domain.Authors;
using Service.CatalogWrite.Domain.Books;
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain.Publishers;

namespace Service.CatalogWrite.Application.Books.Commands.CreateBook
{
	/// <summary>
	/// Represents the <see cref="CreateBookCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="CreateBookCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="bookRepository">The book repository.</param>
	/// <param name="publisherRepository">The publisher repository.</param>
	/// <param name="authorRepository">The author repository.</param>
	/// <param name="categoryRepository">The category repository.</param>
	internal sealed class CreateBookCommandHandler(
		ICatalogDb db,
		IBookRepository bookRepository,
		IRepository<Publisher, PublisherId> publisherRepository,
		IRepository<Author, AuthorId> authorRepository,
		IRepository<Category, CategoryId> categoryRepository)
		: ICommandHandler<CreateBookCommand, Guid>
	{
		/// <inheritdoc/>
		public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
		{
			Publisher? publisher = null;

			if (request.PublisherId is not null)
			{
				publisher = await publisherRepository.GetAll()
									.FirstOrDefaultAsync(p => p.Id == request.PublisherId, cancellationToken);

				if (publisher is null)
					return Result.Failure<Guid>(Publishers.PublisherErrors.NotFound(request.PublisherId));
			}

			var authors = request.AuthorIds is null ? null : await authorRepository.GetAll()
										.Where(a => request.AuthorIds.Contains(a.Id))
										.ToListAsync(cancellationToken);

			var categories = request.CategoryIds is null ? null : await categoryRepository.GetAll()
										.Where(c => request.CategoryIds.Contains(c.Id))
										.ToListAsync(cancellationToken);

			return await Book.CreateAsync(request.Title,
											request.ISBN,
											request.Language,
											request.AgeRating,
											authors!,
											categories!,
											bookRepository,
											request.Description,
											publisher,
											request.PublishedDate,
											cancellationToken)
								.Tap(book => bookRepository.Create(book))
								.Tap(() => db.SaveChangesAsync(cancellationToken))
								.Map(book => book.Id.Value);
		}
	}
}
