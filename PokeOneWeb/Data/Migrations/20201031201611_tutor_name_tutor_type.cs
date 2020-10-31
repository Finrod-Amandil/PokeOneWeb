using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class tutor_name_tutor_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TutorType",
                table: "TutorMoves",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NpcName",
                table: "MoveLearnMethodLocation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TutorType",
                table: "TutorMoves");

            migrationBuilder.DropColumn(
                name: "NpcName",
                table: "MoveLearnMethodLocation");
        }
    }
}
