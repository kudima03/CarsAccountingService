#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Migrations.PersistedGrantsDb;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "DeviceCodes",
            table => new
            {
                UserCode = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                DeviceCode = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                SubjectId = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                SessionId = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                ClientId = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                Expiration = table.Column<DateTime>("datetime2", nullable: false),
                Data = table.Column<string>("nvarchar(max)", maxLength: 50000, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_DeviceCodes", x => x.UserCode); });

        migrationBuilder.CreateTable(
            "PersistedGrants",
            table => new
            {
                Key = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                Type = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                SubjectId = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                SessionId = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: true),
                ClientId = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>("nvarchar(200)", maxLength: 200, nullable: true),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                Expiration = table.Column<DateTime>("datetime2", nullable: true),
                ConsumedTime = table.Column<DateTime>("datetime2", nullable: true),
                Data = table.Column<string>("nvarchar(max)", maxLength: 50000, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_PersistedGrants", x => x.Key); });

        migrationBuilder.CreateIndex(
            "IX_DeviceCodes_DeviceCode",
            "DeviceCodes",
            "DeviceCode",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_DeviceCodes_Expiration",
            "DeviceCodes",
            "Expiration");

        migrationBuilder.CreateIndex(
            "IX_PersistedGrants_Expiration",
            "PersistedGrants",
            "Expiration");

        migrationBuilder.CreateIndex(
            "IX_PersistedGrants_SubjectId_ClientId_Type",
            "PersistedGrants",
            new[] { "SubjectId", "ClientId", "Type" });

        migrationBuilder.CreateIndex(
            "IX_PersistedGrants_SubjectId_SessionId_Type",
            "PersistedGrants",
            new[] { "SubjectId", "SessionId", "Type" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "DeviceCodes");

        migrationBuilder.DropTable(
            "PersistedGrants");
    }
}