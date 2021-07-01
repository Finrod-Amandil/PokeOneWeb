using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class refinements_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "SeasonTimesOfDay");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "SeasonTimesOfDay");

            migrationBuilder.AddColumn<int>(
                name: "EndHour",
                table: "SeasonTimesOfDay",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "SeasonTimesOfDay",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "SeasonTimesOfDay");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "SeasonTimesOfDay");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "SeasonTimesOfDay",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "SeasonTimesOfDay",
                type: "datetime2",
                nullable: true);
        }
    }
}
