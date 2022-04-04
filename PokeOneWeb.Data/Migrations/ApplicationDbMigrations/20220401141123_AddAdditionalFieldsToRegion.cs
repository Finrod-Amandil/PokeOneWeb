using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class AddAdditionalFieldsToRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Region",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainRegion",
                table: "Region",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReleased",
                table: "Region",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSideRegion",
                table: "Region",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "IsMainRegion",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "IsReleased",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "IsSideRegion",
                table: "Region");
        }
    }
}
