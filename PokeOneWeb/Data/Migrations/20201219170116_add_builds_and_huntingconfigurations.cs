using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class add_builds_and_huntingconfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_Location_LocationId",
                table: "Shop");

            migrationBuilder.CreateTable(
                name: "Build",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PokemonVarietyId = table.Column<int>(nullable: true),
                    AbilityId = table.Column<int>(nullable: false),
                    EvDistributionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Build", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Build_Ability_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Build_Stats_EvDistributionId",
                        column: x => x.EvDistributionId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Build_PokemonVariety_PokemonVarietyId",
                        column: x => x.PokemonVarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    StatBoostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nature_Stats_StatBoostId",
                        column: x => x.StatBoostId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    BuildId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOption_Build_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Build",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemOption_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveId = table.Column<int>(nullable: false),
                    BuildId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveOption_Build_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Build",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveOption_Move_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Move",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HuntingConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonVarietyId = table.Column<int>(nullable: false),
                    NatureId = table.Column<int>(nullable: false),
                    AbilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntingConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HuntingConfiguration_Ability_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HuntingConfiguration_Nature_NatureId",
                        column: x => x.NatureId,
                        principalTable: "Nature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HuntingConfiguration_PokemonVariety_PokemonVarietyId",
                        column: x => x.PokemonVarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NatureOption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NatureId = table.Column<int>(nullable: false),
                    BuildId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NatureOption_Build_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Build",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NatureOption_Nature_NatureId",
                        column: x => x.NatureId,
                        principalTable: "Nature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Build_AbilityId",
                table: "Build",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_EvDistributionId",
                table: "Build",
                column: "EvDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_PokemonVarietyId",
                table: "Build",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfiguration_AbilityId",
                table: "HuntingConfiguration",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfiguration_NatureId",
                table: "HuntingConfiguration",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfiguration_PokemonVarietyId",
                table: "HuntingConfiguration",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOption_BuildId",
                table: "ItemOption",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOption_ItemId",
                table: "ItemOption",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveOption_BuildId",
                table: "MoveOption",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveOption_MoveId",
                table: "MoveOption",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_Nature_StatBoostId",
                table: "Nature",
                column: "StatBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_NatureOption_BuildId",
                table: "NatureOption",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_NatureOption_NatureId",
                table: "NatureOption",
                column: "NatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_Location_LocationId",
                table: "Shop",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_Location_LocationId",
                table: "Shop");

            migrationBuilder.DropTable(
                name: "HuntingConfiguration");

            migrationBuilder.DropTable(
                name: "ItemOption");

            migrationBuilder.DropTable(
                name: "MoveOption");

            migrationBuilder.DropTable(
                name: "NatureOption");

            migrationBuilder.DropTable(
                name: "Build");

            migrationBuilder.DropTable(
                name: "Nature");

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_Location_LocationId",
                table: "Shop",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
