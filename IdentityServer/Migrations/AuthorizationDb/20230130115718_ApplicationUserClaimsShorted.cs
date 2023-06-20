#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Migrations.AuthorizationDb;

/// <inheritdoc />
public partial class ApplicationUserClaimsShorted : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn("CardHolderName",
                                    "AspNetUsers");

        migrationBuilder.DropColumn("CardNumber",
                                    "AspNetUsers");

        migrationBuilder.DropColumn("CardType",
                                    "AspNetUsers");

        migrationBuilder.DropColumn("Expiration",
                                    "AspNetUsers");

        migrationBuilder.DropColumn("SecurityNumber",
                                    "AspNetUsers");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>("CardHolderName",
                                           "AspNetUsers",
                                           "nvarchar(max)",
                                           nullable: false,
                                           defaultValue: "");

        migrationBuilder.AddColumn<string>("CardNumber",
                                           "AspNetUsers",
                                           "nvarchar(max)",
                                           nullable: false,
                                           defaultValue: "");

        migrationBuilder.AddColumn<int>("CardType",
                                        "AspNetUsers",
                                        "int",
                                        nullable: false,
                                        defaultValue: 0);

        migrationBuilder.AddColumn<string>("Expiration",
                                           "AspNetUsers",
                                           "nvarchar(max)",
                                           nullable: false,
                                           defaultValue: "");

        migrationBuilder.AddColumn<string>("SecurityNumber",
                                           "AspNetUsers",
                                           "nvarchar(max)",
                                           nullable: false,
                                           defaultValue: "");
    }
}