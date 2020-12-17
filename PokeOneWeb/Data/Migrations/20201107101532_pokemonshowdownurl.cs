using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class pokemonshowdownurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PokemonShowDownUrl",
                table: "PokemonVariety",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PokemonShowDownUrl",
                table: "PokemonVariety");
        }
    }
}
