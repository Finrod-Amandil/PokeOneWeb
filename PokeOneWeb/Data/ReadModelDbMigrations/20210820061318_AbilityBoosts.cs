using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.ReadModelDbMigrations
{
    public partial class AbilityBoosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "HiddenAbilityAttackBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "HiddenAbilityBoostConditions",
                table: "PokemonVarietyReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HiddenAbilityDefenseBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HiddenAbilityHitPointsBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HiddenAbilitySpecialAttackBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HiddenAbilitySpecialDefenseBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HiddenAbilitySpeedBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrimaryAbilityAttackBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryAbilityBoostConditions",
                table: "PokemonVarietyReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrimaryAbilityDefenseBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrimaryAbilityHitPointsBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrimaryAbilitySpecialAttackBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrimaryAbilitySpecialDefenseBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrimaryAbilitySpeedBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecondaryAbilityAttackBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryAbilityBoostConditions",
                table: "PokemonVarietyReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SecondaryAbilityDefenseBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecondaryAbilityHitPointsBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecondaryAbilitySpecialAttackBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecondaryAbilitySpecialDefenseBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SecondaryAbilitySpeedBoost",
                table: "PokemonVarietyReadModel",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HiddenAbilityAttackBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "HiddenAbilityBoostConditions",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "HiddenAbilityDefenseBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "HiddenAbilityHitPointsBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "HiddenAbilitySpecialAttackBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "HiddenAbilitySpecialDefenseBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "HiddenAbilitySpeedBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "PrimaryAbilityAttackBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "PrimaryAbilityBoostConditions",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "PrimaryAbilityDefenseBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "PrimaryAbilityHitPointsBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "PrimaryAbilitySpecialAttackBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "PrimaryAbilitySpecialDefenseBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "PrimaryAbilitySpeedBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "SecondaryAbilityAttackBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "SecondaryAbilityBoostConditions",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "SecondaryAbilityDefenseBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "SecondaryAbilityHitPointsBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "SecondaryAbilitySpecialAttackBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "SecondaryAbilitySpecialDefenseBoost",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "SecondaryAbilitySpeedBoost",
                table: "PokemonVarietyReadModel");
        }
    }
}
