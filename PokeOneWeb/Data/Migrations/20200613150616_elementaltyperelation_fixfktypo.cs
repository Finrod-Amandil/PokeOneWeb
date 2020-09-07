using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class elementaltyperelation_fixfktypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefendingTypeIdId",
                table: "ElementalTypeRelation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefendingTypeIdId",
                table: "ElementalTypeRelation",
                type: "int",
                nullable: true);
        }
    }
}
