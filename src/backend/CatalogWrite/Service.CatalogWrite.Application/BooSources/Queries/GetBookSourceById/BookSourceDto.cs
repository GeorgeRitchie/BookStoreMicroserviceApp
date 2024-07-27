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

using Service.CatalogWrite.Domain.BookSources;

namespace Service.CatalogWrite.Application.BooSources.Queries.GetBookSourceById
{
	/// <summary>
	/// Represents the dto for <see cref="BookSource"/> entity.
	/// </summary>
	public sealed class BookSourceDto : IMapWith<BookSource>
	{
		/// <summary>
		/// Book source identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Related book entity's identifier.
		/// </summary>
		public Guid BookId { get; set; }

		/// <summary>
		/// Book source format (e. g. paper, pdf, txt).
		/// </summary>
		public string Format { get; set; }

		/// <summary>
		/// Books quantity in storage. (Used only for paper format book sources).
		/// </summary>
		public uint? StockQuantity { get; set; }

		/// <summary>
		/// Book price.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// Book's preview source url of if available.
		/// </summary>
		public string? PreviewUrl { get; set; }

		/// <summary>
		/// E-Book's source url if available.
		/// </summary>
		public string? Url { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<BookSource, BookSourceDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value))
				.ForMember(dto => dto.BookId, opt => opt.MapFrom(c => c.BookId.Value))
				.ForMember(dto => dto.Format, opt => opt.MapFrom(c => c.Format.Name));
	}
}
