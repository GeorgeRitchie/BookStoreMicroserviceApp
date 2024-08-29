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

using Service.Carts.Domain.CartItems;
using Service.Carts.Domain.Carts;

namespace Service.Carts.Application.Carts.GetOrCreateCart
{
	/// <summary>
	/// Represents the dto for <see cref="Cart"/> entity.
	/// </summary>
	public sealed class CartDto : IMapWith<Cart>
	{
		/// <summary>
		/// Cart items.
		/// </summary>
		public List<CartItemDto> Items { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Cart, CartDto>();
	}

	/// <summary>
	/// Represents the dto for <see cref="CartItem"/> entity.
	/// </summary>
	public sealed class CartItemDto : IMapWith<CartItem>
	{
		/// <summary>
		/// Book identifier.
		/// </summary>
		public Guid BookId { get; set; }

		/// <summary>
		/// Book source identifier.
		/// </summary>
		public Guid BookSourceId { get; set; }

		/// <summary>
		/// Book format (e. g. paper, pdf, txt).
		/// </summary>
		public string FormatName { get; set; }

		/// <summary>
		/// Book price.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// Book quantity customer is purchasing.
		/// </summary>
		public uint Quantity { get; set; }

		/// <summary>
		/// Book title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Book description.
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Book ISBN if available.
		/// </summary>
		public string? ISBN { get; set; }

		/// <summary>
		/// Book language in ISO 639-1 format (e. g. ru, en, fr).
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		/// Book cover image if available.
		/// </summary>
		public string? Cover { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<CartItem, CartItemDto>()
				.ForMember(dto => dto.BookId, opt => opt.MapFrom(ci => ci.BookSource.Book.Id.Value))
				.ForMember(dto => dto.BookSourceId, opt => opt.MapFrom(ci => ci.BookSource.Id.Value))
				.ForMember(dto => dto.FormatName, opt => opt.MapFrom(ci => ci.BookSource.Format.Name))
				.ForMember(dto => dto.Price, opt => opt.MapFrom(ci => ci.BookSource.Price))
				.ForMember(dto => dto.Quantity, opt => opt.MapFrom(ci => ci.Quantity))
				.ForMember(dto => dto.Title, opt => opt.MapFrom(ci => ci.BookSource.Book.Title))
				.ForMember(dto => dto.Description, opt => opt.MapFrom(ci => ci.BookSource.Book.Description))
				.ForMember(dto => dto.ISBN, opt => opt.MapFrom(ci => ci.BookSource.Book.ISBN))
				.ForMember(dto => dto.Language, opt => opt.MapFrom(ci => ci.BookSource.Book.Language))
				.ForMember(dto => dto.Cover, opt => opt.MapFrom(ci => ci.BookSource.Book.Cover));
	}
}
