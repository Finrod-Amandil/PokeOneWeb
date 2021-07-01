using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class evolution_sortindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaseSortIndex",
                table: "EvolutionReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EvolvedSortIndex",
                table: "EvolutionReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseSortIndex",
                table: "EvolutionReadModel");

            migrationBuilder.DropColumn(
                name: "EvolvedSortIndex",
                table: "EvolutionReadModel");
        }
    }
}
