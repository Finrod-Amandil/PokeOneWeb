using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class pokemonform_availabilityid_optional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonForm_PokemonAvailability_AvailabilityId",
                table: "PokemonForm");

            migrationBuilder.AlterColumn<int>(
                name: "AvailabilityId",
                table: "PokemonForm",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonForm_PokemonAvailability_AvailabilityId",
                table: "PokemonForm",
                column: "AvailabilityId",
                principalTable: "PokemonAvailability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonForm_PokemonAvailability_AvailabilityId",
                table: "PokemonForm");

            migrationBuilder.AlterColumn<int>(
                name: "AvailabilityId",
                table: "PokemonForm",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonForm_PokemonAvailability_AvailabilityId",
                table: "PokemonForm",
                column: "AvailabilityId",
                principalTable: "PokemonAvailability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
