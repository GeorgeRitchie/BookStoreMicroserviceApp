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

using Application.Mapper;
using AutoMapper;
using Service.Analytics.Domain.OrderItems;

namespace Service.Analytics.WebApi.GraphQL.Vms
{
	/// <summary>
	/// Represents the <see cref="OrderItem"/> vm class.
	/// </summary>
	public class OrderItemVm : IMapWith<OrderItem>
	{
		/// <summary>
		/// Order item identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Book identifier.
		/// </summary>
		public Guid BookId { get; set; }

		/// <summary>
		/// Book title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Book ISBN.
		/// </summary>
		public string? ISBN { get; set; }

		/// <summary>
		/// Book cover image.
		/// </summary>
		public string? Cover { get; set; }

		/// <summary>
		/// Book language.
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		///Book source identifier.
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
		public int Quantity { get; set; }

		/// <inheritdoc/>
		public void Mapping(Profile profile)
			=> profile.CreateMap<OrderItem, OrderItemVm>()
				.ForMember(vm => vm.Id, opt => opt.MapFrom(o => o.Id.Value))
				.ForMember(vm => vm.BookId, opt => opt.MapFrom(o => o.BookId.Value))
				.ForMember(vm => vm.SourceId, opt => opt.MapFrom(o => o.SourceId.Value))
				.ForMember(vm => vm.Format, opt => opt.MapFrom(o => o.Format.Name))
				.ForMember(vm => vm.Quantity, opt => opt.MapFrom(o => (int)o.Quantity));
	}
}
