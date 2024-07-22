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
using Service.CatalogWrite.Domain.ImageSources;

namespace Service.CatalogWrite.Application.Authors
{
	/// <summary>
	/// Represents dto for <see cref="ImageSource{TEnum}"/> of <see cref="Author"/> entity images.
	/// </summary>
	public sealed class AuthorImageSourceDto : IMapWith<ImageSource<AuthorImageType>>
	{
		/// <summary>
		/// The image source identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// The image file source.
		/// </summary>
		public string Source { get; set; }

		/// <summary>
		/// The image type name.
		/// </summary>
		public string Type { get; set; }

		public void Mapping(Profile profile) =>
			profile.CreateMap<ImageSource<AuthorImageType>, AuthorImageSourceDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(i => i.Id.Value))
				.ForMember(dto => dto.Source, opt => opt.MapFrom(i => i.Source))
				.ForMember(dto => dto.Type, opt => opt.MapFrom(i => i.Type.Name));
	}
}
