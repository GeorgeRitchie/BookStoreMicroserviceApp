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

namespace Service.Orders.Application.Orders.Commands.CreateOrder
{
	/// <summary>
	/// Represents the command to create a new order.
	/// </summary>
	public sealed class CreateOrderCommand : ICommand<Guid>
	{
		/// <summary>
		/// Customer identifier.
		/// </summary>
		public Guid CustomerId { get; set; }

		/// <summary>
		/// Ordering items.
		/// </summary>
		public IEnumerable<OrderItemDto> Items { get; set; }
	}

	/// <summary>
	/// Represents ordering item dto.
	/// </summary>
	public sealed class OrderItemDto
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
	}
}
