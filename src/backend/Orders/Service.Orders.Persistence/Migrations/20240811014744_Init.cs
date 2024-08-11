using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "order");

            migrationBuilder.CreateTable(
                name: "inbox_message_consumers",
                schema: "order",
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
                schema: "order",
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
                name: "orders",
                schema: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ordered_date_time_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "outbox_message_consumers",
                schema: "order",
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
                schema: "order",
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
                name: "order_items",
                schema: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    book_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    cover = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    language = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    source_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    format = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "order",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                schema: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    error_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    error_message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    error_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    error_source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_interaction_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                    table.ForeignKey(
                        name: "fk_payments_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "order",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shipments",
                schema: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address_country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_district = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address_home = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shipments", x => x.id);
                    table.ForeignKey(
                        name: "fk_shipments_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "order",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_items_book_id",
                schema: "order",
                table: "order_items",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_order_id",
                schema: "order",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_items_source_id",
                schema: "order",
                table: "order_items",
                column: "source_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_customer_id",
                schema: "order",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_order_id",
                schema: "order",
                table: "payments",
                column: "order_id",
                unique: true,
                filter: "[order_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_shipments_order_id",
                schema: "order",
                table: "shipments",
                column: "order_id",
                unique: true,
                filter: "[order_id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inbox_message_consumers",
                schema: "order");

            migrationBuilder.DropTable(
                name: "inbox_messages",
                schema: "order");

            migrationBuilder.DropTable(
                name: "order_items",
                schema: "order");

            migrationBuilder.DropTable(
                name: "outbox_message_consumers",
                schema: "order");

            migrationBuilder.DropTable(
                name: "outbox_messages",
                schema: "order");

            migrationBuilder.DropTable(
                name: "payments",
                schema: "order");

            migrationBuilder.DropTable(
                name: "shipments",
                schema: "order");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "order");
        }
    }
}
