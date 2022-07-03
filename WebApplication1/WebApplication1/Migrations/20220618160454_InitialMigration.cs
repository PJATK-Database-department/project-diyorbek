using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "polygon");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "polygon",
                columns: table => new
                {
                    Ticker = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Ticker);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "polygon",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                schema: "polygon",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => new { x.Login, x.Ticker });
                    table.ForeignKey(
                        name: "FK_Wishlists_Companies_Ticker",
                        column: x => x.Ticker,
                        principalSchema: "polygon",
                        principalTable: "Companies",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_Users_Login",
                        column: x => x.Login,
                        principalSchema: "polygon",
                        principalTable: "Users",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_Ticker",
                schema: "polygon",
                table: "Wishlists",
                column: "Ticker");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wishlists",
                schema: "polygon");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "polygon");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "polygon");
        }
    }
}
