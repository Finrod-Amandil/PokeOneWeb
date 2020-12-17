using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class pokemon_form_remove_sprite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmallSprite",
                table: "PokemonForm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SmallSprite",
                table: "PokemonForm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
