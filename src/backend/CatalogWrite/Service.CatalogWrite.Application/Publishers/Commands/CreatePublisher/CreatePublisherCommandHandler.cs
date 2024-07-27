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

namespace Service.CatalogWrite.Application.Publishers.Commands.CreatePublisher
{
	/// <summary>
	/// Represents the <see cref="CreatePublisherCommand"/> handler.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="CreatePublisherCommandHandler"/> class.
	/// </remarks>
	/// <param name="db">The database.</param>
	/// <param name="repository">The publisher repository.</param>
	internal sealed class CreatePublisherCommandHandler(
		ICatalogDb db,
		IRepository<Publisher, PublisherId> repository)
		: ICommandHandler<CreatePublisherCommand, Guid>
	{
		/// <inheritdoc/>
		public async Task<Result<Guid>> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
		{
			var email = request.Email is null ? null : Email.Create(request.Email);
			var website = request.Website is null ? null : Website.Create(request.Website);
			var phoneNumber = request.PhoneNumber is null ? null : PhoneNumber.Create(request.PhoneNumber);

			return await Result.Combine(
							phoneNumber ?? Result.Success(),
							email ?? Result.Success(),
							website ?? Result.Success())
						.Bind(() => Publisher.Create(request.Name,
												request.Address,
												request.City,
												request.Country,
												phoneNumber?.Value,
												email?.Value,
												website?.Value))
						.Tap<Publisher>(publisher => repository.Create(publisher))
						.Tap(() => db.SaveChangesAsync(cancellationToken))
						.Map(publisher => publisher.Id.Value);
		}
	}
}
