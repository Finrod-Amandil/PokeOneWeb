using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class learnable_move_improvements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "LearnableMove");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "LearnableMoveLearnMethod",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LevelLearnedAt",
                table: "LearnableMoveLearnMethod",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Currency",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "LearnableMoveLearnMethod");

            migrationBuilder.DropColumn(
                name: "LevelLearnedAt",
                table: "LearnableMoveLearnMethod");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "LearnableMove",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Currency",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
