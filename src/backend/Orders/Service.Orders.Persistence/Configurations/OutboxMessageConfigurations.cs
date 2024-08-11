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
using Persistence.Outbox;
using Service.Orders.Persistence.Contracts;

namespace Service.Orders.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="OutboxMessage"/> entity configuration.
	/// </summary>
	internal sealed class OutboxMessageConfigurations : IEntityTypeConfiguration<OutboxMessage>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<OutboxMessage> builder) => ConfigureDataStructure(builder);

		private static void ConfigureDataStructure(EntityTypeBuilder<OutboxMessage> builder)
		{
			builder.ToTable(TableNames.OutboxMessages);

			builder.HasKey(outboxMessage => outboxMessage.Id);

			builder.Property(outboxMessage => outboxMessage.Id).ValueGeneratedNever();

			builder.Property(outboxMessage => outboxMessage.OccurredOnUtc).IsRequired();

			builder.Property(outboxMessage => outboxMessage.Type).IsRequired();

			// TODO __##__ use ObjectJsonConverter if db provider does not support json column type
			builder.Property(outboxMessage => outboxMessage.Content)
				.IsRequired();
			// .HasColumnType("json");

			builder.Property(outboxMessage => outboxMessage.ProcessedOnUtc).IsRequired(false);

			builder.Property(outboxMessage => outboxMessage.Error).IsRequired(false);
		}
	}
}
