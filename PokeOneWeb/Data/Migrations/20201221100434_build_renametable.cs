using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class build_renametable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Builds_Ability_AbilityId",
                table: "Builds");

            migrationBuilder.DropForeignKey(
                name: "FK_Builds_Stats_EvDistributionId",
                table: "Builds");

            migrationBuilder.DropForeignKey(
                name: "FK_Builds_PokemonVariety_PokemonVarietyId",
                table: "Builds");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemOption_Builds_BuildId",
                table: "ItemOption");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveOption_Builds_BuildId",
                table: "MoveOption");

            migrationBuilder.DropForeignKey(
                name: "FK_NatureOption_Builds_BuildId",
                table: "NatureOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Builds",
                table: "Builds");

            migrationBuilder.RenameTable(
                name: "Builds",
                newName: "Build");

            migrationBuilder.RenameIndex(
                name: "IX_Builds_PokemonVarietyId",
                table: "Build",
                newName: "IX_Build_PokemonVarietyId");

            migrationBuilder.RenameIndex(
                name: "IX_Builds_EvDistributionId",
                table: "Build",
                newName: "IX_Build_EvDistributionId");

            migrationBuilder.RenameIndex(
                name: "IX_Builds_AbilityId",
                table: "Build",
                newName: "IX_Build_AbilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Build",
                table: "Build",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Build_Ability_AbilityId",
                table: "Build",
                column: "AbilityId",
                principalTable: "Ability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Build_Stats_EvDistributionId",
                table: "Build",
                column: "EvDistributionId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Build_PokemonVariety_PokemonVarietyId",
                table: "Build",
                column: "PokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOption_Build_BuildId",
                table: "ItemOption",
                column: "BuildId",
                principalTable: "Build",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveOption_Build_BuildId",
                table: "MoveOption",
                column: "BuildId",
                principalTable: "Build",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NatureOption_Build_BuildId",
                table: "NatureOption",
                column: "BuildId",
                principalTable: "Build",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Build_Ability_AbilityId",
                table: "Build");

            migrationBuilder.DropForeignKey(
                name: "FK_Build_Stats_EvDistributionId",
                table: "Build");

            migrationBuilder.DropForeignKey(
                name: "FK_Build_PokemonVariety_PokemonVarietyId",
                table: "Build");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemOption_Build_BuildId",
                table: "ItemOption");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveOption_Build_BuildId",
                table: "MoveOption");

            migrationBuilder.DropForeignKey(
                name: "FK_NatureOption_Build_BuildId",
                table: "NatureOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Build",
                table: "Build");

            migrationBuilder.RenameTable(
                name: "Build",
                newName: "Builds");

            migrationBuilder.RenameIndex(
                name: "IX_Build_PokemonVarietyId",
                table: "Builds",
                newName: "IX_Builds_PokemonVarietyId");

            migrationBuilder.RenameIndex(
                name: "IX_Build_EvDistributionId",
                table: "Builds",
                newName: "IX_Builds_EvDistributionId");

            migrationBuilder.RenameIndex(
                name: "IX_Build_AbilityId",
                table: "Builds",
                newName: "IX_Builds_AbilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Builds",
                table: "Builds",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Builds_Ability_AbilityId",
                table: "Builds",
                column: "AbilityId",
                principalTable: "Ability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Builds_Stats_EvDistributionId",
                table: "Builds",
                column: "EvDistributionId",
                principalTable: "Stats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Builds_PokemonVariety_PokemonVarietyId",
                table: "Builds",
                column: "PokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOption_Builds_BuildId",
                table: "ItemOption",
                column: "BuildId",
                principalTable: "Builds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveOption_Builds_BuildId",
                table: "MoveOption",
                column: "BuildId",
                principalTable: "Builds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NatureOption_Builds_BuildId",
                table: "NatureOption",
                column: "BuildId",
                principalTable: "Builds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
