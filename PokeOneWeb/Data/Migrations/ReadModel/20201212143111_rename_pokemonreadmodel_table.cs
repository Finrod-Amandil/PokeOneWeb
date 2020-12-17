using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class rename_pokemonreadmodel_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonListEntry",
                table: "PokemonListEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoveReadModels",
                table: "MoveReadModels");

            migrationBuilder.RenameTable(
                name: "PokemonListEntry",
                newName: "PokemonReadModel");

            migrationBuilder.RenameTable(
                name: "MoveReadModels",
                newName: "MoveReadModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonReadModel",
                table: "PokemonReadModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoveReadModel",
                table: "MoveReadModel",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonReadModel",
                table: "PokemonReadModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoveReadModel",
                table: "MoveReadModel");

            migrationBuilder.RenameTable(
                name: "PokemonReadModel",
                newName: "PokemonListEntry");

            migrationBuilder.RenameTable(
                name: "MoveReadModel",
                newName: "MoveReadModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonListEntry",
                table: "PokemonListEntry",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoveReadModels",
                table: "MoveReadModels",
                column: "Id");
        }
    }
}
