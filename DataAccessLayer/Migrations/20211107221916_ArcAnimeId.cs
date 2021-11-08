using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeLib.Domain.Migrations
{
    public partial class ArcAnimeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arcs_Animes_AnimeId",
                table: "Arcs");

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Arcs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Arcs_Animes_AnimeId",
                table: "Arcs",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arcs_Animes_AnimeId",
                table: "Arcs");

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Arcs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Arcs_Animes_AnimeId",
                table: "Arcs",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
