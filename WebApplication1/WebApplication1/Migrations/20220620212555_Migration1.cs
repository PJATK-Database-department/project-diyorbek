using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Companies_Ticker",
                schema: "polygon",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Users_Login",
                schema: "polygon",
                table: "Wishlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wishlists",
                schema: "polygon",
                table: "Wishlists");

            migrationBuilder.RenameTable(
                name: "Wishlists",
                schema: "polygon",
                newName: "Watchlists",
                newSchema: "polygon");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlists_Ticker",
                schema: "polygon",
                table: "Watchlists",
                newName: "IX_Watchlists_Ticker");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Watchlists",
                schema: "polygon",
                table: "Watchlists",
                columns: new[] { "Login", "Ticker" });

            migrationBuilder.AddForeignKey(
                name: "FK_Watchlists_Companies_Ticker",
                schema: "polygon",
                table: "Watchlists",
                column: "Ticker",
                principalSchema: "polygon",
                principalTable: "Companies",
                principalColumn: "Ticker",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Watchlists_Users_Login",
                schema: "polygon",
                table: "Watchlists",
                column: "Login",
                principalSchema: "polygon",
                principalTable: "Users",
                principalColumn: "Login",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Watchlists_Companies_Ticker",
                schema: "polygon",
                table: "Watchlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Watchlists_Users_Login",
                schema: "polygon",
                table: "Watchlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Watchlists",
                schema: "polygon",
                table: "Watchlists");

            migrationBuilder.RenameTable(
                name: "Watchlists",
                schema: "polygon",
                newName: "Wishlists",
                newSchema: "polygon");

            migrationBuilder.RenameIndex(
                name: "IX_Watchlists_Ticker",
                schema: "polygon",
                table: "Wishlists",
                newName: "IX_Wishlists_Ticker");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wishlists",
                schema: "polygon",
                table: "Wishlists",
                columns: new[] { "Login", "Ticker" });

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Companies_Ticker",
                schema: "polygon",
                table: "Wishlists",
                column: "Ticker",
                principalSchema: "polygon",
                principalTable: "Companies",
                principalColumn: "Ticker",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Users_Login",
                schema: "polygon",
                table: "Wishlists",
                column: "Login",
                principalSchema: "polygon",
                principalTable: "Users",
                principalColumn: "Login",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
