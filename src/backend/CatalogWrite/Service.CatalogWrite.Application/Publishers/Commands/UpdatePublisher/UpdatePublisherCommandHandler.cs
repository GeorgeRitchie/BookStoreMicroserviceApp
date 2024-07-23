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
using Service.CatalogWrite.Domain.Publishers;
using Service.CatalogWrite.Domain.ValueObjects;

namespace Service.CatalogWrite.Application.Publishers.Commands.UpdatePublisher
{
	/// <summary>
	/// Represents the <see cref="UpdatePublisherCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="UpdatePublisherCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The publisher repository.</param>
	internal sealed class UpdatePublisherCommandHandler(
		ICatalogDb db,
		IRepository<Publisher, PublisherId> repository)
		: ICommandHandler<UpdatePublisherCommand>
	{
		/// <inheritdoc/>
		public async Task<Result> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
		{
			var publisher = await repository.GetAll()
											.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

			if (publisher == null)
				return Result.Failure(PublisherErrors.NotFound(request.Id));

			var name = request.Name ?? publisher.Name;
			var address = request.Address ?? publisher.Address;
			var city = request.City ?? publisher.City;
			var country = request.Country ?? publisher.Country;

			var emailCreateResult = string.IsNullOrEmpty(request.Email) ? null : Email.Create(request.Email);
			var websiteCreateResult = string.IsNullOrEmpty(request.Website) ? null : Website.Create(request.Website);
			var phoneCreateResult = string.IsNullOrEmpty(request.PhoneNumber) ? null : PhoneNumber.Create(request.PhoneNumber);

			var result = await Result.Combine(
					emailCreateResult ?? Result.Success(),
					phoneCreateResult ?? Result.Success(),
					websiteCreateResult ?? Result.Success())
				.Bind(() => publisher.Change(name,
										address,
										city,
										country,
										request.PhoneNumber is null ? publisher.PhoneNumber : phoneCreateResult?.Value,
										request.Email is null ? publisher.Email : emailCreateResult?.Value,
										request.Website is null ? publisher.Website : websiteCreateResult?.Value))
				.Tap<Publisher>(repository.Update)
				.Tap(() => db.SaveChangesAsync(cancellationToken));

			return result;
		}
	}
}
