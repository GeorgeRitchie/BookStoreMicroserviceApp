using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Catalog.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class Init : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
				name: "catalog");

			migrationBuilder.CreateTable(
				name: "authors",
				schema: "catalog",
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
				schema: "catalog",
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
				schema: "catalog",
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
				schema: "catalog",
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
				schema: "catalog",
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
				schema: "catalog",
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
				name: "publishers",
				schema: "catalog",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
					modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
					name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					city = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					phone_number_number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
					email_email_address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
					website_url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_publishers", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "books",
				schema: "catalog",
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
					publisher_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					published_date = table.Column<DateOnly>(type: "date", nullable: true),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_books", x => x.id);
					table.ForeignKey(
						name: "fk_books_publishers_publisher_id",
						column: x => x.publisher_id,
						principalSchema: "catalog",
						principalTable: "publishers",
						principalColumn: "id",
						onDelete: ReferentialAction.SetNull);
				});

			migrationBuilder.CreateTable(
				name: "book_author",
				schema: "catalog",
				columns: table => new
				{
					book_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_book_author", x => new { x.book_id, x.author_id });
					table.ForeignKey(
						name: "fk_book_author_authors_author_id",
						column: x => x.author_id,
						principalSchema: "catalog",
						principalTable: "authors",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_book_author_books_book_id",
						column: x => x.book_id,
						principalSchema: "catalog",
						principalTable: "books",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "book_category",
				schema: "catalog",
				columns: table => new
				{
					book_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_book_category", x => new { x.book_id, x.category_id });
					table.ForeignKey(
						name: "fk_book_category_books_book_id",
						column: x => x.book_id,
						principalSchema: "catalog",
						principalTable: "books",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_book_category_categories_category_id",
						column: x => x.category_id,
						principalSchema: "catalog",
						principalTable: "categories",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "book_sources",
				schema: "catalog",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
					modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
					format = table.Column<string>(type: "nvarchar(max)", nullable: false),
					stock_quantity = table.Column<long>(type: "bigint", nullable: true),
					price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					url = table.Column<string>(type: "nvarchar(max)", nullable: true),
					preview_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
					book_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_book_sources", x => x.id);
					table.ForeignKey(
						name: "fk_book_sources_books_book_id",
						column: x => x.book_id,
						principalSchema: "catalog",
						principalTable: "books",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "image_sources",
				schema: "catalog",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
					discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
					Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
					author_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					book_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					publisher_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("pk_image_sources", x => x.id);
					table.ForeignKey(
						name: "fk_image_sources_authors_author_id",
						column: x => x.author_id,
						principalSchema: "catalog",
						principalTable: "authors",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_image_sources_books_book_id",
						column: x => x.book_id,
						principalSchema: "catalog",
						principalTable: "books",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_image_sources_categories_category_id",
						column: x => x.category_id,
						principalSchema: "catalog",
						principalTable: "categories",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_image_sources_publishers_publisher_id",
						column: x => x.publisher_id,
						principalSchema: "catalog",
						principalTable: "publishers",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "ix_authors_email_email_address",
				schema: "catalog",
				table: "authors",
				column: "email_email_address");

			migrationBuilder.CreateIndex(
				name: "ix_book_author_author_id",
				schema: "catalog",
				table: "book_author",
				column: "author_id");

			migrationBuilder.CreateIndex(
				name: "ix_book_category_category_id",
				schema: "catalog",
				table: "book_category",
				column: "category_id");

			migrationBuilder.CreateIndex(
				name: "ix_book_sources_book_id",
				schema: "catalog",
				table: "book_sources",
				column: "book_id");

			migrationBuilder.CreateIndex(
				name: "ix_books_isbn",
				schema: "catalog",
				table: "books",
				column: "isbn",
				unique: true,
				filter: "[isbn] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "ix_books_publisher_id",
				schema: "catalog",
				table: "books",
				column: "publisher_id");

			migrationBuilder.CreateIndex(
				name: "ix_categories_title",
				schema: "catalog",
				table: "categories",
				column: "title",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "ix_image_sources_author_id",
				schema: "catalog",
				table: "image_sources",
				column: "author_id");

			migrationBuilder.CreateIndex(
				name: "ix_image_sources_book_id",
				schema: "catalog",
				table: "image_sources",
				column: "book_id");

			migrationBuilder.CreateIndex(
				name: "ix_image_sources_category_id",
				schema: "catalog",
				table: "image_sources",
				column: "category_id",
				unique: true,
				filter: "[category_id] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "ix_image_sources_publisher_id",
				schema: "catalog",
				table: "image_sources",
				column: "publisher_id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "book_author",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "book_category",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "book_sources",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "image_sources",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "inbox_message_consumers",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "inbox_messages",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "outbox_message_consumers",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "outbox_messages",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "authors",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "books",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "categories",
				schema: "catalog");

			migrationBuilder.DropTable(
				name: "publishers",
				schema: "catalog");
		}
	}
}
