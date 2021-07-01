using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class seasontime_color : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "TimeReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "SeasonReadModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "TimeReadModel");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "SeasonReadModel");
        }
    }
}
