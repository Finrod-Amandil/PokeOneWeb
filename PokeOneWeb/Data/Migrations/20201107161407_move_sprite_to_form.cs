using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class move_sprite_to_form : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sprite",
                table: "PokemonVariety");

            migrationBuilder.AddColumn<string>(
                name: "SmallSprite",
                table: "PokemonForm",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmallSprite",
                table: "PokemonForm");

            migrationBuilder.AddColumn<string>(
                name: "Sprite",
                table: "PokemonVariety",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
