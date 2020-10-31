using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class temp_learnable_moves_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BigMushrooms",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeartScales",
                table: "LearnableMoveApis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BigMushrooms",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "HeartScales",
                table: "LearnableMoveApis");
        }
    }
}
