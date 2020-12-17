using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class extend_readmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvailabilityDescription",
                table: "PokemonListReadModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HiddenAbilityEffect",
                table: "PokemonListReadModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryAbilityEffect",
                table: "PokemonListReadModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PvpTierSortIndex",
                table: "PokemonListReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryAbilityEffect",
                table: "PokemonListReadModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailabilityDescription",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "HiddenAbilityEffect",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "PrimaryAbilityEffect",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "PvpTierSortIndex",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "SecondaryAbilityEffect",
                table: "PokemonListReadModel");
        }
    }
}
