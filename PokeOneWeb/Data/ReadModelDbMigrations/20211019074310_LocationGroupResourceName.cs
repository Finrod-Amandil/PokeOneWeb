using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.ReadModelDbMigrations
{
    public partial class LocationGroupResourceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LocationSortIndex",
                table: "PlacedItemReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationGroupResourceName",
                table: "PlacedItemReadModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationGroupResourceName",
                table: "PlacedItemReadModel");

            migrationBuilder.AlterColumn<string>(
                name: "LocationSortIndex",
                table: "PlacedItemReadModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
