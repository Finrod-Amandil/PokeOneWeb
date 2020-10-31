using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class temp_tutor_moves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TutorMoves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TutorName = table.Column<string>(nullable: true),
                    LocationName = table.Column<string>(nullable: true),
                    PlacementDescription = table.Column<string>(nullable: true),
                    MoveName = table.Column<string>(nullable: true),
                    RedShardPrice = table.Column<int>(nullable: true),
                    BlueShardPrice = table.Column<int>(nullable: true),
                    GreenShardPrice = table.Column<int>(nullable: true),
                    YellowShardPrice = table.Column<int>(nullable: true),
                    PWTBPPrice = table.Column<int>(nullable: true),
                    BFBPPrice = table.Column<int>(nullable: true),
                    PokeDollarPrice = table.Column<int>(nullable: true),
                    PokeGoldPrice = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorMoves", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorMoves");
        }
    }
}
