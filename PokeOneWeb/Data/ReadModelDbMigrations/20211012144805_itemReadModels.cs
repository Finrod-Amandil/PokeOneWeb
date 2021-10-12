using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.ReadModelDbMigrations
{
    public partial class itemReadModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BagCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BagCategorySortIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlacedItemReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    ItemResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationSortIndex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    PlacementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Screenshot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedItemReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacedItemReadModel_ItemReadModel_ItemReadModelId",
                        column: x => x.ItemReadModelId,
                        principalTable: "ItemReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemReadModel_ApplicationDbId",
                table: "ItemReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemReadModel_ResourceName",
                table: "ItemReadModel",
                column: "ResourceName",
                unique: true,
                filter: "[ResourceName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItemReadModel_ApplicationDbId",
                table: "PlacedItemReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItemReadModel_ItemReadModelId",
                table: "PlacedItemReadModel",
                column: "ItemReadModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlacedItemReadModel");

            migrationBuilder.DropTable(
                name: "ItemReadModel");
        }
    }
}
