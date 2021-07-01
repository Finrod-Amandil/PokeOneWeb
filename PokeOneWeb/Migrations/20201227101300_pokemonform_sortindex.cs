using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class pokemonform_sortindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortIndex",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortIndex",
                table: "PokemonReadModel");
        }
    }
}
