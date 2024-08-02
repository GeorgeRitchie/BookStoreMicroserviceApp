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
using Persistence.Inbox;
using Service.Catalog.Persistence.Contracts;

namespace Service.Catalog.Persistence.Configurations
{
	/// <summary>
	/// Represents the <see cref="InboxMessageConsumer"/> entity configuration.
	/// </summary>
	internal sealed class InboxMessageConsumerConfigurations : IEntityTypeConfiguration<InboxMessageConsumer>
	{
		public void Configure(EntityTypeBuilder<InboxMessageConsumer> builder) => ConfigureDataStructure(builder);

		private static void ConfigureDataStructure(EntityTypeBuilder<InboxMessageConsumer> builder)
		{
			builder.ToTable(TableNames.InboxMessageConsumers);

			builder.HasKey(inboxMessageConsumer => new { inboxMessageConsumer.Id, inboxMessageConsumer.Name });
		}
	}
}
