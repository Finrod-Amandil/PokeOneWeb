using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class temp_learnable_moves_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gen7LearnMethod",
                table: "LearnableMoveApis");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "LearnableMoveApis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BFBPPrice",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BlueShardPrice",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GreenShardPrice",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LearnMethod",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PWTBPPrice",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PokeDollarPrice",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PokeGoldPrice",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RedShardPrice",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredItem",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TutorLocation",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TutorName",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TutorPlacementDescription",
                table: "LearnableMoveApis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YellowShardPrice",
                table: "LearnableMoveApis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "BFBPPrice",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "BlueShardPrice",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "GreenShardPrice",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "LearnMethod",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "PWTBPPrice",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "PokeDollarPrice",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "PokeGoldPrice",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "RedShardPrice",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "RequiredItem",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "TutorLocation",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "TutorName",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "TutorPlacementDescription",
                table: "LearnableMoveApis");

            migrationBuilder.DropColumn(
                name: "YellowShardPrice",
                table: "LearnableMoveApis");

            migrationBuilder.AddColumn<string>(
                name: "Gen7LearnMethod",
                table: "LearnableMoveApis",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
