using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class separate_learnablemoves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnMethodReadModel_MoveReadModel_MoveReadModelId",
                table: "LearnMethodReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveReadModel_PokemonReadModel_PokemonVarietyAsAvailableMoveId",
                table: "MoveReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveReadModel_PokemonReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "MoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_MoveReadModel_PokemonVarietyAsAvailableMoveId",
                table: "MoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_MoveReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "MoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_LearnMethodReadModel_MoveReadModelId",
                table: "LearnMethodReadModel");

            migrationBuilder.DropColumn(
                name: "PokemonVarietyAsAvailableMoveId",
                table: "MoveReadModel");

            migrationBuilder.DropColumn(
                name: "PokemonVarietyAsUnavailableMoveId",
                table: "MoveReadModel");

            migrationBuilder.DropColumn(
                name: "MoveReadModelId",
                table: "LearnMethodReadModel");

            migrationBuilder.AddColumn<int>(
                name: "LearnableMoveReadModelId",
                table: "LearnMethodReadModel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LearnableMoveReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    DamageClass = table.Column<string>(nullable: true),
                    AttackPower = table.Column<int>(nullable: false),
                    Accuracy = table.Column<int>(nullable: false),
                    PowerPoints = table.Column<int>(nullable: false),
                    EffectDescription = table.Column<string>(nullable: true),
                    PokemonVarietyAsAvailableMoveId = table.Column<int>(nullable: true),
                    PokemonVarietyAsUnavailableMoveId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnableMoveReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnableMoveReadModel_PokemonReadModel_PokemonVarietyAsAvailableMoveId",
                        column: x => x.PokemonVarietyAsAvailableMoveId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnableMoveReadModel_PokemonReadModel_PokemonVarietyAsUnavailableMoveId",
                        column: x => x.PokemonVarietyAsUnavailableMoveId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearnMethodReadModel_LearnableMoveReadModelId",
                table: "LearnMethodReadModel",
                column: "LearnableMoveReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveReadModel_PokemonVarietyAsAvailableMoveId",
                table: "LearnableMoveReadModel",
                column: "PokemonVarietyAsAvailableMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "LearnableMoveReadModel",
                column: "PokemonVarietyAsUnavailableMoveId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnMethodReadModel_LearnableMoveReadModel_LearnableMoveReadModelId",
                table: "LearnMethodReadModel",
                column: "LearnableMoveReadModelId",
                principalTable: "LearnableMoveReadModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnMethodReadModel_LearnableMoveReadModel_LearnableMoveReadModelId",
                table: "LearnMethodReadModel");

            migrationBuilder.DropTable(
                name: "LearnableMoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_LearnMethodReadModel_LearnableMoveReadModelId",
                table: "LearnMethodReadModel");

            migrationBuilder.DropColumn(
                name: "LearnableMoveReadModelId",
                table: "LearnMethodReadModel");

            migrationBuilder.AddColumn<int>(
                name: "PokemonVarietyAsAvailableMoveId",
                table: "MoveReadModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PokemonVarietyAsUnavailableMoveId",
                table: "MoveReadModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MoveReadModelId",
                table: "LearnMethodReadModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveReadModel_PokemonVarietyAsAvailableMoveId",
                table: "MoveReadModel",
                column: "PokemonVarietyAsAvailableMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "MoveReadModel",
                column: "PokemonVarietyAsUnavailableMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnMethodReadModel_MoveReadModelId",
                table: "LearnMethodReadModel",
                column: "MoveReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnMethodReadModel_MoveReadModel_MoveReadModelId",
                table: "LearnMethodReadModel",
                column: "MoveReadModelId",
                principalTable: "MoveReadModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveReadModel_PokemonReadModel_PokemonVarietyAsAvailableMoveId",
                table: "MoveReadModel",
                column: "PokemonVarietyAsAvailableMoveId",
                principalTable: "PokemonReadModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveReadModel_PokemonReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "MoveReadModel",
                column: "PokemonVarietyAsUnavailableMoveId",
                principalTable: "PokemonReadModel",
                principalColumn: "Id");
        }
    }
}
