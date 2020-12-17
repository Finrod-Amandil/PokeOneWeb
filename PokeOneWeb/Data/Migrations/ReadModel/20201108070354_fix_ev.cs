using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class fix_ev : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvAmount",
                table: "PokemonListReadModel");

            migrationBuilder.DropColumn(
                name: "EvStat",
                table: "PokemonListReadModel");

            migrationBuilder.AddColumn<int>(
                name: "AtkEv",
                table: "PokemonListReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefEv",
                table: "PokemonListReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HpEv",
                table: "PokemonListReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpaEv",
                table: "PokemonListReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpdEv",
                table: "PokemonListReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpeEv",
                table: "PokemonListReadModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "EvAmount",
                table: "PokemonListReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EvStat",
                table: "PokemonListReadModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
