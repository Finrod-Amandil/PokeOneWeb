using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.ReadModelDbMigrations
{
    public partial class pokemonvarietyadditionalinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondaryType",
                table: "PokemonVarietyReadModel",
                newName: "SecondaryElementalType");

            migrationBuilder.RenameColumn(
                name: "PrimaryType",
                table: "PokemonVarietyReadModel",
                newName: "PrimaryElementalType");

            migrationBuilder.AddColumn<int>(
                name: "EggCycles",
                table: "PokemonVarietyReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpYield",
                table: "PokemonVarietyReadModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "FemaleRatio",
                table: "PokemonVarietyReadModel",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "HasGender",
                table: "PokemonVarietyReadModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "PokemonVarietyReadModel",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MaleRatio",
                table: "PokemonVarietyReadModel",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "PokemonVarietyReadModel",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PokemonVarietyFormReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonVarietyReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonVarietyFormReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonVarietyFormReadModel_PokemonVarietyReadModel_PokemonVarietyReadModelId",
                        column: x => x.PokemonVarietyReadModelId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PokemonVarietyVarietyReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonVarietyReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonVarietyVarietyReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonVarietyVarietyReadModel_PokemonVarietyReadModel_PokemonVarietyReadModelId",
                        column: x => x.PokemonVarietyReadModelId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarietyFormReadModel_PokemonVarietyReadModelId",
                table: "PokemonVarietyFormReadModel",
                column: "PokemonVarietyReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarietyVarietyReadModel_PokemonVarietyReadModelId",
                table: "PokemonVarietyVarietyReadModel",
                column: "PokemonVarietyReadModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonVarietyFormReadModel");

            migrationBuilder.DropTable(
                name: "PokemonVarietyVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "EggCycles",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "ExpYield",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "FemaleRatio",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "HasGender",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "MaleRatio",
                table: "PokemonVarietyReadModel");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "PokemonVarietyReadModel");

            migrationBuilder.RenameColumn(
                name: "SecondaryElementalType",
                table: "PokemonVarietyReadModel",
                newName: "SecondaryType");

            migrationBuilder.RenameColumn(
                name: "PrimaryElementalType",
                table: "PokemonVarietyReadModel",
                newName: "PrimaryType");
        }
    }
}
