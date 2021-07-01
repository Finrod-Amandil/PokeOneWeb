using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class pokemonreadmodel_locationsortindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationSortIndex",
                table: "SpawnReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PokemonFormSortIndex",
                table: "SpawnReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "LearnMethodReadModel",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationSortIndex",
                table: "SpawnReadModel");

            migrationBuilder.DropColumn(
                name: "PokemonFormSortIndex",
                table: "SpawnReadModel");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "LearnMethodReadModel");
        }
    }
}
