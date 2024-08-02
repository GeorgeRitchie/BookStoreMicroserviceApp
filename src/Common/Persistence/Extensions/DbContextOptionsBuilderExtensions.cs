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

using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence.Constants;

namespace Persistence.Extensions
{
	/// <summary>
	/// Contains extension method for the any classes of any EF Core DB provider that implements <see cref="RelationalDbContextOptionsBuilder{TBuilder, TExtension}"/> class.
	/// </summary>
	public static class DbContextOptionsBuilderExtensions
	{
		/// <summary>
		/// Configures the migration history table to live in the specified schema.
		/// </summary>
		/// <param name="dbContextOptionsBuilder">The database context options builder.</param>
		/// <param name="schema">The schema.</param>
		/// <returns>The same database context options builder.</returns>
		public static RelationalDbContextOptionsBuilder<TBuilder, TExtension> WithMigrationHistoryTableInSchema<TBuilder, TExtension>(
			this RelationalDbContextOptionsBuilder<TBuilder, TExtension> dbContextOptionsBuilder, string schema)
				where TBuilder : RelationalDbContextOptionsBuilder<TBuilder, TExtension>
				where TExtension : RelationalOptionsExtension, new()
			=> dbContextOptionsBuilder.MigrationsHistoryTable(TableNames.MigrationHistory, schema);
	}
}
