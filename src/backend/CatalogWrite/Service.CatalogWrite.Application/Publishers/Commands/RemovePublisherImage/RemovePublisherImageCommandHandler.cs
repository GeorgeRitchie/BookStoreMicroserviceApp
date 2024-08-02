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

using Service.Catalog.Application.Common.Services;
using Service.Catalog.Domain;
using Service.Catalog.Domain.ImageSources;
using Service.Catalog.Domain.Publishers;

namespace Service.Catalog.Application.Publishers.Commands.RemovePublisherImage
{
	/// <summary>
	/// Represents the <see cref="RemovePublisherImageCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RemovePublisherImageCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="publisherRepository">The publisher repository.</param>
	/// <param name="imgRepository">The image repository.</param>
	/// <param name="fileManager">The file manager.</param>
	internal sealed class RemovePublisherImageCommandHandler(
		ICatalogDb db,
		IRepository<Publisher, PublisherId> publisherRepository,
		IRepository<ImageSource<PublisherImageType>, ImageSourceId> imgRepository,
		IFileManager fileManager)
		: ICommandHandler<RemovePublisherImageCommand>
	{
		public async Task<Result> Handle(RemovePublisherImageCommand request, CancellationToken cancellationToken)
		{
			var publisher = await publisherRepository.GetAll()
											.Include(a => a.Images)
											.FirstOrDefaultAsync(c => c.Id == request.PublisherId, cancellationToken);

			if (publisher == null)
				return Result.Failure(PublisherErrors.NotFound(request.PublisherId));

			if (request.ImageIds?.Count > 0)
			{
				List<Result<ImageSource<PublisherImageType>>> result = [];

				request.ImageIds.ForEach(i =>
				{
					var imageToDelete = publisher.Images.FirstOrDefault(o => o.Id == i);
					if (imageToDelete is not null)
						result.Add(Result.Success(imageToDelete));
					else
						result.Add(Result.Failure<ImageSource<PublisherImageType>>(
													PublisherErrors.PublisherImageNotFound(publisher.Id, i)));
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

		private async Task RemoveFromDb(IEnumerable<ImageSource<PublisherImageType>> images, CancellationToken cancellationToken)
			=> await images.Tap(imgRepository.DeleteRange)
							.Tap(() => db.SaveChangesAsync(cancellationToken));

		private async Task RemoveFiles(string? source, CancellationToken cancellationToken = default)
		{
			if (source is not null)
				await fileManager.DeleteAsync(source, cancellationToken);
		}
	}
}
