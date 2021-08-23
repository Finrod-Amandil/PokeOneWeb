using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.ApplicationDbMigrations
{
    public partial class AbilityBoosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AttackBoost",
                table: "Ability",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BoostConditions",
                table: "Ability",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DefenseBoost",
                table: "Ability",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HitPointsBoost",
                table: "Ability",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SpecialAttackBoost",
                table: "Ability",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SpecialDefenseBoost",
                table: "Ability",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SpeedBoost",
                table: "Ability",
                type: "decimal(4,1)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttackBoost",
                table: "Ability");

            migrationBuilder.DropColumn(
                name: "BoostConditions",
                table: "Ability");

            migrationBuilder.DropColumn(
                name: "DefenseBoost",
                table: "Ability");

            migrationBuilder.DropColumn(
                name: "HitPointsBoost",
                table: "Ability");

            migrationBuilder.DropColumn(
                name: "SpecialAttackBoost",
                table: "Ability");

            migrationBuilder.DropColumn(
                name: "SpecialDefenseBoost",
                table: "Ability");

            migrationBuilder.DropColumn(
                name: "SpeedBoost",
                table: "Ability");
        }
    }
}
