using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class add_simplelearnablemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimpleLearnableMoveReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonName = table.Column<string>(nullable: true),
                    MoveName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleLearnableMoveReadModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimpleLearnableMoveReadModel");
        }
    }
}
