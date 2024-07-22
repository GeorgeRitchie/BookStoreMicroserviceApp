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


using Service.CatalogWrite.Domain.Authors;
using Service.CatalogWrite.Domain;
using Service.CatalogWrite.Domain.ImageSources;

namespace Service.CatalogWrite.Application.Authors.Commands.RemoveAuthorImage
{
	/// <summary>
	/// Represents the <see cref="RemoveAuthorImageCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RemoveAuthorImageCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="authorRepository">The author repository.</param>
	/// <param name="imgRepository">The image repository.</param>
	/// <param name="fileManager">The file manager.</param>
	internal sealed class RemoveAuthorImageCommandHandler(
		ICatalogDb db,
		IRepository<Author, AuthorId> authorRepository,
		IRepository<ImageSource<AuthorImageType>, ImageSourceId> imgRepository,
		IFileManager fileManager)
		: ICommandHandler<RemoveAuthorImageCommand>
	{
		public async Task<Result> Handle(RemoveAuthorImageCommand request, CancellationToken cancellationToken)
		{
			var author = await authorRepository.GetAll()
											.Include(a => a.Images)
											.FirstOrDefaultAsync(c => c.Id == request.AuthorId, cancellationToken);

			if (author == null)
				return Result.Failure(AuthorErrors.NotFound(request.AuthorId));

			if (request.ImageIds?.Count > 0)
			{
				List<Result<ImageSource<AuthorImageType>>> result = [];

				request.ImageIds.ForEach(i =>
				{
					var imageToDelete = author.Images.FirstOrDefault(o => o.Id == i);
					if (imageToDelete is not null)
						result.Add(Result.Success(imageToDelete));
					else
						result.Add(Result.Failure<ImageSource<AuthorImageType>>(
																AuthorErrors.AuthorImageNotFound(author.Id, i)));
				});

				return await Result.Combine(result.ToArray())
					.Tap(async () =>
					{
						var images = result.Select(i => i.Value);
						await RemoveFromDb(images!, cancellationToken);
						await images.Tap(o => o.ForEachElement(i => RemoveFiles(i!.Source, cancellationToken)));
					});
			}

			return Result.Success();
		}

		private async Task RemoveFromDb(IEnumerable<ImageSource<AuthorImageType>> images, CancellationToken cancellationToken)
			=> await images.Tap(imgRepository.DeleteRange)
							.Tap(() => db.SaveChangesAsync(cancellationToken));

		private async Task RemoveFiles(string? source, CancellationToken cancellationToken = default)
		{
			if (source is not null)
				await fileManager.DeleteAsync(source, cancellationToken);
		}
	}
}
