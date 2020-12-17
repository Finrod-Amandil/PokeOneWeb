using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class more_pokemon_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BulbapediaUrl",
                table: "PokemonVariety",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PvpTierId",
                table: "PokemonVariety",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "PokemonVariety",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmogonUrl",
                table: "PokemonVariety",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sprite",
                table: "PokemonVariety",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PvpTier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortIndex = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PvpTier", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_PvpTierId",
                table: "PokemonVariety",
                column: "PvpTierId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_PvpTier_PvpTierId",
                table: "PokemonVariety",
                column: "PvpTierId",
                principalTable: "PvpTier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_PvpTier_PvpTierId",
                table: "PokemonVariety");

            migrationBuilder.DropTable(
                name: "PvpTier");

            migrationBuilder.DropIndex(
                name: "IX_PokemonVariety_PvpTierId",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "BulbapediaUrl",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "PvpTierId",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "SmogonUrl",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "Sprite",
                table: "PokemonVariety");
        }
    }
}
