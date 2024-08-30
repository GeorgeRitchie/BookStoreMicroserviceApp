using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Identity.Data.Migrations.PersistedGrantDb
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ids_persisted_grant");

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                schema: "ids_persisted_grant",
                columns: table => new
                {
                    user_code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    device_code = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    subject_id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    session_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    client_id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    creation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_device_codes", x => x.user_code);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                schema: "ids_persisted_grant",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    version = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    is_x509certificate = table.Column<bool>(type: "bit", nullable: false),
                    data_protected = table.Column<bool>(type: "bit", nullable: false),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_keys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                schema: "ids_persisted_grant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    subject_id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    session_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    client_id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    creation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    consumed_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_persisted_grants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PushedAuthorizationRequests",
                schema: "ids_persisted_grant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reference_value_hash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    expires_at_utc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    parameters = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pushed_authorization_requests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ServerSideSessions",
                schema: "ids_persisted_grant",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    key = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    scheme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    subject_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    session_id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    display_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    renewed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_server_side_sessions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_device_codes_device_code",
                schema: "ids_persisted_grant",
                table: "DeviceCodes",
                column: "device_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_device_codes_expiration",
                schema: "ids_persisted_grant",
                table: "DeviceCodes",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "ix_keys_use",
                schema: "ids_persisted_grant",
                table: "Keys",
                column: "use");

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_consumed_time",
                schema: "ids_persisted_grant",
                table: "PersistedGrants",
                column: "consumed_time");

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_expiration",
                schema: "ids_persisted_grant",
                table: "PersistedGrants",
                column: "expiration");

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_key",
                schema: "ids_persisted_grant",
                table: "PersistedGrants",
                column: "key",
                unique: true,
                filter: "[key] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_subject_id_client_id_type",
                schema: "ids_persisted_grant",
                table: "PersistedGrants",
                columns: new[] { "subject_id", "client_id", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_persisted_grants_subject_id_session_id_type",
                schema: "ids_persisted_grant",
                table: "PersistedGrants",
                columns: new[] { "subject_id", "session_id", "type" });

            migrationBuilder.CreateIndex(
                name: "ix_pushed_authorization_requests_expires_at_utc",
                schema: "ids_persisted_grant",
                table: "PushedAuthorizationRequests",
                column: "expires_at_utc");

            migrationBuilder.CreateIndex(
                name: "ix_pushed_authorization_requests_reference_value_hash",
                schema: "ids_persisted_grant",
                table: "PushedAuthorizationRequests",
                column: "reference_value_hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_display_name",
                schema: "ids_persisted_grant",
                table: "ServerSideSessions",
                column: "display_name");

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_expires",
                schema: "ids_persisted_grant",
                table: "ServerSideSessions",
                column: "expires");

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_key",
                schema: "ids_persisted_grant",
                table: "ServerSideSessions",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_session_id",
                schema: "ids_persisted_grant",
                table: "ServerSideSessions",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "ix_server_side_sessions_subject_id",
                schema: "ids_persisted_grant",
                table: "ServerSideSessions",
                column: "subject_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceCodes",
                schema: "ids_persisted_grant");

            migrationBuilder.DropTable(
                name: "Keys",
                schema: "ids_persisted_grant");

            migrationBuilder.DropTable(
                name: "PersistedGrants",
                schema: "ids_persisted_grant");

            migrationBuilder.DropTable(
                name: "PushedAuthorizationRequests",
                schema: "ids_persisted_grant");

            migrationBuilder.DropTable(
                name: "ServerSideSessions",
                schema: "ids_persisted_grant");
        }
    }
}
