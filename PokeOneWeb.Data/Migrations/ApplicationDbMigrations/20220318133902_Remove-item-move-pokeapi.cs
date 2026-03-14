using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class Removeitemmovepokeapi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "Move");

            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "ElementalType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "Move",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "ElementalType",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}