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

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Carts.Domain.CartItems;
using Service.Carts.Persistence.Contracts;
using Shared.Extensions;

namespace Service.Carts.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="CartItem"/> entity configuration.
	/// </summary>
	internal sealed class CartItemConfigurations : IEntityTypeConfiguration<CartItem>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<CartItem> builder) =>
			 builder
				.Tap(ConfigureDataStructure)
				.Tap(ConfigureRelationships);

		private static void ConfigureDataStructure(EntityTypeBuilder<CartItem> builder)
		{
			builder.ToTable(TableNames.CartItems);

			builder.HasKey(cartItem => cartItem.Id);

			builder.Property(cartItem => cartItem.Id).ValueGeneratedNever()
							.HasConversion(cartItemId => cartItemId.Value, value => new CartItemId(value));

			builder.Property(cartItem => cartItem.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.Property(cartItem => cartItem.Quantity).IsRequired();

			builder.Property(cartItem => cartItem.CreatedOnUtc).IsRequired();

			builder.Property(cartItem => cartItem.ModifiedOnUtc).IsRequired(false);
		}

		private static void ConfigureRelationships(EntityTypeBuilder<CartItem> builder)
		{
			builder.HasOne(cartItem => cartItem.Cart)
				.WithMany(cart => cart.Items)
				.HasForeignKey(cartItem => cartItem.CartId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			builder.HasOne(cartItem => cartItem.BookSource)
				.WithMany()
				.HasForeignKey(cartItem => cartItem.BookSourceId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}
