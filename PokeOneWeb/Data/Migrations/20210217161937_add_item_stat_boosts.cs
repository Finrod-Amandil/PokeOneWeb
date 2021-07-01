using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class add_item_stat_boosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemStatBoost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    StatBoostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatBoost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemStatBoost_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemStatBoost_Stats_StatBoostId",
                        column: x => x.StatBoostId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemStatBoostPokemon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemStatBoostId = table.Column<int>(type: "int", nullable: false),
                    PokemonVarietyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatBoostPokemon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemStatBoostPokemon_ItemStatBoost_ItemStatBoostId",
                        column: x => x.ItemStatBoostId,
                        principalTable: "ItemStatBoost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemStatBoostPokemon_PokemonVariety_PokemonVarietyId",
                        column: x => x.PokemonVarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoost_ItemId",
                table: "ItemStatBoost",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoost_StatBoostId",
                table: "ItemStatBoost",
                column: "StatBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoostPokemon_ItemStatBoostId",
                table: "ItemStatBoostPokemon",
                column: "ItemStatBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoostPokemon_PokemonVarietyId",
                table: "ItemStatBoostPokemon",
                column: "PokemonVarietyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemStatBoostPokemon");

            migrationBuilder.DropTable(
                name: "ItemStatBoost");
        }
    }
}
