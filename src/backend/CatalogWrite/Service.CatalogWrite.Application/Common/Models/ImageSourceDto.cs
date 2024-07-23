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
using Service.CatalogWrite.Domain.Books;
using Service.CatalogWrite.Domain.Categories;
using Service.CatalogWrite.Domain.ImageSources;
using Service.CatalogWrite.Domain.Publishers;

namespace Service.CatalogWrite.Application.Common.Models
{
    /// <summary>
    /// Represents dto for <see cref="ImageSource{TEnum}"/>.
    /// </summary>
    public sealed class ImageSourceDto
        : IMapWith<ImageSource<PublisherImageType>>,
        IMapWith<ImageSource<AuthorImageType>>,
        IMapWith<ImageSource<BookImageType>>,
        IMapWith<ImageSource<CategoryImageType>>
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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ImageSource<PublisherImageType>, ImageSourceDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(i => i.Id.Value))
                .ForMember(dto => dto.Source, opt => opt.MapFrom(i => i.Source))
                .ForMember(dto => dto.Type, opt => opt.MapFrom(i => i.Type.Name));

            profile.CreateMap<ImageSource<AuthorImageType>, ImageSourceDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(i => i.Id.Value))
                .ForMember(dto => dto.Source, opt => opt.MapFrom(i => i.Source))
                .ForMember(dto => dto.Type, opt => opt.MapFrom(i => i.Type.Name));

            profile.CreateMap<ImageSource<BookImageType>, ImageSourceDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(i => i.Id.Value))
                .ForMember(dto => dto.Source, opt => opt.MapFrom(i => i.Source))
                .ForMember(dto => dto.Type, opt => opt.MapFrom(i => i.Type.Name));

            profile.CreateMap<ImageSource<CategoryImageType>, ImageSourceDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(i => i.Id.Value))
                .ForMember(dto => dto.Source, opt => opt.MapFrom(i => i.Source))
                .ForMember(dto => dto.Type, opt => opt.MapFrom(i => i.Type.Name));
        }
    }
}
