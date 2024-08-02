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

using Service.Catalog.Domain.Categories;

namespace Service.Catalog.Application.Categories.Queries.GetCategories
{
	/// <summary>
	/// Represents the dto for <see cref="Category"/> entity.
	/// </summary>
	public sealed class CategoryDto : IMapWith<Category>
	{
		/// <summary>
		/// Category identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Category title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Category description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Category icon.
		/// </summary>
		public string Icon { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Category, CategoryDto>()
						.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value))
						.ForMember(dto => dto.Icon, opt => opt.MapFrom(c => c.Icon.Source));
	}
}
