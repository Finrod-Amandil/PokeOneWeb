using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class add_unique_indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnableMove_PokemonVariety_PokemonVarietyId",
                table: "LearnableMove");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_EvolutionChain_EvlutionChainId",
                table: "PokemonVariety");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonVariety_EvlutionChainId",
                newName: "IX_PokemonVariety_EvolutionChainId",
                table: "PokemonVariety");

            migrationBuilder.RenameColumn(
                name: "EvlutionChainId",
                table: "PokemonVariety",
                newName: "EvolutionChainId");

            migrationBuilder.DropColumn(
                name: "ProkemonVarietyId",
                table: "LearnableMove");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "TimeOfDay",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SpawnType",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "Season",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Region",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonVariety",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonSpecies",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonForm",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LocationGroup",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Location",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyId",
                table: "LearnableMove",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Item",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOfDay_Abbreviation",
                table: "TimeOfDay",
                column: "Abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpawnType_Name",
                table: "SpawnType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_Abbreviation",
                table: "Season",
                column: "Abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_Name",
                table: "Region",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_Name",
                table: "PokemonVariety",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_Name",
                table: "PokemonSpecies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_PokedexNumber",
                table: "PokemonSpecies",
                column: "PokedexNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForm_Name",
                table: "PokemonForm",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationGroup_Name",
                table: "LocationGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Name",
                table: "Location",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_Name",
                table: "Item",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LearnableMove_PokemonVariety_PokemonVarietyId",
                table: "LearnableMove",
                column: "PokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_EvolutionChain_EvolutionChainId",
                table: "PokemonVariety",
                column: "EvolutionChainId",
                principalTable: "EvolutionChain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnableMove_PokemonVariety_PokemonVarietyId",
                table: "LearnableMove");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_EvolutionChain_EvolutionChainId",
                table: "PokemonVariety");

            migrationBuilder.DropIndex(
                name: "IX_TimeOfDay_Abbreviation",
                table: "TimeOfDay");

            migrationBuilder.DropIndex(
                name: "IX_SpawnType_Name",
                table: "SpawnType");

            migrationBuilder.DropIndex(
                name: "IX_Season_Abbreviation",
                table: "Season");

            migrationBuilder.DropIndex(
                name: "IX_Region_Name",
                table: "Region");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonVariety_EvolutionChainId",
                newName: "IX_PokemonVariety_EvlutionChainId",
                table: "PokemonVariety");

            migrationBuilder.DropIndex(
                name: "IX_PokemonVariety_Name",
                table: "PokemonVariety");

            migrationBuilder.DropIndex(
                name: "IX_PokemonSpecies_Name",
                table: "PokemonSpecies");

            migrationBuilder.DropIndex(
                name: "IX_PokemonSpecies_PokedexNumber",
                table: "PokemonSpecies");

            migrationBuilder.DropIndex(
                name: "IX_PokemonForm_Name",
                table: "PokemonForm");

            migrationBuilder.DropIndex(
                name: "IX_LocationGroup_Name",
                table: "LocationGroup");

            migrationBuilder.DropIndex(
                name: "IX_Location_Name",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Item_Name",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "TimeOfDay");

            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "Season");

            migrationBuilder.RenameColumn(
                name: "EvolutionChainId",
                table: "PokemonVariety",
                newName: "EvlutionChainId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SpawnType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Region",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonVariety",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonSpecies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonForm",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LocationGroup",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "PokemonVarietyId",
                table: "LearnableMove",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProkemonVarietyId",
                table: "LearnableMove",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_EvlutionChainId",
                table: "PokemonVariety",
                column: "EvlutionChainId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnableMove_PokemonVariety_PokemonVarietyId",
                table: "LearnableMove",
                column: "PokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_EvolutionChain_EvlutionChainId",
                table: "PokemonVariety",
                column: "EvlutionChainId",
                principalTable: "EvolutionChain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
