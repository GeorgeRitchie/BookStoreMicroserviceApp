using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Shipments.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shipment");

            migrationBuilder.CreateTable(
                name: "books",
                schema: "shipment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    language = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    age_rating = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "inbox_message_consumers",
                schema: "shipment",
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
                schema: "shipment",
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
                schema: "shipment",
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
                schema: "shipment",
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
                name: "shipments",
                schema: "shipment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ordered_date_time_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    address_country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_home = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shipments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "book_sources",
                schema: "shipment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    format = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    book_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book_sources", x => x.id);
                    table.ForeignKey(
                        name: "fk_book_sources_books_book_id",
                        column: x => x.book_id,
                        principalSchema: "shipment",
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "shipment_items",
                schema: "shipment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    book_source_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    shipment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shipment_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_shipment_items_book_sources_book_source_id",
                        column: x => x.book_source_id,
                        principalSchema: "shipment",
                        principalTable: "book_sources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_shipment_items_shipments_shipment_id",
                        column: x => x.shipment_id,
                        principalSchema: "shipment",
                        principalTable: "shipments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_book_sources_book_id",
                schema: "shipment",
                table: "book_sources",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_shipment_items_book_source_id",
                schema: "shipment",
                table: "shipment_items",
                column: "book_source_id");

            migrationBuilder.CreateIndex(
                name: "ix_shipment_items_shipment_id",
                schema: "shipment",
                table: "shipment_items",
                column: "shipment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inbox_message_consumers",
                schema: "shipment");

            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "shipment");

            migrationBuilder.DropTable(
                name: "outbox_message_consumers",
                schema: "shipment");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "shipment");

            migrationBuilder.DropTable(
                name: "shipment_items",
                schema: "shipment");

            migrationBuilder.DropTable(
                name: "book_sources",
                schema: "shipment");

            migrationBuilder.DropTable(
                name: "shipments",
                schema: "shipment");

            migrationBuilder.DropTable(
                name: "books",
                schema: "shipment");
        }
    }
}
