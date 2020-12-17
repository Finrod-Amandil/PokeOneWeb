using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class serebii_pokemondb_url : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PokemonDbUrl",
                table: "PokemonVariety",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerebiiUrl",
                table: "PokemonVariety",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PokemonDbUrl",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "SerebiiUrl",
                table: "PokemonVariety");
        }
    }
}
