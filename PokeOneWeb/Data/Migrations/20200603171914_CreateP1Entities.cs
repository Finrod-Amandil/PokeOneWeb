using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class CreateP1Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ability",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    EffectDescription = table.Column<string>(nullable: true),
                    EffectShortDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BagCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BagCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElementalType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementalType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EvolutionChain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolutionChain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoveDamageClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveDamageClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoveLearnMethod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveLearnMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAvailability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    IsEventRegion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpawnType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attack = table.Column<int>(nullable: false),
                    Defense = table.Column<int>(nullable: false),
                    SpecialAttack = table.Column<int>(nullable: false),
                    SpecialDefense = table.Column<int>(nullable: false),
                    Speed = table.Column<int>(nullable: false),
                    HitPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeOfDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOfDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    BagCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_BagCategory_BagCategoryId",
                        column: x => x.BagCategoryId,
                        principalTable: "BagCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementalTypeCombination",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryTypeId = table.Column<int>(nullable: false),
                    SecondaryTypeId = table.Column<int>(nullable: false)
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
                name: "ElementalTypeRelation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttackingTypeId = table.Column<int>(nullable: false),
                    DefendingTypeId = table.Column<int>(nullable: false),
                    AttackEffectivity = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    DefendingTypeIdId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementalTypeRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementalTypeRelation_ElementalType_AttackingTypeId",
                        column: x => x.AttackingTypeId,
                        principalTable: "ElementalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementalTypeRelation_ElementalType_DefendingTypeId",
                        column: x => x.DefendingTypeId,
                        principalTable: "ElementalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Move",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DamageClassId = table.Column<int>(nullable: false),
                    ElementalTypeId = table.Column<int>(nullable: false),
                    AttackPower = table.Column<int>(nullable: false),
                    Accuracy = table.Column<int>(nullable: false),
                    PowerPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Move_MoveDamageClass_DamageClassId",
                        column: x => x.DamageClassId,
                        principalTable: "MoveDamageClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Move_ElementalType_ElementalTypeId",
                        column: x => x.ElementalTypeId,
                        principalTable: "ElementalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    EventRegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Region_EventRegionId",
                        column: x => x.EventRegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationGroup_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currency_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    LocationGroupId = table.Column<int>(nullable: false),
                    IsDiscoverable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_LocationGroup_LocationGroupId",
                        column: x => x.LocationGroupId,
                        principalTable: "LocationGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveLearnMethodLocation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveLearnMethodId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    PlacementDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveLearnMethodLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveLearnMethodLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveLearnMethodLocation_MoveLearnMethod_MoveLearnMethodId",
                        column: x => x.MoveLearnMethodId,
                        principalTable: "MoveLearnMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlacedItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    PlacementDescription = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    IsHidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacedItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacedItem_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    ExperienceReward = table.Column<int>(nullable: true),
                    MoneyReward = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    QuestTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quest_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quest_QuestType_QuestTypeId",
                        column: x => x.QuestTypeId,
                        principalTable: "QuestType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shop_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestItemReward",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    QuestId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestItemReward", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestItemReward_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestItemReward_Quest_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonVariety",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    PokemonSpeciesId = table.Column<int>(nullable: false),
                    DefaultFormId = table.Column<int>(nullable: false),
                    BaseStatsId = table.Column<int>(nullable: true),
                    EvYieldId = table.Column<int>(nullable: true),
                    ElementalTypeCombinationId = table.Column<int>(nullable: false),
                    EvlutionChainId = table.Column<int>(nullable: true),
                    EvolutionChainId = table.Column<int>(nullable: false),
                    PrimaryAbilityId = table.Column<int>(nullable: true),
                    SecondaryAbilityId = table.Column<int>(nullable: true),
                    HiddenAbilityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonVariety", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonVariety_Stats_BaseStatsId",
                        column: x => x.BaseStatsId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonVariety_ElementalTypeCombination_ElementalTypeCombinationId",
                        column: x => x.ElementalTypeCombinationId,
                        principalTable: "ElementalTypeCombination",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonVariety_Stats_EvYieldId",
                        column: x => x.EvYieldId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonVariety_EvolutionChain_EvlutionChainId",
                        column: x => x.EvlutionChainId,
                        principalTable: "EvolutionChain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonVariety_Ability_HiddenAbilityId",
                        column: x => x.HiddenAbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonVariety_Ability_PrimaryAbilityId",
                        column: x => x.PrimaryAbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokemonVariety_Ability_SecondaryAbilityId",
                        column: x => x.SecondaryAbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evolution",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvolutionChainId = table.Column<int>(nullable: false),
                    BasePokemonVarietyId = table.Column<int>(nullable: false),
                    EvolvedPokemonVarietyId = table.Column<int>(nullable: false),
                    EvolutionTrigger = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolution_PokemonVariety_BasePokemonVarietyId",
                        column: x => x.BasePokemonVarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evolution_EvolutionChain_EvolutionChainId",
                        column: x => x.EvolutionChainId,
                        principalTable: "EvolutionChain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evolution_PokemonVariety_EvolvedPokemonVarietyId",
                        column: x => x.EvolvedPokemonVarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LearnableMove",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveId = table.Column<int>(nullable: false),
                    PokemonVarietyId = table.Column<int>(nullable: true),
                    ProkemonVarietyId = table.Column<int>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnableMove", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnableMove_Move_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Move",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnableMove_PokemonVariety_PokemonVarietyId",
                        column: x => x.PokemonVarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PokemonForm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    PokemonVarietyId = table.Column<int>(nullable: false),
                    AvailabilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonForm_PokemonAvailability_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "PokemonAvailability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonForm_PokemonVariety_PokemonVarietyId",
                        column: x => x.PokemonVarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonHeldItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    PokemonVarietyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonHeldItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonHeldItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonHeldItem_PokemonVariety_PokemonVarietyId",
                        column: x => x.PokemonVarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonSpecies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokedexNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DefaultVarietyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSpecies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonSpecies_PokemonVariety_DefaultVarietyId",
                        column: x => x.DefaultVarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LearnableMoveLearnMethod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnableMoveId = table.Column<int>(nullable: false),
                    MoveLearnMethodId = table.Column<int>(nullable: false),
                    RequiredItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnableMoveLearnMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnableMoveLearnMethod_LearnableMove_LearnableMoveId",
                        column: x => x.LearnableMoveId,
                        principalTable: "LearnableMove",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnableMoveLearnMethod_MoveLearnMethod_MoveLearnMethodId",
                        column: x => x.MoveLearnMethodId,
                        principalTable: "MoveLearnMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnableMoveLearnMethod_Item_RequiredItemId",
                        column: x => x.RequiredItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Spawn",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonFormId = table.Column<int>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    SpawnTypeId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spawn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spawn_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spawn_PokemonForm_PokemonFormId",
                        column: x => x.PokemonFormId,
                        principalTable: "PokemonForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spawn_SpawnType_SpawnTypeId",
                        column: x => x.SpawnTypeId,
                        principalTable: "SpawnType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyAmount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    LearnableMoveLearnMethodId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyAmount_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyAmount_LearnableMoveLearnMethod_LearnableMoveLearnMethodId",
                        column: x => x.LearnableMoveLearnMethodId,
                        principalTable: "LearnableMoveLearnMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpawnOpportunity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonSpawnId = table.Column<int>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    TimeOfDayId = table.Column<int>(nullable: false),
                    SpawnProbability = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    EncounterCount = table.Column<int>(nullable: true),
                    SpawnCommonality = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnOpportunity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpawnOpportunity_Spawn_PokemonSpawnId",
                        column: x => x.PokemonSpawnId,
                        principalTable: "Spawn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpawnOpportunity_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpawnOpportunity_TimeOfDay_TimeOfDayId",
                        column: x => x.TimeOfDayId,
                        principalTable: "TimeOfDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopBoughtItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    ShopId = table.Column<int>(nullable: false),
                    PriceId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopBoughtItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopBoughtItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopBoughtItem_CurrencyAmount_PriceId",
                        column: x => x.PriceId,
                        principalTable: "CurrencyAmount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopBoughtItem_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopSoldItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: false),
                    ShopId = table.Column<int>(nullable: false),
                    PriceId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopSoldItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopSoldItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopSoldItem_CurrencyAmount_PriceId",
                        column: x => x.PriceId,
                        principalTable: "CurrencyAmount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopSoldItem_Shop_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currency_ItemId",
                table: "Currency",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmount_CurrencyId",
                table: "CurrencyAmount",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmount_LearnableMoveLearnMethodId",
                table: "CurrencyAmount",
                column: "LearnableMoveLearnMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeCombination_PrimaryTypeId",
                table: "ElementalTypeCombination",
                column: "PrimaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeCombination_SecondaryTypeId",
                table: "ElementalTypeCombination",
                column: "SecondaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeRelation_AttackingTypeId",
                table: "ElementalTypeRelation",
                column: "AttackingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeRelation_DefendingTypeId",
                table: "ElementalTypeRelation",
                column: "DefendingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventRegionId",
                table: "Event",
                column: "EventRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_BasePokemonVarietyId",
                table: "Evolution",
                column: "BasePokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_EvolutionChainId",
                table: "Evolution",
                column: "EvolutionChainId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_EvolvedPokemonVarietyId",
                table: "Evolution",
                column: "EvolvedPokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_BagCategoryId",
                table: "Item",
                column: "BagCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMove_MoveId",
                table: "LearnableMove",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMove_PokemonVarietyId",
                table: "LearnableMove",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_LearnableMoveId",
                table: "LearnableMoveLearnMethod",
                column: "LearnableMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_MoveLearnMethodId",
                table: "LearnableMoveLearnMethod",
                column: "MoveLearnMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_RequiredItemId",
                table: "LearnableMoveLearnMethod",
                column: "RequiredItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationGroupId",
                table: "Location",
                column: "LocationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationGroup_RegionId",
                table: "LocationGroup",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_DamageClassId",
                table: "Move",
                column: "DamageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_ElementalTypeId",
                table: "Move",
                column: "ElementalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethodLocation_LocationId",
                table: "MoveLearnMethodLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethodLocation_MoveLearnMethodId",
                table: "MoveLearnMethodLocation",
                column: "MoveLearnMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItem_ItemId",
                table: "PlacedItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItem_LocationId",
                table: "PlacedItem",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForm_AvailabilityId",
                table: "PokemonForm",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForm_PokemonVarietyId",
                table: "PokemonForm",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonHeldItem_ItemId",
                table: "PokemonHeldItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonHeldItem_PokemonVarietyId",
                table: "PokemonHeldItem",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_DefaultVarietyId",
                table: "PokemonSpecies",
                column: "DefaultVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_BaseStatsId",
                table: "PokemonVariety",
                column: "BaseStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_DefaultFormId",
                table: "PokemonVariety",
                column: "DefaultFormId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_ElementalTypeCombinationId",
                table: "PokemonVariety",
                column: "ElementalTypeCombinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_EvYieldId",
                table: "PokemonVariety",
                column: "EvYieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_EvlutionChainId",
                table: "PokemonVariety",
                column: "EvlutionChainId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_HiddenAbilityId",
                table: "PokemonVariety",
                column: "HiddenAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_PokemonSpeciesId",
                table: "PokemonVariety",
                column: "PokemonSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_PrimaryAbilityId",
                table: "PokemonVariety",
                column: "PrimaryAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_SecondaryAbilityId",
                table: "PokemonVariety",
                column: "SecondaryAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Quest_LocationId",
                table: "Quest",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Quest_QuestTypeId",
                table: "Quest",
                column: "QuestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestItemReward_ItemId",
                table: "QuestItemReward",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestItemReward_QuestId",
                table: "QuestItemReward",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_LocationId",
                table: "Shop",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopBoughtItem_ItemId",
                table: "ShopBoughtItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopBoughtItem_PriceId",
                table: "ShopBoughtItem",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopBoughtItem_ShopId",
                table: "ShopBoughtItem",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopSoldItem_ItemId",
                table: "ShopSoldItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopSoldItem_PriceId",
                table: "ShopSoldItem",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopSoldItem_ShopId",
                table: "ShopSoldItem",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Spawn_LocationId",
                table: "Spawn",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Spawn_PokemonFormId",
                table: "Spawn",
                column: "PokemonFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Spawn_SpawnTypeId",
                table: "Spawn",
                column: "SpawnTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpawnOpportunity_PokemonSpawnId",
                table: "SpawnOpportunity",
                column: "PokemonSpawnId");

            migrationBuilder.CreateIndex(
                name: "IX_SpawnOpportunity_SeasonId",
                table: "SpawnOpportunity",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SpawnOpportunity_TimeOfDayId",
                table: "SpawnOpportunity",
                column: "TimeOfDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_PokemonForm_DefaultFormId",
                table: "PokemonVariety",
                column: "DefaultFormId",
                principalTable: "PokemonForm",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonVariety_PokemonSpecies_PokemonSpeciesId",
                table: "PokemonVariety",
                column: "PokemonSpeciesId",
                principalTable: "PokemonSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElementalTypeCombination_ElementalType_PrimaryTypeId",
                table: "ElementalTypeCombination");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementalTypeCombination_ElementalType_SecondaryTypeId",
                table: "ElementalTypeCombination");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonForm_PokemonVariety_PokemonVarietyId",
                table: "PokemonForm");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSpecies_PokemonVariety_DefaultVarietyId",
                table: "PokemonSpecies");

            migrationBuilder.DropTable(
                name: "ElementalTypeRelation");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Evolution");

            migrationBuilder.DropTable(
                name: "MoveLearnMethodLocation");

            migrationBuilder.DropTable(
                name: "PlacedItem");

            migrationBuilder.DropTable(
                name: "PokemonHeldItem");

            migrationBuilder.DropTable(
                name: "QuestItemReward");

            migrationBuilder.DropTable(
                name: "ShopBoughtItem");

            migrationBuilder.DropTable(
                name: "ShopSoldItem");

            migrationBuilder.DropTable(
                name: "SpawnOpportunity");

            migrationBuilder.DropTable(
                name: "Quest");

            migrationBuilder.DropTable(
                name: "CurrencyAmount");

            migrationBuilder.DropTable(
                name: "Shop");

            migrationBuilder.DropTable(
                name: "Spawn");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "TimeOfDay");

            migrationBuilder.DropTable(
                name: "QuestType");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "LearnableMoveLearnMethod");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "SpawnType");

            migrationBuilder.DropTable(
                name: "LearnableMove");

            migrationBuilder.DropTable(
                name: "MoveLearnMethod");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "LocationGroup");

            migrationBuilder.DropTable(
                name: "Move");

            migrationBuilder.DropTable(
                name: "BagCategory");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "MoveDamageClass");

            migrationBuilder.DropTable(
                name: "ElementalType");

            migrationBuilder.DropTable(
                name: "PokemonVariety");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "PokemonForm");

            migrationBuilder.DropTable(
                name: "ElementalTypeCombination");

            migrationBuilder.DropTable(
                name: "EvolutionChain");

            migrationBuilder.DropTable(
                name: "Ability");

            migrationBuilder.DropTable(
                name: "PokemonSpecies");

            migrationBuilder.DropTable(
                name: "PokemonAvailability");
        }
    }
}
