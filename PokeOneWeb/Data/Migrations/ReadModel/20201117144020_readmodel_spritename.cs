using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class readmodel_spritename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sprite",
                table: "PokemonListReadModel");

            migrationBuilder.AddColumn<string>(
                name: "SpriteName",
                table: "PokemonListReadModel",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpriteName",
                table: "PokemonListReadModel");

            migrationBuilder.AddColumn<string>(
                name: "Sprite",
                table: "PokemonListReadModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
