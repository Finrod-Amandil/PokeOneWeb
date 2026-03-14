using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class LearnableMoveLearnMethodAvailabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LearnableMoveLearnMethodAvailability",
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
                    table.PrimaryKey("PK_LearnableMoveLearnMethodAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnableMoveLearnMethodAvailability_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethodAvailability_Hash",
                table: "LearnableMoveLearnMethodAvailability",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethodAvailability_IdHash",
                table: "LearnableMoveLearnMethodAvailability",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethodAvailability_ImportSheetId",
                table: "LearnableMoveLearnMethodAvailability",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethodAvailability_Name",
                table: "LearnableMoveLearnMethodAvailability",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearnableMoveLearnMethodAvailability");
        }
    }
}
