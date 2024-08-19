using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Carts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cart");

            migrationBuilder.CreateTable(
                name: "books",
                schema: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    language = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                schema: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_carts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inbox_message_consumers",
                schema: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox_message_consumers", x => new { x.id, x.name });
                });

            migrationBuilder.CreateTable(
                name: "inbox_messages",
                schema: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_message_consumers",
                schema: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_message_consumers", x => new { x.id, x.name });
                });

            migrationBuilder.CreateTable(
                name: "outbox_messages",
                schema: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book_sources",
                schema: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    format = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    book_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book_sources", x => x.id);
                    table.ForeignKey(
                        name: "fk_book_sources_books_book_id",
                        column: x => x.book_id,
                        principalSchema: "cart",
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                schema: "cart",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    book_source_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cart_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cart_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_cart_items_book_sources_book_source_id",
                        column: x => x.book_source_id,
                        principalSchema: "cart",
                        principalTable: "book_sources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cart_items_carts_cart_id",
                        column: x => x.cart_id,
                        principalSchema: "cart",
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_book_sources_book_id",
                schema: "cart",
                table: "book_sources",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_book_source_id",
                schema: "cart",
                table: "cart_items",
                column: "book_source_id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_cart_id",
                schema: "cart",
                table: "cart_items",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "ix_carts_customer_id",
                schema: "cart",
                table: "carts",
                column: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_items",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "inbox_message_consumers",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "outbox_message_consumers",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "book_sources",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "carts",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "books",
                schema: "cart");
        }
    }
}
