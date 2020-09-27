using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesLibrary.Migrations
{
    public partial class FullDB12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameDevelopers_Studio_studioId",
                table: "GameDevelopers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameDevelopers",
                table: "GameDevelopers");

            migrationBuilder.RenameColumn(
                name: "studioId",
                table: "GameDevelopers",
                newName: "StudioId");

            migrationBuilder.RenameIndex(
                name: "IX_GameDevelopers_studioId",
                table: "GameDevelopers",
                newName: "IX_GameDevelopers_StudioId");

            migrationBuilder.AlterColumn<int>(
                name: "StudioId",
                table: "GameDevelopers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameDevelopers",
                table: "GameDevelopers",
                columns: new[] { "DeveloperId", "GameId", "StudioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameDevelopers_Studio_StudioId",
                table: "GameDevelopers",
                column: "StudioId",
                principalTable: "Studio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameDevelopers_Studio_StudioId",
                table: "GameDevelopers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameDevelopers",
                table: "GameDevelopers");

            migrationBuilder.RenameColumn(
                name: "StudioId",
                table: "GameDevelopers",
                newName: "studioId");

            migrationBuilder.RenameIndex(
                name: "IX_GameDevelopers_StudioId",
                table: "GameDevelopers",
                newName: "IX_GameDevelopers_studioId");

            migrationBuilder.AlterColumn<int>(
                name: "studioId",
                table: "GameDevelopers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameDevelopers",
                table: "GameDevelopers",
                columns: new[] { "DeveloperId", "GameId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameDevelopers_Studio_studioId",
                table: "GameDevelopers",
                column: "studioId",
                principalTable: "Studio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
