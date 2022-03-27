using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class addregionresourcename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "Region",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_ResourceName",
                table: "Region",
                column: "ResourceName",
                unique: true,
                filter: "[ResourceName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Region_ResourceName",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "Region");
        }
    }
}