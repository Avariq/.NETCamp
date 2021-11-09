using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimeLib.Domain.Migrations
{
    public partial class AnimeExplicitExternalIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_AgeRestrictions_AgeRestrictionId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Animes_Statuses_StatusId",
                table: "Animes");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Animes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgeRestrictionId",
                table: "Animes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_AgeRestrictions_AgeRestrictionId",
                table: "Animes",
                column: "AgeRestrictionId",
                principalTable: "AgeRestrictions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_Statuses_StatusId",
                table: "Animes",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animes_AgeRestrictions_AgeRestrictionId",
                table: "Animes");

            migrationBuilder.DropForeignKey(
                name: "FK_Animes_Statuses_StatusId",
                table: "Animes");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Animes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AgeRestrictionId",
                table: "Animes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_AgeRestrictions_AgeRestrictionId",
                table: "Animes",
                column: "AgeRestrictionId",
                principalTable: "AgeRestrictions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animes_Statuses_StatusId",
                table: "Animes",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
