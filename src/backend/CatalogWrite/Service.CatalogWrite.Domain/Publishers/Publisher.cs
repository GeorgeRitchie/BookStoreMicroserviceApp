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

using Service.CatalogWrite.Domain.ImageSources;
using Service.CatalogWrite.Domain.Publishers.Events;
using Service.CatalogWrite.Domain.ValueObjects;

namespace Service.CatalogWrite.Domain.Publishers
{
	/// <summary>
	/// Represents the Publisher entity.
	/// </summary>
	public sealed class Publisher : Entity<PublisherId>, IAuditable
	{
		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the publisher name.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the publisher location address.
		/// </summary>
		public string Address { get; private set; }

		/// <summary>
		/// Gets the publisher location city.
		/// </summary>
		public string City { get; private set; }

		/// <summary>
		/// Gets the publisher location country.
		/// </summary>
		public string Country { get; private set; }

		/// <summary>
		/// Gets the publisher phone number.
		/// </summary>
		public PhoneNumber? PhoneNumber { get; private set; }

		/// <summary>
		/// Gets the publisher email address.
		/// </summary>
		public Email? Email { get; private set; }

		/// <summary>
		/// Gets the publisher website.
		/// </summary>
		public Website? Website { get; private set; }

		/// <summary>
		/// Gets the publisher photos.
		/// </summary>
		public IReadOnlyCollection<ImageSource<PublisherImageType>> Images { get; private set; } = [];

		/// <summary>
		/// Initializes a new instance of the <see cref="Publisher"/> class.
		/// </summary>
		/// <param name="id">The publisher identifier.</param>
		/// <param name="isDeleted">The publisher deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private Publisher(PublisherId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Publisher"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Publisher()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="Publisher"/> instance based on the specified parameters by applying validations.
		/// </summary>
		/// <param name="name">Publisher's name.</param>
		/// <param name="address">Publisher's address.</param>
		/// <param name="city">Publisher's city.</param>
		/// <param name="country">Publisher's country.</param>
		/// <param name="phoneNumber">Publisher's phone number.</param>
		/// <param name="email">Publisher's email address.</param>
		/// <param name="website">Publisher's website.</param>
		/// <param name="images">Publisher's images.</param>
		/// <returns>The new <see cref="Publisher"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Publisher> Create(
			string name,
			string address,
			string city,
			string country,
			PhoneNumber? phoneNumber = null,
			Email? email = null,
			Website? website = null,
			IEnumerable<ImageSource<PublisherImageType>>? images = null)
			=> Result.Success(
					new Publisher(new PublisherId(Guid.NewGuid()), false)
					{
						Name = name,
						Address = address,
						City = city,
						Country = country,
						PhoneNumber = phoneNumber,
						Email = email,
						Website = website,
						Images = images?.ToList() ?? [],
					})
				.Ensure(p => string.IsNullOrWhiteSpace(p.Name) == false, PublisherErrors.EmptyName)
				.Ensure(p => string.IsNullOrWhiteSpace(p.Address) == false, PublisherErrors.EmptyAddress)
				.Ensure(p => string.IsNullOrWhiteSpace(p.City) == false, PublisherErrors.EmptyCity)
				.Ensure(p => string.IsNullOrWhiteSpace(p.Country) == false, PublisherErrors.EmptyCountry)
				.Tap(p => p.RaiseDomainEvent(
					new PublisherCreatedDomainEvent(
						Guid.NewGuid(),
						DateTime.UtcNow,
						p.Id,
						p.Name,
						p.Address,
						p.City,
						p.Country,
						p.PhoneNumber,
						p.Email,
						p.Website)));

		/// <summary>
		/// Changes publisher information.
		/// </summary>
		/// <param name="name">Publisher's name.</param>
		/// <param name="address">Publisher's address.</param>
		/// <param name="city">Publisher's city.</param>
		/// <param name="country">Publisher's country.</param>
		/// <param name="phoneNumber">Publisher's phone number.</param>
		/// <param name="email">Publisher's email address.</param>
		/// <param name="website">Publisher's website.</param>
		/// <returns>The updated publisher.</returns>
		public Result<Publisher> Change(
			string name,
			string address,
			string city,
			string country,
			PhoneNumber? phoneNumber = null,
			Email? email = null,
			Website? website = null)
			=> Result.Success(this)
				.Ensure(p => string.IsNullOrWhiteSpace(p.Name) == false, PublisherErrors.EmptyName)
				.Ensure(p => string.IsNullOrWhiteSpace(p.Address) == false, PublisherErrors.EmptyAddress)
				.Ensure(p => string.IsNullOrWhiteSpace(p.City) == false, PublisherErrors.EmptyCity)
				.Ensure(p => string.IsNullOrWhiteSpace(p.Country) == false, PublisherErrors.EmptyCountry)
				.Tap(p =>
				{
					bool publisherInfoChanged = Name != name
											|| Address != address
											|| City != city
											|| Country != country
											|| PhoneNumber != PhoneNumber
											|| Email != email
											|| Website != website;

					if (publisherInfoChanged)
					{
						p.Name = name;
						p.Address = address;
						p.City = city;
						p.Country = country;
						p.PhoneNumber = phoneNumber;
						p.Email = email;
						p.Website = website;

						p.RaiseDomainEvent(new PublisherUpdatedDomainEvent(
							Guid.NewGuid(),
							DateTime.UtcNow,
							p.Id,
							p.Name,
							p.Address,
							p.City,
							p.Country,
							p.PhoneNumber,
							p.Email,
							p.Website));
					}
				});
	}
}
