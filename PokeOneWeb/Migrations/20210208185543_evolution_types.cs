using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class evolution_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaseType1",
                table: "EvolutionReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BaseType2",
                table: "EvolutionReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvolvedType1",
                table: "EvolutionReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvolvedType2",
                table: "EvolutionReadModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseType1",
                table: "EvolutionReadModel");

            migrationBuilder.DropColumn(
                name: "BaseType2",
                table: "EvolutionReadModel");

            migrationBuilder.DropColumn(
                name: "EvolvedType1",
                table: "EvolutionReadModel");

            migrationBuilder.DropColumn(
                name: "EvolvedType2",
                table: "EvolutionReadModel");
        }
    }
}
