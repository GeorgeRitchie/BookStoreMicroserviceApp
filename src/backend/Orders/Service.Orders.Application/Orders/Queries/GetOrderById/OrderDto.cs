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

using Service.Orders.Domain.OrderItems;
using Service.Orders.Domain.Orders;
using Service.Orders.Domain.Payments;
using Service.Orders.Domain.Shipments;

namespace Service.Orders.Application.Orders.Queries.GetOrderById
{
	/// <summary>
	/// Represents the dto for <see cref="Order"/> entity.
	/// </summary>
	public sealed class OrderDto : IMapWith<Order>
	{
		/// <summary>
		/// Order identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Customer identifier.
		/// </summary>
		public Guid CustomerId { get; set; }

		/// <summary>
		/// Order status.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Ordered date and time.
		/// </summary>
		public DateTime OrderedDateTimeUtc { get; set; }

		/// <summary>
		/// Order payment information if available.
		/// </summary>
		public PaymentDto? Payment { get; set; }

		/// <summary>
		/// Order shipment information if available.
		/// </summary>
		public ShipmentDto? Shipment { get; set; }

		/// <summary>
		/// Ordered items collection.
		/// </summary>
		public List<OrderItemDto> Items { get; set; }

		/// <summary>
		/// Total price of the order.
		/// </summary>
		public decimal TotalPrice { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Order, OrderDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value))
				.ForMember(dto => dto.CustomerId, opt => opt.MapFrom(c => c.CustomerId.Value))
				.ForMember(dto => dto.Status, opt => opt.MapFrom(c => c.Status.Name));
	}

	/// <summary>
	/// Represents the dto for <see cref="OrderItem"/> entity.
	/// </summary>
	public class OrderItemDto : IMapWith<OrderItem>
	{
		/// <summary>
		/// Book identifier.
		/// </summary>
		public Guid BookId { get; set; }

		/// <summary>
		/// Book title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Book ISBN if available.
		/// </summary>
		public string? ISBN { get; set; }

		/// <summary>
		/// Book cover image if available.
		/// </summary>
		public string? Cover { get; set; }

		/// <summary>
		/// Book language.
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		/// Book source identifier.
		/// </summary>
		public Guid SourceId { get; set; }

		/// <summary>
		/// Book source format.
		/// </summary>
		public string Format { get; set; }

		/// <summary>
		/// One unit price.
		/// </summary>
		public decimal UnitPrice { get; set; }

		/// <summary>
		/// Ordering quantity.
		/// </summary>
		public uint Quantity { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<OrderItem, OrderItemDto>()
				.ForMember(dto => dto.BookId, opt => opt.MapFrom(c => c.BookId.Value))
				.ForMember(dto => dto.SourceId, opt => opt.MapFrom(c => c.SourceId.Value))
				.ForMember(dto => dto.Format, opt => opt.MapFrom(c => c.Format.Name));
	}

	/// <summary>
	/// Represents the dto for <see cref="Shipment"/> entity.
	/// </summary>
	public class ShipmentDto : IMapWith<Shipment>
	{
		/// <summary>
		/// Shipment status.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Shipment address if available.
		/// </summary>
		public Address? Address { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Shipment, ShipmentDto>()
				.ForMember(dto => dto.Status, opt => opt.MapFrom(c => c.Status.Name));
	}

	/// <summary>
	/// Represents the dto for <see cref="Payment"/> entity.
	/// </summary>
	public class PaymentDto : IMapWith<Payment>
	{
		/// <summary>
		/// Payment status.
		/// </summary>
		public string Status { get; set; }

		/// <summary>
		/// Failure payment error.
		/// </summary>
		public Error? Error { get; set; }

		/// <summary>
		/// Url for payment required user interaction.
		/// </summary>
		public Uri? UserInteractionUrl { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Payment, PaymentDto>()
				.ForMember(dto => dto.Status, opt => opt.MapFrom(c => c.Status.Name));
	}
}
