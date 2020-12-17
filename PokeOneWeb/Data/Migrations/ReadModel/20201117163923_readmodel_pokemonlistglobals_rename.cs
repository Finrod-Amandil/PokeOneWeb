using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class readmodel_pokemonlistglobals_rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxHP",
                table: "PokemonListGlobals",
                newName: "MaxHp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxHp",
                table: "PokemonListGlobals",
                newName: "MaxHP");
        }
    }
}
