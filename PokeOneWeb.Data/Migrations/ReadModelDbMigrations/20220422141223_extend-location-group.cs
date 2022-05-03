using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ReadModelDbMigrations
{
    public partial class extendlocationgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationReadModelId",
                table: "SpawnReadModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationReadModelId",
                table: "PlacedItemReadModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventEndDate",
                table: "LocationGroupReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "LocationGroupReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventStartDate",
                table: "LocationGroupReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEventRegion",
                table: "LocationGroupReadModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NextLocationGroupName",
                table: "LocationGroupReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextLocationGroupResourceName",
                table: "LocationGroupReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousLocationGroupName",
                table: "LocationGroupReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousLocationGroupResourceName",
                table: "LocationGroupReadModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LocationReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    IsDiscoverable = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationGroupReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationReadModel_LocationGroupReadModel_LocationGroupReadModelId",
                        column: x => x.LocationGroupReadModelId,
                        principalTable: "LocationGroupReadModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpawnReadModel_LocationReadModelId",
                table: "SpawnReadModel",
                column: "LocationReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItemReadModel_LocationReadModelId",
                table: "PlacedItemReadModel",
                column: "LocationReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationReadModel_LocationGroupReadModelId",
                table: "LocationReadModel",
                column: "LocationGroupReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlacedItemReadModel_LocationReadModel_LocationReadModelId",
                table: "PlacedItemReadModel",
                column: "LocationReadModelId",
                principalTable: "LocationReadModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpawnReadModel_LocationReadModel_LocationReadModelId",
                table: "SpawnReadModel",
                column: "LocationReadModelId",
                principalTable: "LocationReadModel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlacedItemReadModel_LocationReadModel_LocationReadModelId",
                table: "PlacedItemReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_SpawnReadModel_LocationReadModel_LocationReadModelId",
                table: "SpawnReadModel");

            migrationBuilder.DropTable(
                name: "LocationReadModel");

            migrationBuilder.DropIndex(
                name: "IX_SpawnReadModel_LocationReadModelId",
                table: "SpawnReadModel");

            migrationBuilder.DropIndex(
                name: "IX_PlacedItemReadModel_LocationReadModelId",
                table: "PlacedItemReadModel");

            migrationBuilder.DropColumn(
                name: "LocationReadModelId",
                table: "SpawnReadModel");

            migrationBuilder.DropColumn(
                name: "LocationReadModelId",
                table: "PlacedItemReadModel");

            migrationBuilder.DropColumn(
                name: "EventEndDate",
                table: "LocationGroupReadModel");

            migrationBuilder.DropColumn(
                name: "EventName",
                table: "LocationGroupReadModel");

            migrationBuilder.DropColumn(
                name: "EventStartDate",
                table: "LocationGroupReadModel");

            migrationBuilder.DropColumn(
                name: "IsEventRegion",
                table: "LocationGroupReadModel");

            migrationBuilder.DropColumn(
                name: "NextLocationGroupName",
                table: "LocationGroupReadModel");

            migrationBuilder.DropColumn(
                name: "NextLocationGroupResourceName",
                table: "LocationGroupReadModel");

            migrationBuilder.DropColumn(
                name: "PreviousLocationGroupName",
                table: "LocationGroupReadModel");

            migrationBuilder.DropColumn(
                name: "PreviousLocationGroupResourceName",
                table: "LocationGroupReadModel");
        }
    }
}
