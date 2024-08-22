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
using Service.Payments.Persistence.Contracts;

namespace Service.Payments.Persistence
{
	// add-migration Init -context PaymentDbContext -o Migrations
	// update-database -context PaymentDbContext
	// migration -context PaymentDbContext
	// remove-migration -context PaymentDbContext
	// drop-database -context PaymentDbContext

	/// <summary>
	/// Represents the Payment module database context.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="PaymentDbContext"/> class.
	/// </remarks>
	/// <param name="options">The database context options.</param>
	public sealed class PaymentDbContext(DbContextOptions<PaymentDbContext> options) : DbContext(options)
	{
		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schemas.Payment);

			modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

			// TODO __##__ For any entity to be added to db schema add property with DbSet<T> to this class, or create IEntityTypeConfiguration<T>, or have relation with already added entity.
		}
	}
}
