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

using Service.Shipments.Domain.ShipmentItems;
using Service.Shipments.Domain.Shipments;

namespace Service.Shipments.Application.Shipments.GetShipments
{
	/// <summary>
	/// Represents the dto for <see cref="Shipment"/> entity.
	/// </summary>
	public sealed class ShipmentDto : IMapWith<Shipment>
	{
		/// <summary>
		/// The shipment identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// The customer identifier.
		/// </summary>
		public Guid CustomerId { get; set; }

		/// <summary>
		/// The order identifier.
		/// </summary>
		public Guid OrderId { get; set; }

		/// <summary>
		/// The ordered date time.
		/// </summary>
		public DateTime OrderedDateTimeUtc { get; set; }

		/// <summary>
		/// The shipment address if available.
		/// </summary>
		public Address? Address { get; set; }

		/// <summary>
		/// The shipment status.
		/// </summary>
		public string StatusName { get; set; }

		/// <summary>
		/// The shipment items.
		/// </summary>
		public List<ShipmentItemDto> Items { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Shipment, ShipmentDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value))
				.ForMember(dto => dto.CustomerId, opt => opt.MapFrom(c => c.CustomerId.Value))
				.ForMember(dto => dto.OrderId, opt => opt.MapFrom(c => c.OrderId.Value))
				.ForMember(dto => dto.StatusName, opt => opt.MapFrom(c => c.Status.Name));
	}

	/// <summary>
	/// Represents the dto for <see cref="ShipmentItem"/> entity.
	/// </summary>
	public sealed class ShipmentItemDto : IMapWith<ShipmentItem>
	{
		/// <summary>
		/// The shipment item identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// The shipment item quantity.
		/// </summary>
		public uint Quantity { get; set; }

		/// <summary>
		/// The book source identifier.
		/// </summary>
		public Guid BookSourceId { get; set; }

		/// <summary>
		/// The book format name.
		/// </summary>
		public string BookFormatName { get; set; }

		/// <summary>
		/// The book identifier.
		/// </summary>
		public Guid BookId { get; set; }

		/// <summary>
		/// The book title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The book description.
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// The book ISBN.
		/// </summary>
		public string? ISBN { get; set; }

		/// <summary>
		/// The book language.
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		/// The book age rating.
		/// </summary>
		public uint AgeRating { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<ShipmentItem, ShipmentItemDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value))
				.ForMember(dto => dto.Quantity, opt => opt.MapFrom(c => c.Quantity))
				.ForMember(dto => dto.BookSourceId, opt => opt.MapFrom(c => c.BookSource.Id.Value))
				.ForMember(dto => dto.BookFormatName, opt => opt.MapFrom(c => c.BookSource.Format.Name))
				.ForMember(dto => dto.BookId, opt => opt.MapFrom(c => c.BookSource.Book.Id.Value))
				.ForMember(dto => dto.Title, opt => opt.MapFrom(c => c.BookSource.Book.Title))
				.ForMember(dto => dto.Description, opt => opt.MapFrom(c => c.BookSource.Book.Description))
				.ForMember(dto => dto.ISBN, opt => opt.MapFrom(c => c.BookSource.Book.ISBN))
				.ForMember(dto => dto.Language, opt => opt.MapFrom(c => c.BookSource.Book.Language))
				.ForMember(dto => dto.AgeRating, opt => opt.MapFrom(c => c.BookSource.Book.AgeRating));
	}
}
