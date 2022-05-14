using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class ImportRefactoring_ChangesForDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimesOfDay_ImportSheet_ImportSheetId",
                table: "SeasonTimesOfDay");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimesOfDay_Season_SeasonId",
                table: "SeasonTimesOfDay");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimesOfDay_TimeOfDay_TimeOfDayId",
                table: "SeasonTimesOfDay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeasonTimesOfDay",
                table: "SeasonTimesOfDay");

            migrationBuilder.RenameTable(
                name: "SeasonTimesOfDay",
                newName: "SeasonTimeOfDay");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimesOfDay_TimeOfDayId",
                table: "SeasonTimeOfDay",
                newName: "IX_SeasonTimeOfDay_TimeOfDayId");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimesOfDay_SeasonId",
                table: "SeasonTimeOfDay",
                newName: "IX_SeasonTimeOfDay_SeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimesOfDay_ImportSheetId",
                table: "SeasonTimeOfDay",
                newName: "IX_SeasonTimeOfDay_ImportSheetId");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimesOfDay_IdHash",
                table: "SeasonTimeOfDay",
                newName: "IX_SeasonTimeOfDay_IdHash");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimesOfDay_Hash",
                table: "SeasonTimeOfDay",
                newName: "IX_SeasonTimeOfDay_Hash");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeasonTimeOfDay",
                table: "SeasonTimeOfDay",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimeOfDay_ImportSheet_ImportSheetId",
                table: "SeasonTimeOfDay",
                column: "ImportSheetId",
                principalTable: "ImportSheet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimeOfDay_Season_SeasonId",
                table: "SeasonTimeOfDay",
                column: "SeasonId",
                principalTable: "Season",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimeOfDay_TimeOfDay_TimeOfDayId",
                table: "SeasonTimeOfDay",
                column: "TimeOfDayId",
                principalTable: "TimeOfDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimeOfDay_ImportSheet_ImportSheetId",
                table: "SeasonTimeOfDay");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimeOfDay_Season_SeasonId",
                table: "SeasonTimeOfDay");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimeOfDay_TimeOfDay_TimeOfDayId",
                table: "SeasonTimeOfDay");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeasonTimeOfDay",
                table: "SeasonTimeOfDay");

            migrationBuilder.RenameTable(
                name: "SeasonTimeOfDay",
                newName: "SeasonTimesOfDay");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimeOfDay_TimeOfDayId",
                table: "SeasonTimesOfDay",
                newName: "IX_SeasonTimesOfDay_TimeOfDayId");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimeOfDay_SeasonId",
                table: "SeasonTimesOfDay",
                newName: "IX_SeasonTimesOfDay_SeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimeOfDay_ImportSheetId",
                table: "SeasonTimesOfDay",
                newName: "IX_SeasonTimesOfDay_ImportSheetId");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimeOfDay_IdHash",
                table: "SeasonTimesOfDay",
                newName: "IX_SeasonTimesOfDay_IdHash");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimeOfDay_Hash",
                table: "SeasonTimesOfDay",
                newName: "IX_SeasonTimesOfDay_Hash");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeasonTimesOfDay",
                table: "SeasonTimesOfDay",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimesOfDay_ImportSheet_ImportSheetId",
                table: "SeasonTimesOfDay",
                column: "ImportSheetId",
                principalTable: "ImportSheet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimesOfDay_Season_SeasonId",
                table: "SeasonTimesOfDay",
                column: "SeasonId",
                principalTable: "Season",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimesOfDay_TimeOfDay_TimeOfDayId",
                table: "SeasonTimesOfDay",
                column: "TimeOfDayId",
                principalTable: "TimeOfDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
