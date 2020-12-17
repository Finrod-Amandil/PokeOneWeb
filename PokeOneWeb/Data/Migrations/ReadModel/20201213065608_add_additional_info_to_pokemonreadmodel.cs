using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class add_additional_info_to_pokemonreadmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Generation",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullyEvolved",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMega",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Generation",
                table: "PokemonReadModel");

            migrationBuilder.DropColumn(
                name: "IsFullyEvolved",
                table: "PokemonReadModel");

            migrationBuilder.DropColumn(
                name: "IsMega",
                table: "PokemonReadModel");
        }
    }
}
