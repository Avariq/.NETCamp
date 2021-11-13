using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeLib.Domain.Migrations
{
    public partial class ExplicitManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Animes_AnimesId",
                table: "AnimeGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Genres_AnimeGenresId",
                table: "AnimeGenre");

            migrationBuilder.RenameColumn(
                name: "AnimesId",
                table: "AnimeGenre",
                newName: "AnimeId");

            migrationBuilder.RenameColumn(
                name: "AnimeGenresId",
                table: "AnimeGenre",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeGenre_AnimesId",
                table: "AnimeGenre",
                newName: "IX_AnimeGenre_AnimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Animes_AnimeId",
                table: "AnimeGenre",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Genres_GenreId",
                table: "AnimeGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Animes_AnimeId",
                table: "AnimeGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeGenre_Genres_GenreId",
                table: "AnimeGenre");

            migrationBuilder.RenameColumn(
                name: "AnimeId",
                table: "AnimeGenre",
                newName: "AnimesId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "AnimeGenre",
                newName: "AnimeGenresId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimeGenre_AnimeId",
                table: "AnimeGenre",
                newName: "IX_AnimeGenre_AnimesId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Animes_AnimesId",
                table: "AnimeGenre",
                column: "AnimesId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeGenre_Genres_AnimeGenresId",
                table: "AnimeGenre",
                column: "AnimeGenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
