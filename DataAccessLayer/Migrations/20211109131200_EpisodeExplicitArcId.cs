using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeLib.Domain.Migrations
{
    public partial class EpisodeExplicitArcId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Arcs_ArcId",
                table: "Episodes");

            migrationBuilder.AlterColumn<int>(
                name: "ArcId",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Arcs_ArcId",
                table: "Episodes",
                column: "ArcId",
                principalTable: "Arcs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Arcs_ArcId",
                table: "Episodes");

            migrationBuilder.AlterColumn<int>(
                name: "ArcId",
                table: "Episodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Arcs_ArcId",
                table: "Episodes",
                column: "ArcId",
                principalTable: "Arcs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
