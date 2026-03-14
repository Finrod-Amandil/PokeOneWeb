using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class AddImportLog_RemoveSheetHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SheetHash",
                table: "ImportSheet");

            migrationBuilder.CreateTable(
                name: "ImportLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedSheets = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportLog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImportLog_ImportTime",
                table: "ImportLog",
                column: "ImportTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportLog");

            migrationBuilder.AddColumn<string>(
                name: "SheetHash",
                table: "ImportSheet",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
