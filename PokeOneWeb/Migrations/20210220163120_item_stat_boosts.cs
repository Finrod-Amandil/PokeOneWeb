using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class item_stat_boosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemStatBoostReadModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtkBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DefBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SpaBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SpdBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SpeBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    HpBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    HasRequiredPokemon = table.Column<bool>(type: "bit", nullable: false),
                    RequiredPokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredPokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatBoostReadModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemStatBoostReadModels");
        }
    }
}
