﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Identity.Data.Migrations.ConfigurationDb
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ids_configuration");

            migrationBuilder.CreateTable(
                name: "ApiResources",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enabled = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    allowed_access_token_signing_algorithms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    show_in_discovery_document = table.Column<bool>(type: "bit", nullable: false),
                    require_resource_indicator = table.Column<bool>(type: "bit", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_accessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    non_editable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_resources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopes",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enabled = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    required = table.Column<bool>(type: "bit", nullable: false),
                    emphasize = table.Column<bool>(type: "bit", nullable: false),
                    show_in_discovery_document = table.Column<bool>(type: "bit", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_accessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    non_editable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_scopes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enabled = table.Column<bool>(type: "bit", nullable: false),
                    client_id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    protocol_type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    require_client_secret = table.Column<bool>(type: "bit", nullable: false),
                    client_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    client_uri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    logo_uri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    require_consent = table.Column<bool>(type: "bit", nullable: false),
                    allow_remember_consent = table.Column<bool>(type: "bit", nullable: false),
                    always_include_user_claims_in_id_token = table.Column<bool>(type: "bit", nullable: false),
                    require_pkce = table.Column<bool>(type: "bit", nullable: false),
                    allow_plain_text_pkce = table.Column<bool>(type: "bit", nullable: false),
                    require_request_object = table.Column<bool>(type: "bit", nullable: false),
                    allow_access_tokens_via_browser = table.Column<bool>(type: "bit", nullable: false),
                    require_d_po_p = table.Column<bool>(type: "bit", nullable: false),
                    d_po_p_validation_mode = table.Column<int>(type: "int", nullable: false),
                    d_po_p_clock_skew = table.Column<TimeSpan>(type: "time", nullable: false),
                    front_channel_logout_uri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    front_channel_logout_session_required = table.Column<bool>(type: "bit", nullable: false),
                    back_channel_logout_uri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    back_channel_logout_session_required = table.Column<bool>(type: "bit", nullable: false),
                    allow_offline_access = table.Column<bool>(type: "bit", nullable: false),
                    identity_token_lifetime = table.Column<int>(type: "int", nullable: false),
                    allowed_identity_token_signing_algorithms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    access_token_lifetime = table.Column<int>(type: "int", nullable: false),
                    authorization_code_lifetime = table.Column<int>(type: "int", nullable: false),
                    consent_lifetime = table.Column<int>(type: "int", nullable: true),
                    absolute_refresh_token_lifetime = table.Column<int>(type: "int", nullable: false),
                    sliding_refresh_token_lifetime = table.Column<int>(type: "int", nullable: false),
                    refresh_token_usage = table.Column<int>(type: "int", nullable: false),
                    update_access_token_claims_on_refresh = table.Column<bool>(type: "bit", nullable: false),
                    refresh_token_expiration = table.Column<int>(type: "int", nullable: false),
                    access_token_type = table.Column<int>(type: "int", nullable: false),
                    enable_local_login = table.Column<bool>(type: "bit", nullable: false),
                    include_jwt_id = table.Column<bool>(type: "bit", nullable: false),
                    always_send_client_claims = table.Column<bool>(type: "bit", nullable: false),
                    client_claims_prefix = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    pair_wise_subject_salt = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    initiate_login_uri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    user_sso_lifetime = table.Column<int>(type: "int", nullable: true),
                    user_code_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    device_code_lifetime = table.Column<int>(type: "int", nullable: false),
                    ciba_lifetime = table.Column<int>(type: "int", nullable: true),
                    polling_interval = table.Column<int>(type: "int", nullable: true),
                    coordinate_lifetime_with_user_session = table.Column<bool>(type: "bit", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_accessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    non_editable = table.Column<bool>(type: "bit", nullable: false),
                    pushed_authorization_lifetime = table.Column<int>(type: "int", nullable: true),
                    require_pushed_authorization = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityProviders",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scheme = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    enabled = table.Column<bool>(type: "bit", nullable: false),
                    type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    properties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_accessed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    non_editable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_providers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResources",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enabled = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    required = table.Column<bool>(type: "bit", nullable: false),
                    emphasize = table.Column<bool>(type: "bit", nullable: false),
                    show_in_discovery_document = table.Column<bool>(type: "bit", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    non_editable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_resources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceClaims",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    api_resource_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_resource_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_resource_claims_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalSchema: "ids_configuration",
                        principalTable: "ApiResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceProperties",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    api_resource_id = table.Column<int>(type: "int", nullable: false),
                    key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_resource_properties", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_resource_properties_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalSchema: "ids_configuration",
                        principalTable: "ApiResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceScopes",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scope = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    api_resource_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_resource_scopes", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_resource_scopes_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalSchema: "ids_configuration",
                        principalTable: "ApiResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiResourceSecrets",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    api_resource_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_resource_secrets", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_resource_secrets_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalSchema: "ids_configuration",
                        principalTable: "ApiResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeClaims",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scope_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_scope_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_scope_claims_api_scopes_scope_id",
                        column: x => x.scope_id,
                        principalSchema: "ids_configuration",
                        principalTable: "ApiScopes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiScopeProperties",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scope_id = table.Column<int>(type: "int", nullable: false),
                    key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_scope_properties", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_scope_properties_api_scopes_scope_id",
                        column: x => x.scope_id,
                        principalSchema: "ids_configuration",
                        principalTable: "ApiScopes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientClaims",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_claims_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "ids_configuration",
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientCorsOrigins",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    origin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_cors_origins", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_cors_origins_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "ids_configuration",
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientGrantTypes",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grant_type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_grant_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_grant_types_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "ids_configuration",
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientIdPRestrictions",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    provider = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_id_p_restrictions", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_id_p_restrictions_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "ids_configuration",
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientPostLogoutRedirectUris",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    post_logout_redirect_uri = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_post_logout_redirect_uris", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_post_logout_redirect_uris_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "ids_configuration",
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientProperties",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_properties", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_properties_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "ids_configuration",
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientRedirectUris",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    redirect_uri = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_redirect_uris", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_redirect_uris_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "ids_configuration",
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientScopes",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scope = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_scopes", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_scopes_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "ids_configuration",
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSecrets",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    value = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_secrets", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_secrets_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "ids_configuration",
                        principalTable: "Clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResourceClaims",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    identity_resource_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_resource_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_identity_resource_claims_identity_resources_identity_resource_id",
                        column: x => x.identity_resource_id,
                        principalSchema: "ids_configuration",
                        principalTable: "IdentityResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityResourceProperties",
                schema: "ids_configuration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    identity_resource_id = table.Column<int>(type: "int", nullable: false),
                    key = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_resource_properties", x => x.id);
                    table.ForeignKey(
                        name: "fk_identity_resource_properties_identity_resources_identity_resource_id",
                        column: x => x.identity_resource_id,
                        principalSchema: "ids_configuration",
                        principalTable: "IdentityResources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_api_resource_claims_api_resource_id_type",
                schema: "ids_configuration",
                table: "ApiResourceClaims",
                columns: new[] { "api_resource_id", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_api_resource_properties_api_resource_id_key",
                schema: "ids_configuration",
                table: "ApiResourceProperties",
                columns: new[] { "api_resource_id", "key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_api_resources_name",
                schema: "ids_configuration",
                table: "ApiResources",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_api_resource_scopes_api_resource_id_scope",
                schema: "ids_configuration",
                table: "ApiResourceScopes",
                columns: new[] { "api_resource_id", "scope" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_api_resource_secrets_api_resource_id",
                schema: "ids_configuration",
                table: "ApiResourceSecrets",
                column: "api_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_scope_claims_scope_id_type",
                schema: "ids_configuration",
                table: "ApiScopeClaims",
                columns: new[] { "scope_id", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_api_scope_properties_scope_id_key",
                schema: "ids_configuration",
                table: "ApiScopeProperties",
                columns: new[] { "scope_id", "key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_api_scopes_name",
                schema: "ids_configuration",
                table: "ApiScopes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_client_claims_client_id_type_value",
                schema: "ids_configuration",
                table: "ClientClaims",
                columns: new[] { "client_id", "type", "value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_client_cors_origins_client_id_origin",
                schema: "ids_configuration",
                table: "ClientCorsOrigins",
                columns: new[] { "client_id", "origin" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_client_grant_types_client_id_grant_type",
                schema: "ids_configuration",
                table: "ClientGrantTypes",
                columns: new[] { "client_id", "grant_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_client_id_p_restrictions_client_id_provider",
                schema: "ids_configuration",
                table: "ClientIdPRestrictions",
                columns: new[] { "client_id", "provider" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_client_post_logout_redirect_uris_client_id_post_logout_redirect_uri",
                schema: "ids_configuration",
                table: "ClientPostLogoutRedirectUris",
                columns: new[] { "client_id", "post_logout_redirect_uri" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_client_properties_client_id_key",
                schema: "ids_configuration",
                table: "ClientProperties",
                columns: new[] { "client_id", "key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_client_redirect_uris_client_id_redirect_uri",
                schema: "ids_configuration",
                table: "ClientRedirectUris",
                columns: new[] { "client_id", "redirect_uri" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_clients_client_id",
                schema: "ids_configuration",
                table: "Clients",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_client_scopes_client_id_scope",
                schema: "ids_configuration",
                table: "ClientScopes",
                columns: new[] { "client_id", "scope" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_client_secrets_client_id",
                schema: "ids_configuration",
                table: "ClientSecrets",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_identity_providers_scheme",
                schema: "ids_configuration",
                table: "IdentityProviders",
                column: "scheme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_identity_resource_claims_identity_resource_id_type",
                schema: "ids_configuration",
                table: "IdentityResourceClaims",
                columns: new[] { "identity_resource_id", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_identity_resource_properties_identity_resource_id_key",
                schema: "ids_configuration",
                table: "IdentityResourceProperties",
                columns: new[] { "identity_resource_id", "key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_identity_resources_name",
                schema: "ids_configuration",
                table: "IdentityResources",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiResourceClaims",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ApiResourceProperties",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ApiResourceScopes",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ApiResourceSecrets",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ApiScopeClaims",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ApiScopeProperties",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ClientClaims",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ClientCorsOrigins",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ClientGrantTypes",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ClientIdPRestrictions",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ClientPostLogoutRedirectUris",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ClientProperties",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ClientRedirectUris",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ClientScopes",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ClientSecrets",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "IdentityProviders",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "IdentityResourceClaims",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "IdentityResourceProperties",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ApiResources",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "ApiScopes",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "ids_configuration");

            migrationBuilder.DropTable(
                name: "IdentityResources",
                schema: "ids_configuration");
        }
    }
}
