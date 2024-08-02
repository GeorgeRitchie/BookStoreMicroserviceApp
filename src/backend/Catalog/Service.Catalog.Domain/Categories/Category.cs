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

using Service.Catalog.Domain.Books;
using Service.Catalog.Domain.Categories.Events;
using Service.Catalog.Domain.ImageSources;

namespace Service.Catalog.Domain.Categories
{
	/// <summary>
	/// Represents the Category entity.
	/// </summary>
	public sealed class Category : Entity<CategoryId>, IAuditable
	{
		private List<Book> books = [];

		/// <inheritdoc/>
		public DateTime CreatedOnUtc { get; private set; }

		/// <inheritdoc/>
		public DateTime? ModifiedOnUtc { get; private set; }

		/// <summary>
		/// Gets the category title.
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// Gets the category description.
		/// </summary>
		public string Description { get; private set; } = string.Empty;

		/// <summary>
		/// Gets the category image.
		/// </summary>
		public ImageSource<CategoryImageType> Icon { get; private set; }

		/// <summary>
		/// Gets books within the category.
		/// </summary>
		public IReadOnlyList<Book> Books => books;

		/// <summary>
		/// Initializes a new instance of the <see cref="Category"/> class.
		/// </summary>
		/// <param name="id">The category identifier.</param>
		/// <param name="isDeleted">The category deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		private Category(CategoryId id, bool isDeleted = false) : base(id, isDeleted)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Category"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private Category()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Creates a new <see cref="Category"/> instance based on the specified parameters by applying validations.
		/// </summary>
		/// <param name="title">Category title.</param>
		/// <param name="icon">Category icon.</param>
		/// <param name="description">Category description.</param>
		/// <returns>The new <see cref="Category"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<Category> Create(string title, ImageSource<CategoryImageType> icon, string? description = null)
			=> Result.Success(
				new Category(new CategoryId(Guid.NewGuid()), false)
				{
					Title = title,
					Description = description ?? string.Empty,
					Icon = icon,
				})
				.Ensure(c => string.IsNullOrWhiteSpace(c.Title) == false, CategoryErrors.TitleIsRequired)
				.Ensure(c => c.Icon is not null, CategoryErrors.IconIsRequired)
				.Tap(c => c.RaiseDomainEvent(
					new CategoryCreatedDomainEvent(
						Guid.NewGuid(),
						DateTime.UtcNow,
						c.Id,
						c.Title,
						c.Description,
						KeyValuePair.Create(c.Icon.Type.Name, c.Icon.Source))));

		/// <summary>
		/// Changes category information.
		/// </summary>
		/// <param name="title">Category title.</param>
		/// <param name="icon">Category icon.</param>
		/// <param name="description">Category description.</param>
		/// <returns>The updated category.</returns>
		public Result<Category> Change(string title, ImageSource<CategoryImageType> icon, string? description = null)
			=> Result.Success(this)
				.Ensure(c => string.IsNullOrWhiteSpace(title) == false, CategoryErrors.TitleIsRequired)
				.Ensure(c => icon is not null, CategoryErrors.IconIsRequired)
				.Tap(c =>
				{
					string _description = description ?? string.Empty;

					bool categoryInfoChanged = Title != title
											|| Icon != icon
											|| Description != _description;

					if (categoryInfoChanged)
					{
						c.Title = title;
						c.Icon = icon;
						c.Description = _description;

						c.RaiseDomainEvent(new CategoryUpdatedDomainEvent(
							Guid.NewGuid(),
							DateTime.UtcNow,
							c.Id,
							c.Title,
							c.Description,
							KeyValuePair.Create(c.Icon.Type.Name, c.Icon.Source)));
					}
				});
	}
}
