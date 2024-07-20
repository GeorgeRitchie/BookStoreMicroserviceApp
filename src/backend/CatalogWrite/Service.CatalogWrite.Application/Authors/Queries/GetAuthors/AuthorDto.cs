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

namespace Service.CatalogWrite.Application.Authors.Queries.GetAuthors
{
	/// <summary>
	/// Represents the dto for <see cref="Author"/> entity.
	/// </summary>
	public sealed class AuthorDto : IMapWith<Author>
	{
		/// <summary>
		/// Author identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Author first name.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Author last name.
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Author brief information.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Author email address.
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// Author website.
		/// </summary>
		public string? Website { get; set; }

		/// <summary>
		/// Author photo as icon.
		/// </summary>
		public string? Icon { get; set; }

		/// <summary>
		/// Author photo.
		/// </summary>
		public string? Photo { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Author, AuthorDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value))
				.ForMember(dto => dto.Email, opt => opt.MapFrom(c => c.Email == null ? null : c.Email.EmailAddress))
				.ForMember(dto => dto.Website, opt => opt.MapFrom(c => c.Website == null ? null : c.Website.Url))
				.ForMember(dto => dto.Icon, opt => opt.MapFrom(c =>
										c.Images.Any(i => i.Type == AuthorImageType.ProfileIcon)
										? c.Images.First(i => i.Type == AuthorImageType.ProfileIcon).Source
										: null))
				.ForMember(dto => dto.Photo, opt => opt.MapFrom(c =>
										c.Images.Any(i => i.Type == AuthorImageType.AuthorPhoto)
										? c.Images.First(i => i.Type == AuthorImageType.AuthorPhoto).Source
										: null));
	}
}
