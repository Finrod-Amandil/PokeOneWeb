using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class remove_ev_from_readmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtkEv",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "DefEv",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "HpEv",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "SpaEv",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "SpdEv",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "SpeEv",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "TotalEv",
                table: "PokemonListReadModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtkEv",
                table: "PokemonListReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefEv",
                table: "PokemonListReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HpEv",
                table: "PokemonListReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpaEv",
                table: "PokemonListReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpdEv",
                table: "PokemonListReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpeEv",
                table: "PokemonListReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalEv",
                table: "PokemonListReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
