using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class placeditem_quantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_Location_LocationId",
                table: "Shop");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PlacedItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_Location_LocationId",
                table: "Shop",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shop_Location_LocationId",
                table: "Shop");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PlacedItem");

            migrationBuilder.AddForeignKey(
                name: "FK_Shop_Location_LocationId",
                table: "Shop",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
