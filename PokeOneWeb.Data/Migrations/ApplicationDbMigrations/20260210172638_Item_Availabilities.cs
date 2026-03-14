using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class Item_Availabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "AvailabilityId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ItemAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAvailability_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_AvailabilityId",
                table: "Item",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAvailability_Hash",
                table: "ItemAvailability",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAvailability_IdHash",
                table: "ItemAvailability",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAvailability_ImportSheetId",
                table: "ItemAvailability",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAvailability_Name",
                table: "ItemAvailability",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemAvailability_AvailabilityId",
                table: "Item",
                column: "AvailabilityId",
                principalTable: "ItemAvailability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemAvailability_AvailabilityId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "ItemAvailability");

            migrationBuilder.DropIndex(
                name: "IX_Item_AvailabilityId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "AvailabilityId",
                table: "Item");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Item",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
