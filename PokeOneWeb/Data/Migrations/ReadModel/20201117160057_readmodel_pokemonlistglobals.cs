using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class readmodel_pokemonlistglobals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonListReadModel");

            migrationBuilder.CreateTable(
                name: "PokemonListEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(nullable: true),
                    PokedexNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SpriteName = table.Column<string>(nullable: true),
                    Type1 = table.Column<string>(nullable: true),
                    Type2 = table.Column<string>(nullable: true),
                    Atk = table.Column<int>(nullable: false),
                    Spa = table.Column<int>(nullable: false),
                    Def = table.Column<int>(nullable: false),
                    Spd = table.Column<int>(nullable: false),
                    Spe = table.Column<int>(nullable: false),
                    Hp = table.Column<int>(nullable: false),
                    StatTotal = table.Column<int>(nullable: false),
                    PrimaryAbility = table.Column<string>(nullable: true),
                    PrimaryAbilityEffect = table.Column<string>(nullable: true),
                    SecondaryAbility = table.Column<string>(nullable: true),
                    SecondaryAbilityEffect = table.Column<string>(nullable: true),
                    HiddenAbility = table.Column<string>(nullable: true),
                    HiddenAbilityEffect = table.Column<string>(nullable: true),
                    Availability = table.Column<string>(nullable: true),
                    AvailabilityDescription = table.Column<string>(nullable: true),
                    PvpTier = table.Column<string>(nullable: true),
                    PvpTierSortIndex = table.Column<int>(nullable: false),
                    SmogonUrl = table.Column<string>(nullable: true),
                    BulbapediaUrl = table.Column<string>(nullable: true),
                    PokeOneCommunityUrl = table.Column<string>(nullable: true),
                    PokemonShowDownUrl = table.Column<string>(nullable: true),
                    SerebiiUrl = table.Column<string>(nullable: true),
                    PokemonDbUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonListEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonListGlobals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxAtk = table.Column<int>(nullable: false),
                    MaxSpa = table.Column<int>(nullable: false),
                    MaxDef = table.Column<int>(nullable: false),
                    MaxSpd = table.Column<int>(nullable: false),
                    MaxSpe = table.Column<int>(nullable: false),
                    MaxHP = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonListGlobals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonListEntry");

            migrationBuilder.DropTable(
                name: "PokemonListGlobals");

            migrationBuilder.CreateTable(
                name: "PokemonListReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Atk = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailabilityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BulbapediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Def = table.Column<int>(type: "int", nullable: false),
                    HiddenAbility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HiddenAbilityEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hp = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokeOneCommunityUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokedexNumber = table.Column<int>(type: "int", nullable: false),
                    PokemonDbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonShowDownUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryAbility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryAbilityEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PvpTier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PvpTierSortIndex = table.Column<int>(type: "int", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryAbility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryAbilityEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerebiiUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmogonUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spa = table.Column<int>(type: "int", nullable: false),
                    Spd = table.Column<int>(type: "int", nullable: false),
                    Spe = table.Column<int>(type: "int", nullable: false),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatTotal = table.Column<int>(type: "int", nullable: false),
                    Type1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonListReadModel", x => x.Id);
                });
        }
    }
}
