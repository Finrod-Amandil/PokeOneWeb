using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.ApplicationDbMigrations
{
    public partial class pokemonvarietyadditionalinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EggCycles",
                table: "PokemonVariety",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpYield",
                table: "PokemonVariety",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasGender",
                table: "PokemonVariety",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "PokemonVariety",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MaleRatio",
                table: "PokemonVariety",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "PokemonVariety",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EggCycles",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "ExpYield",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "HasGender",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "MaleRatio",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "PokemonVariety");
        }
    }
}
