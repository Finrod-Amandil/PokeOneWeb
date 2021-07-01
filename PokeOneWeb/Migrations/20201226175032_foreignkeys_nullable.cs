using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class foreignkeys_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsUnavailableMoveId",
                table: "MoveReadModel",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsAvailableMoveId",
                table: "MoveReadModel",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsSecondaryAbilityId",
                table: "AbilityTurnsIntoReadModel",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsPrimaryAbilityId",
                table: "AbilityTurnsIntoReadModel",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsHiddenAbilityId",
                table: "AbilityTurnsIntoReadModel",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsUnavailableMoveId",
                table: "MoveReadModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsAvailableMoveId",
                table: "MoveReadModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsSecondaryAbilityId",
                table: "AbilityTurnsIntoReadModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsPrimaryAbilityId",
                table: "AbilityTurnsIntoReadModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyAsHiddenAbilityId",
                table: "AbilityTurnsIntoReadModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
