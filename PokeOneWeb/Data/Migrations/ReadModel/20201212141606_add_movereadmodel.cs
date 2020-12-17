using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class add_movereadmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonListGlobals");

            migrationBuilder.CreateTable(
                name: "MoveReadModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DamageClass = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    AttackPower = table.Column<int>(nullable: false),
                    Accuracy = table.Column<int>(nullable: false),
                    PowerPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveReadModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoveReadModels");

            migrationBuilder.CreateTable(
                name: "PokemonListGlobals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxAtk = table.Column<int>(type: "int", nullable: false),
                    MaxDef = table.Column<int>(type: "int", nullable: false),
                    MaxHp = table.Column<int>(type: "int", nullable: false),
                    MaxSpa = table.Column<int>(type: "int", nullable: false),
                    MaxSpd = table.Column<int>(type: "int", nullable: false),
                    MaxSpe = table.Column<int>(type: "int", nullable: false),
                    MaxTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonListGlobals", x => x.Id);
                });
        }
    }
}
