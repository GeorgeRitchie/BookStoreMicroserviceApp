using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.CatalogWrite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class booksourceentityupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "price",
                schema: "catalog_write",
                table: "book_sources",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "stock_quantity",
                schema: "catalog_write",
                table: "book_sources",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                schema: "catalog_write",
                table: "book_sources");

            migrationBuilder.DropColumn(
                name: "stock_quantity",
                schema: "catalog_write",
                table: "book_sources");
        }
    }
}
