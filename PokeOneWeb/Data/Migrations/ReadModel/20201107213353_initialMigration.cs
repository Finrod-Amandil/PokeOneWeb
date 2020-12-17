using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonListReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(nullable: true),
                    PokedexNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Sprite = table.Column<string>(nullable: true),
                    Type1 = table.Column<string>(nullable: true),
                    Type2 = table.Column<string>(nullable: true),
                    Atk = table.Column<int>(nullable: false),
                    Spa = table.Column<int>(nullable: false),
                    Def = table.Column<int>(nullable: false),
                    Spd = table.Column<int>(nullable: false),
                    Spe = table.Column<int>(nullable: false),
                    Hp = table.Column<int>(nullable: false),
                    StatTotal = table.Column<int>(nullable: false),
                    EvAmount = table.Column<int>(nullable: false),
                    EvStat = table.Column<string>(nullable: true),
                    PrimaryAbility = table.Column<string>(nullable: true),
                    SecondaryAbility = table.Column<string>(nullable: true),
                    HiddenAbility = table.Column<string>(nullable: true),
                    Availability = table.Column<string>(nullable: true),
                    PvpTier = table.Column<string>(nullable: true),
                    SmogonUrl = table.Column<string>(nullable: true),
                    BulbapediaUrl = table.Column<string>(nullable: true),
                    PokeOneCommunityUrl = table.Column<string>(nullable: true),
                    PokemonShowDownUrl = table.Column<string>(nullable: true),
                    SerebiiUrl = table.Column<string>(nullable: true),
                    PokemonDbUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonListReadModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonListReadModel");
        }
    }
}
