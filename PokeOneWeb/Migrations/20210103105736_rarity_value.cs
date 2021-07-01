using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class rarity_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rarity",
                table: "SpawnReadModel",
                newName: "RarityString");

            migrationBuilder.AddColumn<double>(
                name: "RarityValue",
                table: "SpawnReadModel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RarityValue",
                table: "SpawnReadModel");

            migrationBuilder.RenameColumn(
                name: "RarityString",
                table: "SpawnReadModel",
                newName: "Rarity");
        }
    }
}
