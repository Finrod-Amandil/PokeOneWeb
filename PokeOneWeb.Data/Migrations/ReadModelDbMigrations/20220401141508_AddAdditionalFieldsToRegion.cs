using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ReadModelDbMigrations
{
    public partial class AddAdditionalFieldsToRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RegionReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainRegion",
                table: "RegionReadModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReleased",
                table: "RegionReadModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSideRegion",
                table: "RegionReadModel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RegionReadModel");

            migrationBuilder.DropColumn(
                name: "IsMainRegion",
                table: "RegionReadModel");

            migrationBuilder.DropColumn(
                name: "IsReleased",
                table: "RegionReadModel");

            migrationBuilder.DropColumn(
                name: "IsSideRegion",
                table: "RegionReadModel");
        }
    }
}
