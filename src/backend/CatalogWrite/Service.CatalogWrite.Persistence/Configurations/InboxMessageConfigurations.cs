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

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Persistence.Inbox;
using Service.CatalogWrite.Persistence.Contracts;

namespace Service.CatalogWrite.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="InboxMessage"/> entity configuration.
	/// </summary>
	internal sealed class InboxMessageConfigurations : IEntityTypeConfiguration<InboxMessage>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<InboxMessage> builder) => ConfigureDataStructure(builder);

		private static void ConfigureDataStructure(EntityTypeBuilder<InboxMessage> builder)
		{
			builder.ToTable(TableNames.InboxMessages);

			builder.HasKey(inboxMessage => inboxMessage.Id);

			builder.Property(inboxMessage => inboxMessage.Id).ValueGeneratedNever();

			builder.Property(inboxMessage => inboxMessage.OccurredOnUtc).IsRequired();

			builder.Property(inboxMessage => inboxMessage.Type).IsRequired();

			// TODO use ObjectJsonConverter if db provider does not support json column type
			builder.Property(inboxMessage => inboxMessage.Content).HasColumnType("json").IsRequired();

			builder.Property(inboxMessage => inboxMessage.ProcessedOnUtc).IsRequired(false);

			builder.Property(inboxMessage => inboxMessage.Error).IsRequired(false);
		}
	}
}
