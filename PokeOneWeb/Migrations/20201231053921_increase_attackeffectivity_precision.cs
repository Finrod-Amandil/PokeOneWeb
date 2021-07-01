using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class increase_attackeffectivity_precision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Effectivity",
                table: "AttackEffectivityReadModel",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Effectivity",
                table: "AttackEffectivityReadModel",
                type: "decimal(4,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");
        }
    }
}
