using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ReadModelDbMigrations
{
    public partial class AddLocationGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationGroupReadModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationGroupReadModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationGroupReadModels");
        }
    }
}
