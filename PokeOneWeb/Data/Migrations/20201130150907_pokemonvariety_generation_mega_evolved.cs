using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class pokemonvariety_generation_mega_evolved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Generation",
                table: "PokemonVariety",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFullyEvolved",
                table: "PokemonVariety",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMega",
                table: "PokemonVariety",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Generation",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "IsFullyEvolved",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "IsMega",
                table: "PokemonVariety");
        }
    }
}
