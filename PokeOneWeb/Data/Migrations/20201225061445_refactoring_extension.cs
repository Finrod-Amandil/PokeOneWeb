using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class refactoring_extension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Region_EventRegionId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Evolution_EvolutionChain_EvolutionChainId",
                table: "Evolution");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_ElementalTypeCombination_ElementalTypeCombinationId",
                table: "PokemonVariety");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_EvolutionChain_EvolutionChainId",
                table: "PokemonVariety");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimeOfDay_Season_SeasonId",
                table: "SeasonTimeOfDay");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimeOfDay_TimeOfDay_TimeOfDayId",
                table: "SeasonTimeOfDay");

            migrationBuilder.DropTable(
                name: "ElementalTypeCombination");

            migrationBuilder.DropTable(
                name: "EvolutionChain");

            migrationBuilder.DropIndex(
                name: "IX_PokemonVariety_ElementalTypeCombinationId",
                table: "PokemonVariety");

            migrationBuilder.DropIndex(
                name: "IX_PokemonVariety_EvolutionChainId",
                table: "PokemonVariety");

            migrationBuilder.DropIndex(
                name: "IX_Evolution_EvolutionChainId",
                table: "Evolution");

            migrationBuilder.DropIndex(
                name: "IX_Event_EventRegionId",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeasonTimeOfDay",
                table: "SeasonTimeOfDay");

            migrationBuilder.DropColumn(
                name: "ElementalTypeCombinationId",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "EvolutionChainId",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Move");

            migrationBuilder.DropColumn(
                name: "Available",
                table: "Evolution");

            migrationBuilder.DropColumn(
                name: "EvolutionChainId",
                table: "Evolution");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "Evolution");

            migrationBuilder.DropColumn(
                name: "EventRegionId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Currency");

            migrationBuilder.RenameTable(
                name: "SeasonTimeOfDay",
                newName: "SeasonTimesOfDay");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimeOfDay_TimeOfDayId",
                table: "SeasonTimesOfDay",
                newName: "IX_SeasonTimesOfDay_TimeOfDayId");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimeOfDay_SeasonId",
                table: "SeasonTimesOfDay",
                newName: "IX_SeasonTimesOfDay_SeasonId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TimeOfDay",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsInfinite",
                table: "SpawnType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSyncable",
                table: "SpawnType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Season",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Region",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PvpTier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceName",
                table: "PokemonVariety",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrimaryTypeId",
                table: "PokemonVariety",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondaryTypeId",
                table: "PokemonVariety",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonAvailability",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Nature",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MoveLearnMethod",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MoveDamageClass",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Move",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "Move",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "LocationGroup",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "DoInclude",
                table: "Item",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "Item",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SortIndex",
                table: "Item",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BasePokemonSpeciesId",
                table: "Evolution",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseStage",
                table: "Evolution",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DoInclude",
                table: "Evolution",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EvolvedStage",
                table: "Evolution",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Evolution",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReversible",
                table: "Evolution",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Event",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ElementalType",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BagCategory",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "SortIndex",
                table: "BagCategory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ability",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeasonTimesOfDay",
                table: "SeasonTimesOfDay",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOfDay_Name",
                table: "TimeOfDay",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_Name",
                table: "Season",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_EventId",
                table: "Region",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_PvpTier_Name",
                table: "PvpTier",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_PrimaryTypeId",
                table: "PokemonVariety",
                column: "PrimaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_ResourceName",
                table: "PokemonVariety",
                column: "ResourceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_SecondaryTypeId",
                table: "PokemonVariety",
                column: "SecondaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAvailability_Name",
                table: "PokemonAvailability",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nature_Name",
                table: "Nature",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethod_Name",
                table: "MoveLearnMethod",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveDamageClass_Name",
                table: "MoveDamageClass",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Move_Name",
                table: "Move",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Move_ResourceName",
                table: "Move",
                column: "ResourceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationGroup_ResourceName",
                table: "LocationGroup",
                column: "ResourceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ResourceName",
                table: "Item",
                column: "ResourceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_BasePokemonSpeciesId",
                table: "Evolution",
                column: "BasePokemonSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Name",
                table: "Event",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElementalType_Name",
                table: "ElementalType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BagCategory_Name",
                table: "BagCategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ability_Name",
                table: "Ability",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evolution_PokemonSpecies_BasePokemonSpeciesId",
                table: "Evolution",
                column: "BasePokemonSpeciesId",
                principalTable: "PokemonSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_ElementalType_PrimaryTypeId",
                table: "PokemonVariety",
                column: "PrimaryTypeId",
                principalTable: "ElementalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_ElementalType_SecondaryTypeId",
                table: "PokemonVariety",
                column: "SecondaryTypeId",
                principalTable: "ElementalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Region_Event_EventId",
                table: "Region",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimesOfDay_Season_SeasonId",
                table: "SeasonTimesOfDay",
                column: "SeasonId",
                principalTable: "Season",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimesOfDay_TimeOfDay_TimeOfDayId",
                table: "SeasonTimesOfDay",
                column: "TimeOfDayId",
                principalTable: "TimeOfDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evolution_PokemonSpecies_BasePokemonSpeciesId",
                table: "Evolution");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_ElementalType_PrimaryTypeId",
                table: "PokemonVariety");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_ElementalType_SecondaryTypeId",
                table: "PokemonVariety");

            migrationBuilder.DropForeignKey(
                name: "FK_Region_Event_EventId",
                table: "Region");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimesOfDay_Season_SeasonId",
                table: "SeasonTimesOfDay");

            migrationBuilder.DropForeignKey(
                name: "FK_SeasonTimesOfDay_TimeOfDay_TimeOfDayId",
                table: "SeasonTimesOfDay");

            migrationBuilder.DropIndex(
                name: "IX_TimeOfDay_Name",
                table: "TimeOfDay");

            migrationBuilder.DropIndex(
                name: "IX_Season_Name",
                table: "Season");

            migrationBuilder.DropIndex(
                name: "IX_Region_EventId",
                table: "Region");

            migrationBuilder.DropIndex(
                name: "IX_PvpTier_Name",
                table: "PvpTier");

            migrationBuilder.DropIndex(
                name: "IX_PokemonVariety_PrimaryTypeId",
                table: "PokemonVariety");

            migrationBuilder.DropIndex(
                name: "IX_PokemonVariety_ResourceName",
                table: "PokemonVariety");

            migrationBuilder.DropIndex(
                name: "IX_PokemonVariety_SecondaryTypeId",
                table: "PokemonVariety");

            migrationBuilder.DropIndex(
                name: "IX_PokemonAvailability_Name",
                table: "PokemonAvailability");

            migrationBuilder.DropIndex(
                name: "IX_Nature_Name",
                table: "Nature");

            migrationBuilder.DropIndex(
                name: "IX_MoveLearnMethod_Name",
                table: "MoveLearnMethod");

            migrationBuilder.DropIndex(
                name: "IX_MoveDamageClass_Name",
                table: "MoveDamageClass");

            migrationBuilder.DropIndex(
                name: "IX_Move_Name",
                table: "Move");

            migrationBuilder.DropIndex(
                name: "IX_Move_ResourceName",
                table: "Move");

            migrationBuilder.DropIndex(
                name: "IX_LocationGroup_ResourceName",
                table: "LocationGroup");

            migrationBuilder.DropIndex(
                name: "IX_Item_ResourceName",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Evolution_BasePokemonSpeciesId",
                table: "Evolution");

            migrationBuilder.DropIndex(
                name: "IX_Event_Name",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_ElementalType_Name",
                table: "ElementalType");

            migrationBuilder.DropIndex(
                name: "IX_BagCategory_Name",
                table: "BagCategory");

            migrationBuilder.DropIndex(
                name: "IX_Ability_Name",
                table: "Ability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeasonTimesOfDay",
                table: "SeasonTimesOfDay");

            migrationBuilder.DropColumn(
                name: "IsInfinite",
                table: "SpawnType");

            migrationBuilder.DropColumn(
                name: "IsSyncable",
                table: "SpawnType");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "PrimaryTypeId",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "SecondaryTypeId",
                table: "PokemonVariety");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "Move");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "LocationGroup");

            migrationBuilder.DropColumn(
                name: "DoInclude",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "SortIndex",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "BasePokemonSpeciesId",
                table: "Evolution");

            migrationBuilder.DropColumn(
                name: "BaseStage",
                table: "Evolution");

            migrationBuilder.DropColumn(
                name: "DoInclude",
                table: "Evolution");

            migrationBuilder.DropColumn(
                name: "EvolvedStage",
                table: "Evolution");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Evolution");

            migrationBuilder.DropColumn(
                name: "IsReversible",
                table: "Evolution");

            migrationBuilder.DropColumn(
                name: "SortIndex",
                table: "BagCategory");

            migrationBuilder.RenameTable(
                name: "SeasonTimesOfDay",
                newName: "SeasonTimeOfDay");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimesOfDay_TimeOfDayId",
                table: "SeasonTimeOfDay",
                newName: "IX_SeasonTimeOfDay_TimeOfDayId");

            migrationBuilder.RenameIndex(
                name: "IX_SeasonTimesOfDay_SeasonId",
                table: "SeasonTimeOfDay",
                newName: "IX_SeasonTimeOfDay_SeasonId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TimeOfDay",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Season",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PvpTier",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ResourceName",
                table: "PokemonVariety",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ElementalTypeCombinationId",
                table: "PokemonVariety",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EvolutionChainId",
                table: "PokemonVariety",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PokemonAvailability",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Nature",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MoveLearnMethod",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MoveDamageClass",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Move",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Move",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Evolution",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EvolutionChainId",
                table: "Evolution",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "Evolution",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Event",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "EventRegionId",
                table: "Event",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ElementalType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Currency",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BagCategory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ability",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeasonTimeOfDay",
                table: "SeasonTimeOfDay",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ElementalTypeCombination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryTypeId = table.Column<int>(type: "int", nullable: false),
                    SecondaryTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementalTypeCombination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementalTypeCombination_ElementalType_PrimaryTypeId",
                        column: x => x.PrimaryTypeId,
                        principalTable: "ElementalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementalTypeCombination_ElementalType_SecondaryTypeId",
                        column: x => x.SecondaryTypeId,
                        principalTable: "ElementalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvolutionChain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokeApiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolutionChain", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_ElementalTypeCombinationId",
                table: "PokemonVariety",
                column: "ElementalTypeCombinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_EvolutionChainId",
                table: "PokemonVariety",
                column: "EvolutionChainId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_EvolutionChainId",
                table: "Evolution",
                column: "EvolutionChainId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventRegionId",
                table: "Event",
                column: "EventRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeCombination_PrimaryTypeId",
                table: "ElementalTypeCombination",
                column: "PrimaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeCombination_SecondaryTypeId",
                table: "ElementalTypeCombination",
                column: "SecondaryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Region_EventRegionId",
                table: "Event",
                column: "EventRegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evolution_EvolutionChain_EvolutionChainId",
                table: "Evolution",
                column: "EvolutionChainId",
                principalTable: "EvolutionChain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_ElementalTypeCombination_ElementalTypeCombinationId",
                table: "PokemonVariety",
                column: "ElementalTypeCombinationId",
                principalTable: "ElementalTypeCombination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_EvolutionChain_EvolutionChainId",
                table: "PokemonVariety",
                column: "EvolutionChainId",
                principalTable: "EvolutionChain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimeOfDay_Season_SeasonId",
                table: "SeasonTimeOfDay",
                column: "SeasonId",
                principalTable: "Season",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonTimeOfDay_TimeOfDay_TimeOfDayId",
                table: "SeasonTimeOfDay",
                column: "TimeOfDayId",
                principalTable: "TimeOfDay",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
