using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeLib.Domain.Migrations
{
    public partial class UniqueIndexesForAnimeDependendantEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Statuses_StatusName",
                table: "Statuses",
                column: "StatusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animes_Title",
                table: "Animes",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgeRestrictions_RestrictionCode",
                table: "AgeRestrictions",
                column: "RestrictionCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statuses_StatusName",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Animes_Title",
                table: "Animes");

            migrationBuilder.DropIndex(
                name: "IX_AgeRestrictions_RestrictionCode",
                table: "AgeRestrictions");
        }
    }
}
