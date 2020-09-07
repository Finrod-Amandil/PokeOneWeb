using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class fix_evolutionchain_typo_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvolutionChainId",
                table: "PokemonVariety");

            migrationBuilder.AlterColumn<int>(
                name: "EvlutionChainId",
                table: "PokemonVariety",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EvlutionChainId",
                table: "PokemonVariety",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EvolutionChainId",
                table: "PokemonVariety",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
