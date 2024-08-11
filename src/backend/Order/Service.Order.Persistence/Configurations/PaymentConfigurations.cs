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
using Persistence.Converters;
using Service.Orders.Domain.Payments;
using Service.Orders.Persistence.Contracts;
using Service.Payments.IntegrationEvents;
using Shared.Extensions;

namespace Service.Orders.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="Payment"/> entity configuration.
	/// </summary>
	internal sealed class PaymentConfigurations : IEntityTypeConfiguration<Payment>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Payment> builder) =>
			 builder
				.Tap(ConfigureDataStructure);

		private static void ConfigureDataStructure(EntityTypeBuilder<Payment> builder)
		{
			builder.ToTable(TableNames.Payments);

			builder.HasKey(payment => payment.Id);

			builder.Property(payment => payment.Id).ValueGeneratedNever()
							.HasConversion(paymentId => paymentId.Value, value => new PaymentId(value));

			builder.Property(payment => payment.IsDeleted).IsRequired().HasDefaultValue(false);

			builder.HasQueryFilter(payment => payment.IsDeleted == false);

			builder.OwnsOne(payment => payment.Error, error =>
			{
				error.Property(e => e.Code).IsRequired(false);
				error.Property(e => e.Message).IsRequired(false);
				error.Property(e => e.Description).IsRequired(false);
				error.Property(e => e.Source).IsRequired(false);
				error.Ignore(e => e.InnerError);
			});

			builder.Property(payment => payment.UserInteractionUrl).IsRequired(false);

			builder.Property(bs => bs.Status)
					.HasConversion<EnumerationConverter<PaymentStatus, int>>()
					.IsRequired();

			builder.Property(payment => payment.CreatedOnUtc).IsRequired();

			builder.Property(payment => payment.ModifiedOnUtc).IsRequired(false);
		}
	}
}
