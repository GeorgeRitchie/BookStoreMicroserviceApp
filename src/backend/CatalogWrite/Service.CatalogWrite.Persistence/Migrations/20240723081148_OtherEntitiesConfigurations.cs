using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.CatalogWrite.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class OtherEntitiesConfigurations : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<Guid>(
				name: "book_id",
				schema: "catalog_write",
				table: "image_sources",
				type: "uniqueidentifier",
				nullable: true);

			migrationBuilder.AddColumn<Guid>(
				name: "publisher_id",
				schema: "catalog_write",
				table: "image_sources",
				type: "uniqueidentifier",
				nullable: true);

			migrationBuilder.CreateTable(
				name: "publishers",
				schema: "catalog_write",
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
				schema: "catalog_write",
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
						principalSchema: "catalog_write",
						principalTable: "publishers",
						principalColumn: "id",
						onDelete: ReferentialAction.SetNull);
				});

			migrationBuilder.CreateTable(
				name: "book_author",
				schema: "catalog_write",
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
						principalSchema: "catalog_write",
						principalTable: "authors",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_book_author_books_book_id",
						column: x => x.book_id,
						principalSchema: "catalog_write",
						principalTable: "books",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "book_category",
				schema: "catalog_write",
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
						principalSchema: "catalog_write",
						principalTable: "books",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "fk_book_category_categories_category_id",
						column: x => x.category_id,
						principalSchema: "catalog_write",
						principalTable: "categories",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "book_sources",
				schema: "catalog_write",
				columns: table => new
				{
					id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					created_on_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
					modified_on_utc = table.Column<DateTime>(type: "datetime2", nullable: true),
					format = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
						principalSchema: "catalog_write",
						principalTable: "books",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "ix_image_sources_book_id",
				schema: "catalog_write",
				table: "image_sources",
				column: "book_id");

			migrationBuilder.CreateIndex(
				name: "ix_image_sources_publisher_id",
				schema: "catalog_write",
				table: "image_sources",
				column: "publisher_id");

			migrationBuilder.CreateIndex(
				name: "ix_book_author_author_id",
				schema: "catalog_write",
				table: "book_author",
				column: "author_id");

			migrationBuilder.CreateIndex(
				name: "ix_book_category_category_id",
				schema: "catalog_write",
				table: "book_category",
				column: "category_id");

			migrationBuilder.CreateIndex(
				name: "ix_book_sources_book_id",
				schema: "catalog_write",
				table: "book_sources",
				column: "book_id");

			migrationBuilder.CreateIndex(
				name: "ix_books_isbn",
				schema: "catalog_write",
				table: "books",
				column: "isbn",
				unique: true,
				filter: "[isbn] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "ix_books_publisher_id",
				schema: "catalog_write",
				table: "books",
				column: "publisher_id");

			migrationBuilder.AddForeignKey(
				name: "fk_image_sources_books_book_id",
				schema: "catalog_write",
				table: "image_sources",
				column: "book_id",
				principalSchema: "catalog_write",
				principalTable: "books",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "fk_image_sources_publishers_publisher_id",
				schema: "catalog_write",
				table: "image_sources",
				column: "publisher_id",
				principalSchema: "catalog_write",
				principalTable: "publishers",
				principalColumn: "id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "fk_image_sources_books_book_id",
				schema: "catalog_write",
				table: "image_sources");

			migrationBuilder.DropForeignKey(
				name: "fk_image_sources_publishers_publisher_id",
				schema: "catalog_write",
				table: "image_sources");

			migrationBuilder.DropTable(
				name: "book_author",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "book_category",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "book_sources",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "books",
				schema: "catalog_write");

			migrationBuilder.DropTable(
				name: "publishers",
				schema: "catalog_write");

			migrationBuilder.DropIndex(
				name: "ix_image_sources_book_id",
				schema: "catalog_write",
				table: "image_sources");

			migrationBuilder.DropIndex(
				name: "ix_image_sources_publisher_id",
				schema: "catalog_write",
				table: "image_sources");

			migrationBuilder.DropColumn(
				name: "book_id",
				schema: "catalog_write",
				table: "image_sources");

			migrationBuilder.DropColumn(
				name: "publisher_id",
				schema: "catalog_write",
				table: "image_sources");
		}
	}
}
