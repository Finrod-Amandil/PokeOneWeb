using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class PokeApiUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "PokemonVariety",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "PokemonSpecies",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonForm",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "PokemonForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effect",
                table: "Move",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "Move",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Move",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "Item",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PokeApiId",
                table: "EvolutionChain",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "ElementalType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PokeApiName",
                table: "Ability",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Effect",
                table: "Move");

            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "Move");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Move");

            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "PokeApiId",
                table: "EvolutionChain");

            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "ElementalType");

            migrationBuilder.DropColumn(
                name: "PokeApiName",
                table: "Ability");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "PokemonForm",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
