using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class RemovePokemonPokeApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "PokemonSpecies");

            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "PokemonForm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "PokemonVariety",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "PokemonSpecies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "PokemonForm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
