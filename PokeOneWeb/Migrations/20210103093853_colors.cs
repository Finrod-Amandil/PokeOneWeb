using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class colors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegionColor",
                table: "SpawnReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpawnTypeColor",
                table: "SpawnReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpawnTypeSortIndex",
                table: "SpawnReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionColor",
                table: "SpawnReadModel");

            migrationBuilder.DropColumn(
                name: "SpawnTypeColor",
                table: "SpawnReadModel");

            migrationBuilder.DropColumn(
                name: "SpawnTypeSortIndex",
                table: "SpawnReadModel");
        }
    }
}
