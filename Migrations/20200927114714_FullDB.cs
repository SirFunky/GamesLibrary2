using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesLibrary.Migrations
{
    public partial class FullDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicherId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "StudioId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "StudioId",
                table: "Developers");

            migrationBuilder.AddColumn<int>(
                name: "studioId",
                table: "GameDevelopers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studio", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameDevelopers_studioId",
                table: "GameDevelopers",
                column: "studioId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameDevelopers_Studio_studioId",
                table: "GameDevelopers",
                column: "studioId",
                principalTable: "Studio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameDevelopers_Studio_studioId",
                table: "GameDevelopers");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Studio");

            migrationBuilder.DropIndex(
                name: "IX_GameDevelopers_studioId",
                table: "GameDevelopers");

            migrationBuilder.DropColumn(
                name: "studioId",
                table: "GameDevelopers");

            migrationBuilder.AddColumn<int>(
                name: "PublicherId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudioId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Developers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudioId",
                table: "Developers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
