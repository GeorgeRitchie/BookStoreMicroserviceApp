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

namespace Service.Analytics.Domain.OrderItems
{
	/// <summary>
	/// Contains the Order Item errors.
	/// </summary>
	public static class OrderItemErrors
	{
		/// <summary>
		/// Gets invalid ISBN error.
		/// </summary>
		/// <param name="isbn">The invalid isbn.</param>
		/// <returns>The error.</returns>
		public static Error InvalidISBN(string? isbn)
			=> new("OrderItem.InvalidISBN", $"The passed ISBN '{isbn}' is not valid.");

		/// <summary>
		/// Gets invalid format error.
		/// </summary>
		/// <param name="isbn">The invalid format.</param>
		/// <returns>The error.</returns>
		public static Error InvalidFormat(string? format)
			=> new("OrderItem.InvalidBookFormat", $"The passed books source format '{format}' is not valid.");

		/// <summary>
		/// Gets empty title error.
		/// </summary>
		/// <returns>The error.</returns>
		public static Error EmptyTitle()
			=> new("OrderItem.EmptyTitle", "Book's title cannot be null, empty or white-space string.");

		/// <summary>
		/// Gets invalid language code error.
		/// </summary>
		/// <param name="lang">The invalid language code.</param>
		/// <returns>The error.</returns>
		public static Error InvalidLanguageCode(string? lang)
			=> new("OrderItem.InvalidLanguageCode",
					$"The provided language code is not valid ISO 639-1 formatted language code: {lang}.");

		/// <summary>
		/// Gets invalid quantity error.
		/// </summary>
		/// <param name="quantity">The invalid quantity.</param>
		/// <returns>The error.</returns>
		public static Error InvalidQuantity(uint quantity)
			=> new("OrderItem.InvalidQuantity", $"You cannot order {quantity} items.");
	}
}
