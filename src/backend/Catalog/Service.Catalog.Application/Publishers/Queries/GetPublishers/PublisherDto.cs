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

using Service.Catalog.Application.Common.Models;
using Service.Catalog.Domain.Publishers;

namespace Service.Catalog.Application.Publishers.Queries.GetPublishers
{
	/// <summary>
	/// Represents the dto for <see cref="Publisher"/> entity.
	/// </summary>
	public sealed class PublisherDto : IMapWith<Publisher>
	{
		/// <summary>
		/// Publisher identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Publisher name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Publisher location address.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Publisher location city.
		/// </summary>
		public string City { get; set; }

		/// <summary>
		/// Publisher location country.
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// Publisher phone number.
		/// </summary>
		public string? PhoneNumber { get; set; }

		/// <summary>
		/// Publisher email address.
		/// </summary>
		public string? Email { get; set; }

		/// <summary>
		/// Publisher website.
		/// </summary>
		public string? Website { get; set; }

		/// <summary>
		/// Publisher photos.
		/// </summary>
		public List<ImageSourceDto> Images { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Publisher, PublisherDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value))
				.ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(c => c.PhoneNumber == null ? null : c.PhoneNumber.Number))
				.ForMember(dto => dto.Email, opt => opt.MapFrom(c => c.Email == null ? null : c.Email.EmailAddress))
				.ForMember(dto => dto.Website, opt => opt.MapFrom(c => c.Website == null ? null : c.Website.Url));
	}
}
