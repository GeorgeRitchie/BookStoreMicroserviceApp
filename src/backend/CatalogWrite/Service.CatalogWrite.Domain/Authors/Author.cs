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

using Service.CatalogWrite.Domain.Authors.Events;
using Service.CatalogWrite.Domain.ImageSources;
using Service.CatalogWrite.Domain.ValueObjects;

namespace Service.CatalogWrite.Domain.Authors
{
	/// <summary>
	/// Represents the Author entity.
	/// </summary>
	public sealed class Author : Entity<AuthorId>, IAuditable
	{
		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the author first name.
		/// </summary>
		public string FirstName { get; private set; }

		/// <summary>
		/// Gets the author last name.
		/// </summary>
		public string LastName { get; private set; }

		/// <summary>
		/// Gets the author brief information.
		/// </summary>
		public string Description { get; private set; } = string.Empty;

		/// <summary>
		/// Gets the author email address if available.
		/// </summary>
		public Email? Email { get; private set; }

		/// <summary>
		/// Gets the author website if available.
		/// </summary>
		public Website? Website { get; private set; }

		/// <summary>
		/// Gets the author images.
		/// </summary>
		public IReadOnlyCollection<ImageSource<AuthorImageType>> Images { get; private set; } = [];

		/// <summary>
		/// Initializes a new instance of the <see cref="Author"/> class.
		/// </summary>
		/// <param name="id">The author identifier.</param>
		/// <param name="isDeleted">The author deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private Author(AuthorId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Author"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Author()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="Author"/> instance based on the specified parameters by applying validations.
		/// </summary>
		/// <param name="firstName">Author's first name.</param>
		/// <param name="lastName">Author's last name.</param>
		/// <param name="description">Author's description.</param>
		/// <param name="email">Author's email address.</param>
		/// <param name="website">Author's website.</param>
		/// <param name="images">Author's images.</param>
		/// <returns>The new <see cref="Author"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Author> Create(
			string firstName,
			string lastName,
			string? description = null,
			Email? email = null,
			Website? website = null,
			IEnumerable<ImageSource<AuthorImageType>>? images = null)
			=> Result.Success(
				new Author(new AuthorId(Guid.NewGuid()), false)
				{
					FirstName = firstName,
					LastName = lastName,
					Description = description ?? string.Empty,
					Email = email,
					Website = website,
					Images = images?.ToList() ?? [],
				})
				.Ensure(a => string.IsNullOrWhiteSpace(firstName) == false, AuthorErrors.FirstNameIsRequired)
				.Ensure(a => string.IsNullOrWhiteSpace(lastName) == false, AuthorErrors.LastNameIsRequired)
				.Tap(a => a.RaiseDomainEvent(
					new AuthorCreatedDomainEvent(
					Guid.NewGuid(),
					DateTime.UtcNow,
					a.Id,
					a.FirstName,
					a.LastName,
					a.Description,
					a.Email,
					a.Website)));

		/// <summary>
		/// Changes author information.
		/// </summary>
		/// <param name="firstName">Author's first name.</param>
		/// <param name="lastName">Author's last name.</param>
		/// <param name="description">Author's description.</param>
		/// <param name="email">Author's email address.</param>
		/// <param name="website">Author's website.</param>
		/// <returns>The updated author.</returns>
		public Result<Author> Change(
			string firstName,
			string lastName,
			string? description = null,
			Email? email = null,
			Website? website = null)
			=> Result.Success(this)
				.Ensure(a => string.IsNullOrWhiteSpace(firstName) == false, AuthorErrors.FirstNameIsRequired)
				.Ensure(a => string.IsNullOrWhiteSpace(lastName) == false, AuthorErrors.LastNameIsRequired)
				.Tap(a =>
				{
					string _description = description ?? string.Empty;

					bool authorInfoChanged = FirstName != firstName
											|| LastName != lastName
											|| Description != _description
											|| Email != email
											|| Website != website;

					if (authorInfoChanged)
					{
						a.FirstName = firstName;
						a.LastName = lastName;
						a.Description = _description;
						a.Email = email;
						a.Website = website;

						a.RaiseDomainEvent(new AuthorUpdatedDomainEvent(
							Guid.NewGuid(),
							DateTime.UtcNow,
							a.Id,
							a.FirstName,
							a.LastName,
							a.Description,
							a.Email,
							a.Website));
					}
				});
	}
}
