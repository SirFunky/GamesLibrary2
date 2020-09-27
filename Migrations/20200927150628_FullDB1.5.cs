using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesLibrary.Migrations
{
    public partial class FullDB15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameDevelopers_Studio_StudioId",
                table: "GameDevelopers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Studio",
                table: "Studio");

            migrationBuilder.RenameTable(
                name: "Studio",
                newName: "Studios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Studios",
                table: "Studios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameDevelopers_Studios_StudioId",
                table: "GameDevelopers",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameDevelopers_Studios_StudioId",
                table: "GameDevelopers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Studios",
                table: "Studios");

            migrationBuilder.RenameTable(
                name: "Studios",
                newName: "Studio");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Studio",
                table: "Studio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameDevelopers_Studio_StudioId",
                table: "GameDevelopers",
                column: "StudioId",
                principalTable: "Studio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
