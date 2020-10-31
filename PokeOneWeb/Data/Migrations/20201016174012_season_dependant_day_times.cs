using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class season_dependant_day_times : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "TimeOfDay");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "TimeOfDay");

            migrationBuilder.CreateTable(
                name: "SeasonTimeOfDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonId = table.Column<int>(nullable: true),
                    TimeOfDayId = table.Column<int>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonTimeOfDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonTimeOfDay_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeasonTimeOfDay_TimeOfDay_TimeOfDayId",
                        column: x => x.TimeOfDayId,
                        principalTable: "TimeOfDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTimeOfDay_SeasonId",
                table: "SeasonTimeOfDay",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTimeOfDay_TimeOfDayId",
                table: "SeasonTimeOfDay",
                column: "TimeOfDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeasonTimeOfDay");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "TimeOfDay",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "TimeOfDay",
                type: "datetime2",
                nullable: true);
        }
    }
}
