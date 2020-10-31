using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class temp_learnablemoves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LearnableMoveApis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonVarietyName = table.Column<string>(nullable: true),
                    MoveName = table.Column<string>(nullable: true),
                    Generations = table.Column<string>(nullable: true),
                    Gen7LearnMethod = table.Column<string>(nullable: true),
                    LevelLearnedAt = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnableMoveApis", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearnableMoveApis");
        }
    }
}
