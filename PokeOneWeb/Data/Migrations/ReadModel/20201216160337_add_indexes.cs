using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class add_indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PokemonName",
                table: "SimpleLearnableMoveReadModel",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MoveName",
                table: "SimpleLearnableMoveReadModel",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonReadModel",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimpleLearnableMoveReadModel_MoveName",
                table: "SimpleLearnableMoveReadModel",
                column: "MoveName");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleLearnableMoveReadModel_PokemonName",
                table: "SimpleLearnableMoveReadModel",
                column: "PokemonName");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonReadModel_Name",
                table: "PokemonReadModel",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SimpleLearnableMoveReadModel_MoveName",
                table: "SimpleLearnableMoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_SimpleLearnableMoveReadModel_PokemonName",
                table: "SimpleLearnableMoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_PokemonReadModel_Name",
                table: "PokemonReadModel");

            migrationBuilder.AlterColumn<string>(
                name: "PokemonName",
                table: "SimpleLearnableMoveReadModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MoveName",
                table: "SimpleLearnableMoveReadModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonReadModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
