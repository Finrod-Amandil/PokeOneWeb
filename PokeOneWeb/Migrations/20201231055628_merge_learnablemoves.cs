using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class merge_learnablemoves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnableMoveReadModel_PokemonReadModel_PokemonVarietyAsAvailableMoveId",
                table: "LearnableMoveReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_LearnableMoveReadModel_PokemonReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "LearnableMoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_LearnableMoveReadModel_PokemonVarietyAsAvailableMoveId",
                table: "LearnableMoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_LearnableMoveReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "LearnableMoveReadModel");

            migrationBuilder.DropColumn(
                name: "PokemonVarietyAsAvailableMoveId",
                table: "LearnableMoveReadModel");

            migrationBuilder.DropColumn(
                name: "PokemonVarietyAsUnavailableMoveId",
                table: "LearnableMoveReadModel");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceName",
                table: "PokemonReadModel",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "LearnableMoveReadModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PokemonReadModelId",
                table: "LearnableMoveReadModel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonReadModel_ResourceName",
                table: "PokemonReadModel",
                column: "ResourceName",
                unique: true,
                filter: "[ResourceName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveReadModel_PokemonReadModelId",
                table: "LearnableMoveReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnableMoveReadModel_PokemonReadModel_PokemonReadModelId",
                table: "LearnableMoveReadModel",
                column: "PokemonReadModelId",
                principalTable: "PokemonReadModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnableMoveReadModel_PokemonReadModel_PokemonReadModelId",
                table: "LearnableMoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_PokemonReadModel_ResourceName",
                table: "PokemonReadModel");

            migrationBuilder.DropIndex(
                name: "IX_LearnableMoveReadModel_PokemonReadModelId",
                table: "LearnableMoveReadModel");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "LearnableMoveReadModel");

            migrationBuilder.DropColumn(
                name: "PokemonReadModelId",
                table: "LearnableMoveReadModel");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceName",
                table: "PokemonReadModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PokemonVarietyAsAvailableMoveId",
                table: "LearnableMoveReadModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PokemonVarietyAsUnavailableMoveId",
                table: "LearnableMoveReadModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveReadModel_PokemonVarietyAsAvailableMoveId",
                table: "LearnableMoveReadModel",
                column: "PokemonVarietyAsAvailableMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "LearnableMoveReadModel",
                column: "PokemonVarietyAsUnavailableMoveId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnableMoveReadModel_PokemonReadModel_PokemonVarietyAsAvailableMoveId",
                table: "LearnableMoveReadModel",
                column: "PokemonVarietyAsAvailableMoveId",
                principalTable: "PokemonReadModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LearnableMoveReadModel_PokemonReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "LearnableMoveReadModel",
                column: "PokemonVarietyAsUnavailableMoveId",
                principalTable: "PokemonReadModel",
                principalColumn: "Id");
        }
    }
}
