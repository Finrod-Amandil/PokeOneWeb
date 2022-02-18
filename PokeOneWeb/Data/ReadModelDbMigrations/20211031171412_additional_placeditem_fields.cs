using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.ReadModelDbMigrations
{
    public partial class additional_placeditem_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationGroupResourceName",
                table: "PlacedItemReadModel",
                newName: "RegionColor");

            migrationBuilder.AddColumn<string>(
                name: "ItemSpriteName",
                table: "PlacedItemReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationResourceName",
                table: "PlacedItemReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "PlacedItemReadModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemSpriteName",
                table: "PlacedItemReadModel");

            migrationBuilder.DropColumn(
                name: "LocationResourceName",
                table: "PlacedItemReadModel");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "PlacedItemReadModel");

            migrationBuilder.RenameColumn(
                name: "RegionColor",
                table: "PlacedItemReadModel",
                newName: "LocationGroupResourceName");
        }
    }
}
