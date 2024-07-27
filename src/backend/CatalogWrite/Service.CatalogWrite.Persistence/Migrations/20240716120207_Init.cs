using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.CatalogWrite.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class Init : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
				name: "catalog_write");

			migrationBuilder.CreateTable(
				name: "authors",
				schema: "catalog_write",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
					modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
					first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
					email_email_address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
					website_url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_authors", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "categories",
				schema: "catalog_write",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
					modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
					title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_categories", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "inbox_message_consumers",
				schema: "catalog_write",
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
				schema: "catalog_write",
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
				schema: "catalog_write",
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
				schema: "catalog_write",
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
				name: "image_sources",
				schema: "catalog_write",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
					discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
					Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
					author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_image_sources", x => x.id);
					table.ForeignKey(
						name: "fk_image_sources_authors_author_id",
						column: x => x.author_id,
						principalSchema: "catalog_write",
						principalTable: "authors",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_image_sources_categories_category_id",
						column: x => x.category_id,
						principalSchema: "catalog_write",
						principalTable: "categories",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "ix_authors_email_email_address",
				schema: "catalog_write",
				table: "authors",
				column: "email_email_address");

			migrationBuilder.CreateIndex(
				name: "ix_categories_title",
				schema: "catalog_write",
				table: "categories",
				column: "title",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "ix_image_sources_author_id",
				schema: "catalog_write",
				table: "image_sources",
				column: "author_id");

			migrationBuilder.CreateIndex(
				name: "ix_image_sources_category_id",
				schema: "catalog_write",
				table: "image_sources",
				column: "category_id",
				unique: true,
				filter: "[category_id] IS NOT NULL");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "image_sources",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "inbox_message_consumers",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "inbox_messages",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "outbox_message_consumers",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "outbox_messages",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "authors",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "categories",
				schema: "catalog_write");
		}
	}
}
