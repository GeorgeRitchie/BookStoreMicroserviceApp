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
using Service.Analytics.Domain.Orders;

namespace Service.Analytics.WebApi.GraphQL.Vms
{
	/// <summary>
	/// Represents the <see cref="Order"/> vm class.
	/// </summary>
	public class OrderVm : IMapWith<Order>
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
		/// Ordered date and time.
		/// </summary>
		public DateTime OrderedDateTimeUtc { get; set; }

		/// <summary>
		/// Shipment address if available.
		/// </summary>
		public Address? Address { get; set; }

		/// <summary>
		/// Ordered items collection.
		/// </summary>
		public List<OrderItemVm> Items { get; set; }

		/// <inheritdoc/>
		public void Mapping(Profile profile)
			=> profile.CreateMap<Order, OrderVm>()
				.ForMember(vm => vm.Id, opt => opt.MapFrom(o => o.Id.Value))
				.ForMember(vm => vm.CustomerId, opt => opt.MapFrom(o => o.CustomerId.Value));
	}
}
