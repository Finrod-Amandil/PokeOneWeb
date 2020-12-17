using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class pokemon_form_sprite_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpriteName",
                table: "PokemonForm",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpriteName",
                table: "PokemonForm");
        }
    }
}
