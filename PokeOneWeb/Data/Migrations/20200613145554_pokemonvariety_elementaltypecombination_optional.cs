using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class pokemonvariety_elementaltypecombination_optional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_ElementalTypeCombination_ElementalTypeCombinationId",
                table: "PokemonVariety");

            migrationBuilder.AlterColumn<int>(
                name: "ElementalTypeCombinationId",
                table: "PokemonVariety",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_ElementalTypeCombination_ElementalTypeCombinationId",
                table: "PokemonVariety",
                column: "ElementalTypeCombinationId",
                principalTable: "ElementalTypeCombination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_ElementalTypeCombination_ElementalTypeCombinationId",
                table: "PokemonVariety");

            migrationBuilder.AlterColumn<int>(
                name: "ElementalTypeCombinationId",
                table: "PokemonVariety",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_ElementalTypeCombination_ElementalTypeCombinationId",
                table: "PokemonVariety",
                column: "ElementalTypeCombinationId",
                principalTable: "ElementalTypeCombination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
