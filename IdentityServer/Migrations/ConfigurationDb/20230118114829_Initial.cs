#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Migrations.ConfigurationDb;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable("ApiResources",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         Enabled = table.Column<bool>("bit", nullable: false),
                                         Name = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                                         DisplayName =
                                             table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                                         Description =
                                             table.Column<string>("nvarchar(1000)", maxLength: 1000, nullable: true),
                                         AllowedAccessTokenSigningAlgorithms =
                                             table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                                         ShowInDiscoveryDocument = table.Column<bool>("bit", nullable: false),
                                         Created = table.Column<DateTime>("datetime2", nullable: false),
                                         Updated = table.Column<DateTime>("datetime2", nullable: true),
                                         LastAccessed = table.Column<DateTime>("datetime2", nullable: true),
                                         NonEditable = table.Column<bool>("bit", nullable: false)
                                     },
                                     constraints: table => { table.PrimaryKey("PK_ApiResources", x => x.Id); });

        migrationBuilder.CreateTable("ApiScopes",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         Enabled = table.Column<bool>("bit", nullable: false),
                                         Name = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                                         DisplayName =
                                             table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                                         Description =
                                             table.Column<string>("nvarchar(1000)", maxLength: 1000, nullable: true),
                                         Required = table.Column<bool>("bit", nullable: false),
                                         Emphasize = table.Column<bool>("bit", nullable: false),
                                         ShowInDiscoveryDocument = table.Column<bool>("bit", nullable: false)
                                     },
                                     constraints: table => { table.PrimaryKey("PK_ApiScopes", x => x.Id); });

        migrationBuilder.CreateTable("Clients",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         Enabled = table.Column<bool>("bit", nullable: false),
                                         ClientId = table.Column<string>("nvarchar(200)",
                                                                         maxLength: 200,
                                                                         nullable: false),
                                         ProtocolType =
                                             table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                                         RequireClientSecret = table.Column<bool>("bit", nullable: false),
                                         ClientName =
                                             table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                                         Description =
                                             table.Column<string>("nvarchar(1000)", maxLength: 1000, nullable: true),
                                         ClientUri = table.Column<string>("nvarchar(2000)",
                                                                          maxLength: 2000,
                                                                          nullable: true),
                                         LogoUri = table.Column<string>("nvarchar(2000)",
                                                                        maxLength: 2000,
                                                                        nullable: true),
                                         RequireConsent = table.Column<bool>("bit", nullable: false),
                                         AllowRememberConsent = table.Column<bool>("bit", nullable: false),
                                         AlwaysIncludeUserClaimsInIdToken = table.Column<bool>("bit", nullable: false),
                                         RequirePkce = table.Column<bool>("bit", nullable: false),
                                         AllowPlainTextPkce = table.Column<bool>("bit", nullable: false),
                                         RequireRequestObject = table.Column<bool>("bit", nullable: false),
                                         AllowAccessTokensViaBrowser = table.Column<bool>("bit", nullable: false),
                                         FrontChannelLogoutUri =
                                             table.Column<string>("nvarchar(2000)", maxLength: 2000, nullable: true),
                                         FrontChannelLogoutSessionRequired = table.Column<bool>("bit", nullable: false),
                                         BackChannelLogoutUri =
                                             table.Column<string>("nvarchar(2000)", maxLength: 2000, nullable: true),
                                         BackChannelLogoutSessionRequired = table.Column<bool>("bit", nullable: false),
                                         AllowOfflineAccess = table.Column<bool>("bit", nullable: false),
                                         IdentityTokenLifetime = table.Column<int>("int", nullable: false),
                                         AllowedIdentityTokenSigningAlgorithms =
                                             table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                                         AccessTokenLifetime = table.Column<int>("int", nullable: false),
                                         AuthorizationCodeLifetime = table.Column<int>("int", nullable: false),
                                         ConsentLifetime = table.Column<int>("int", nullable: true),
                                         AbsoluteRefreshTokenLifetime = table.Column<int>("int", nullable: false),
                                         SlidingRefreshTokenLifetime = table.Column<int>("int", nullable: false),
                                         RefreshTokenUsage = table.Column<int>("int", nullable: false),
                                         UpdateAccessTokenClaimsOnRefresh = table.Column<bool>("bit", nullable: false),
                                         RefreshTokenExpiration = table.Column<int>("int", nullable: false),
                                         AccessTokenType = table.Column<int>("int", nullable: false),
                                         EnableLocalLogin = table.Column<bool>("bit", nullable: false),
                                         IncludeJwtId = table.Column<bool>("bit", nullable: false),
                                         AlwaysSendClientClaims = table.Column<bool>("bit", nullable: false),
                                         ClientClaimsPrefix =
                                             table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                                         PairWiseSubjectSalt =
                                             table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                                         Created = table.Column<DateTime>("datetime2", nullable: false),
                                         Updated = table.Column<DateTime>("datetime2", nullable: true),
                                         LastAccessed = table.Column<DateTime>("datetime2", nullable: true),
                                         UserSsoLifetime = table.Column<int>("int", nullable: true),
                                         UserCodeType =
                                             table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                                         DeviceCodeLifetime = table.Column<int>("int", nullable: false),
                                         NonEditable = table.Column<bool>("bit", nullable: false)
                                     },
                                     constraints: table => { table.PrimaryKey("PK_Clients", x => x.Id); });

        migrationBuilder.CreateTable("IdentityResources",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         Enabled = table.Column<bool>("bit", nullable: false),
                                         Name = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                                         DisplayName =
                                             table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                                         Description =
                                             table.Column<string>("nvarchar(1000)", maxLength: 1000, nullable: true),
                                         Required = table.Column<bool>("bit", nullable: false),
                                         Emphasize = table.Column<bool>("bit", nullable: false),
                                         ShowInDiscoveryDocument = table.Column<bool>("bit", nullable: false),
                                         Created = table.Column<DateTime>("datetime2", nullable: false),
                                         Updated = table.Column<DateTime>("datetime2", nullable: true),
                                         NonEditable = table.Column<bool>("bit", nullable: false)
                                     },
                                     constraints: table => { table.PrimaryKey("PK_IdentityResources", x => x.Id); });

        migrationBuilder.CreateTable("ApiResourceClaims",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         ApiResourceId = table.Column<int>("int", nullable: false),
                                         Type = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApiResourceClaims", x => x.Id);

                                         table.ForeignKey("FK_ApiResourceClaims_ApiResources_ApiResourceId",
                                                          x => x.ApiResourceId,
                                                          "ApiResources",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ApiResourceProperties",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         ApiResourceId = table.Column<int>("int", nullable: false),
                                         Key = table.Column<string>("nvarchar(250)", maxLength: 250, nullable: false),
                                         Value = table.Column<string>("nvarchar(2000)",
                                                                      maxLength: 2000,
                                                                      nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApiResourceProperties", x => x.Id);

                                         table.ForeignKey("FK_ApiResourceProperties_ApiResources_ApiResourceId",
                                                          x => x.ApiResourceId,
                                                          "ApiResources",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ApiResourceScopes",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         Scope = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                                         ApiResourceId = table.Column<int>("int", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApiResourceScopes", x => x.Id);

                                         table.ForeignKey("FK_ApiResourceScopes_ApiResources_ApiResourceId",
                                                          x => x.ApiResourceId,
                                                          "ApiResources",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ApiResourceSecrets",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         ApiResourceId = table.Column<int>("int", nullable: false),
                                         Description =
                                             table.Column<string>("nvarchar(1000)", maxLength: 1000, nullable: true),
                                         Value = table.Column<string>("nvarchar(4000)",
                                                                      maxLength: 4000,
                                                                      nullable: false),
                                         Expiration = table.Column<DateTime>("datetime2", nullable: true),
                                         Type = table.Column<string>("nvarchar(250)", maxLength: 250, nullable: false),
                                         Created = table.Column<DateTime>("datetime2", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApiResourceSecrets", x => x.Id);

                                         table.ForeignKey("FK_ApiResourceSecrets_ApiResources_ApiResourceId",
                                                          x => x.ApiResourceId,
                                                          "ApiResources",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ApiScopeClaims",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         ScopeId = table.Column<int>("int", nullable: false),
                                         Type = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApiScopeClaims", x => x.Id);

                                         table.ForeignKey("FK_ApiScopeClaims_ApiScopes_ScopeId",
                                                          x => x.ScopeId,
                                                          "ApiScopes",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ApiScopeProperties",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         ScopeId = table.Column<int>("int", nullable: false),
                                         Key = table.Column<string>("nvarchar(250)", maxLength: 250, nullable: false),
                                         Value = table.Column<string>("nvarchar(2000)",
                                                                      maxLength: 2000,
                                                                      nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ApiScopeProperties", x => x.Id);

                                         table.ForeignKey("FK_ApiScopeProperties_ApiScopes_ScopeId",
                                                          x => x.ScopeId,
                                                          "ApiScopes",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ClientClaims",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         Type = table.Column<string>("nvarchar(250)", maxLength: 250, nullable: false),
                                         Value = table.Column<string>("nvarchar(250)", maxLength: 250, nullable: false),
                                         ClientId = table.Column<int>("int", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ClientClaims", x => x.Id);

                                         table.ForeignKey("FK_ClientClaims_Clients_ClientId",
                                                          x => x.ClientId,
                                                          "Clients",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ClientCorsOrigins",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         Origin =
                                             table.Column<string>("nvarchar(150)", maxLength: 150, nullable: false),
                                         ClientId = table.Column<int>("int", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ClientCorsOrigins", x => x.Id);

                                         table.ForeignKey("FK_ClientCorsOrigins_Clients_ClientId",
                                                          x => x.ClientId,
                                                          "Clients",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ClientGrantTypes",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         GrantType = table.Column<string>("nvarchar(250)",
                                                                          maxLength: 250,
                                                                          nullable: false),
                                         ClientId = table.Column<int>("int", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ClientGrantTypes", x => x.Id);

                                         table.ForeignKey("FK_ClientGrantTypes_Clients_ClientId",
                                                          x => x.ClientId,
                                                          "Clients",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ClientIdPRestrictions",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         Provider = table.Column<string>("nvarchar(200)",
                                                                         maxLength: 200,
                                                                         nullable: false),
                                         ClientId = table.Column<int>("int", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ClientIdPRestrictions", x => x.Id);

                                         table.ForeignKey("FK_ClientIdPRestrictions_Clients_ClientId",
                                                          x => x.ClientId,
                                                          "Clients",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ClientPostLogoutRedirectUris",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         PostLogoutRedirectUri =
                                             table.Column<string>("nvarchar(2000)", maxLength: 2000, nullable: false),
                                         ClientId = table.Column<int>("int", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ClientPostLogoutRedirectUris", x => x.Id);

                                         table.ForeignKey("FK_ClientPostLogoutRedirectUris_Clients_ClientId",
                                                          x => x.ClientId,
                                                          "Clients",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ClientProperties",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         ClientId = table.Column<int>("int", nullable: false),
                                         Key = table.Column<string>("nvarchar(250)", maxLength: 250, nullable: false),
                                         Value = table.Column<string>("nvarchar(2000)",
                                                                      maxLength: 2000,
                                                                      nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ClientProperties", x => x.Id);

                                         table.ForeignKey("FK_ClientProperties_Clients_ClientId",
                                                          x => x.ClientId,
                                                          "Clients",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ClientRedirectUris",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         RedirectUri =
                                             table.Column<string>("nvarchar(2000)", maxLength: 2000, nullable: false),
                                         ClientId = table.Column<int>("int", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ClientRedirectUris", x => x.Id);

                                         table.ForeignKey("FK_ClientRedirectUris_Clients_ClientId",
                                                          x => x.ClientId,
                                                          "Clients",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ClientScopes",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         Scope = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                                         ClientId = table.Column<int>("int", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ClientScopes", x => x.Id);

                                         table.ForeignKey("FK_ClientScopes_Clients_ClientId",
                                                          x => x.ClientId,
                                                          "Clients",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("ClientSecrets",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         ClientId = table.Column<int>("int", nullable: false),
                                         Description =
                                             table.Column<string>("nvarchar(2000)", maxLength: 2000, nullable: true),
                                         Value = table.Column<string>("nvarchar(4000)",
                                                                      maxLength: 4000,
                                                                      nullable: false),
                                         Expiration = table.Column<DateTime>("datetime2", nullable: true),
                                         Type = table.Column<string>("nvarchar(250)", maxLength: 250, nullable: false),
                                         Created = table.Column<DateTime>("datetime2", nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_ClientSecrets", x => x.Id);

                                         table.ForeignKey("FK_ClientSecrets_Clients_ClientId",
                                                          x => x.ClientId,
                                                          "Clients",
                                                          "Id",
                                                          onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("IdentityResourceClaims",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         IdentityResourceId = table.Column<int>("int", nullable: false),
                                         Type = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_IdentityResourceClaims", x => x.Id);

                                         table
                                             .ForeignKey("FK_IdentityResourceClaims_IdentityResources_IdentityResourceId",
                                                         x => x.IdentityResourceId,
                                                         "IdentityResources",
                                                         "Id",
                                                         onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateTable("IdentityResourceProperties",
                                     table => new
                                     {
                                         Id = table.Column<int>("int", nullable: false)
                                                   .Annotation("SqlServer:Identity", "1, 1"),
                                         IdentityResourceId = table.Column<int>("int", nullable: false),
                                         Key = table.Column<string>("nvarchar(250)", maxLength: 250, nullable: false),
                                         Value = table.Column<string>("nvarchar(2000)",
                                                                      maxLength: 2000,
                                                                      nullable: false)
                                     },
                                     constraints: table =>
                                     {
                                         table.PrimaryKey("PK_IdentityResourceProperties", x => x.Id);

                                         table
                                             .ForeignKey("FK_IdentityResourceProperties_IdentityResources_IdentityResourceId",
                                                         x => x.IdentityResourceId,
                                                         "IdentityResources",
                                                         "Id",
                                                         onDelete: ReferentialAction.Cascade);
                                     });

        migrationBuilder.CreateIndex("IX_ApiResourceClaims_ApiResourceId",
                                     "ApiResourceClaims",
                                     "ApiResourceId");

        migrationBuilder.CreateIndex("IX_ApiResourceProperties_ApiResourceId",
                                     "ApiResourceProperties",
                                     "ApiResourceId");

        migrationBuilder.CreateIndex("IX_ApiResources_Name",
                                     "ApiResources",
                                     "Name",
                                     unique: true);

        migrationBuilder.CreateIndex("IX_ApiResourceScopes_ApiResourceId",
                                     "ApiResourceScopes",
                                     "ApiResourceId");

        migrationBuilder.CreateIndex("IX_ApiResourceSecrets_ApiResourceId",
                                     "ApiResourceSecrets",
                                     "ApiResourceId");

        migrationBuilder.CreateIndex("IX_ApiScopeClaims_ScopeId",
                                     "ApiScopeClaims",
                                     "ScopeId");

        migrationBuilder.CreateIndex("IX_ApiScopeProperties_ScopeId",
                                     "ApiScopeProperties",
                                     "ScopeId");

        migrationBuilder.CreateIndex("IX_ApiScopes_Name",
                                     "ApiScopes",
                                     "Name",
                                     unique: true);

        migrationBuilder.CreateIndex("IX_ClientClaims_ClientId",
                                     "ClientClaims",
                                     "ClientId");

        migrationBuilder.CreateIndex("IX_ClientCorsOrigins_ClientId",
                                     "ClientCorsOrigins",
                                     "ClientId");

        migrationBuilder.CreateIndex("IX_ClientGrantTypes_ClientId",
                                     "ClientGrantTypes",
                                     "ClientId");

        migrationBuilder.CreateIndex("IX_ClientIdPRestrictions_ClientId",
                                     "ClientIdPRestrictions",
                                     "ClientId");

        migrationBuilder.CreateIndex("IX_ClientPostLogoutRedirectUris_ClientId",
                                     "ClientPostLogoutRedirectUris",
                                     "ClientId");

        migrationBuilder.CreateIndex("IX_ClientProperties_ClientId",
                                     "ClientProperties",
                                     "ClientId");

        migrationBuilder.CreateIndex("IX_ClientRedirectUris_ClientId",
                                     "ClientRedirectUris",
                                     "ClientId");

        migrationBuilder.CreateIndex("IX_Clients_ClientId",
                                     "Clients",
                                     "ClientId",
                                     unique: true);

        migrationBuilder.CreateIndex("IX_ClientScopes_ClientId",
                                     "ClientScopes",
                                     "ClientId");

        migrationBuilder.CreateIndex("IX_ClientSecrets_ClientId",
                                     "ClientSecrets",
                                     "ClientId");

        migrationBuilder.CreateIndex("IX_IdentityResourceClaims_IdentityResourceId",
                                     "IdentityResourceClaims",
                                     "IdentityResourceId");

        migrationBuilder.CreateIndex("IX_IdentityResourceProperties_IdentityResourceId",
                                     "IdentityResourceProperties",
                                     "IdentityResourceId");

        migrationBuilder.CreateIndex("IX_IdentityResources_Name",
                                     "IdentityResources",
                                     "Name",
                                     unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("ApiResourceClaims");

        migrationBuilder.DropTable("ApiResourceProperties");

        migrationBuilder.DropTable("ApiResourceScopes");

        migrationBuilder.DropTable("ApiResourceSecrets");

        migrationBuilder.DropTable("ApiScopeClaims");

        migrationBuilder.DropTable("ApiScopeProperties");

        migrationBuilder.DropTable("ClientClaims");

        migrationBuilder.DropTable("ClientCorsOrigins");

        migrationBuilder.DropTable("ClientGrantTypes");

        migrationBuilder.DropTable("ClientIdPRestrictions");

        migrationBuilder.DropTable("ClientPostLogoutRedirectUris");

        migrationBuilder.DropTable("ClientProperties");

        migrationBuilder.DropTable("ClientRedirectUris");

        migrationBuilder.DropTable("ClientScopes");

        migrationBuilder.DropTable("ClientSecrets");

        migrationBuilder.DropTable("IdentityResourceClaims");

        migrationBuilder.DropTable("IdentityResourceProperties");

        migrationBuilder.DropTable("ApiResources");

        migrationBuilder.DropTable("ApiScopes");

        migrationBuilder.DropTable("Clients");

        migrationBuilder.DropTable("IdentityResources");
    }
}