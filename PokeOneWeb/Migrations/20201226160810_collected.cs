using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Migrations
{
    public partial class collected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityTypeReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(nullable: true),
                    EntityType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTypeReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonReadModel",
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
                    PvpTier = table.Column<string>(nullable: true),
                    PvpTierSortIndex = table.Column<int>(nullable: false),
                    Generation = table.Column<int>(nullable: false),
                    IsFullyEvolved = table.Column<bool>(nullable: false),
                    IsMega = table.Column<bool>(nullable: false),
                    SmogonUrl = table.Column<string>(nullable: true),
                    BulbapediaUrl = table.Column<string>(nullable: true),
                    PokeOneCommunityUrl = table.Column<string>(nullable: true),
                    PokemonShowDownUrl = table.Column<string>(nullable: true),
                    SerebiiUrl = table.Column<string>(nullable: true),
                    PokemonDbUrl = table.Column<string>(nullable: true),
                    CatchRate = table.Column<int>(nullable: false),
                    AtkEv = table.Column<int>(nullable: false),
                    SpaEv = table.Column<int>(nullable: false),
                    DefEv = table.Column<int>(nullable: false),
                    SpdEv = table.Column<int>(nullable: false),
                    SpeEv = table.Column<int>(nullable: false),
                    HpEv = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleLearnableMoveReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonName = table.Column<string>(nullable: true),
                    MoveName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleLearnableMoveReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbilityTurnsIntoReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonResourceName = table.Column<string>(nullable: true),
                    PokemonName = table.Column<string>(nullable: true),
                    AbilityName = table.Column<string>(nullable: true),
                    PokemonVarietyAsPrimaryAbilityId = table.Column<int>(nullable: false),
                    PokemonVarietyAsSecondaryAbilityId = table.Column<int>(nullable: false),
                    PokemonVarietyAsHiddenAbilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityTurnsIntoReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbilityTurnsIntoReadModel_PokemonReadModel_PokemonVarietyAsHiddenAbilityId",
                        column: x => x.PokemonVarietyAsHiddenAbilityId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AbilityTurnsIntoReadModel_PokemonReadModel_PokemonVarietyAsPrimaryAbilityId",
                        column: x => x.PokemonVarietyAsPrimaryAbilityId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbilityTurnsIntoReadModel_PokemonReadModel_PokemonVarietyAsSecondaryAbilityId",
                        column: x => x.PokemonVarietyAsSecondaryAbilityId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BuildReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonResourceName = table.Column<string>(nullable: true),
                    PokemonName = table.Column<string>(nullable: true),
                    BuildName = table.Column<string>(nullable: true),
                    BuildDescription = table.Column<string>(nullable: true),
                    Move1 = table.Column<string>(nullable: true),
                    Move2 = table.Column<string>(nullable: true),
                    Move3 = table.Column<string>(nullable: true),
                    Move4 = table.Column<string>(nullable: true),
                    Ability = table.Column<string>(nullable: true),
                    AbilityDescription = table.Column<string>(nullable: true),
                    AtkEv = table.Column<int>(nullable: false),
                    SpaEv = table.Column<int>(nullable: false),
                    DefEv = table.Column<int>(nullable: false),
                    SpdEv = table.Column<int>(nullable: false),
                    SpeEv = table.Column<int>(nullable: false),
                    HpEv = table.Column<int>(nullable: false),
                    PokemonReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildReadModel_PokemonReadModel_PokemonReadModelId",
                        column: x => x.PokemonReadModelId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvolutionReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseName = table.Column<string>(nullable: true),
                    BaseResourceName = table.Column<string>(nullable: true),
                    BaseSpriteName = table.Column<string>(nullable: true),
                    BaseStage = table.Column<int>(nullable: false),
                    EvolvedName = table.Column<string>(nullable: true),
                    EvolvedResourceName = table.Column<string>(nullable: true),
                    EvolvedSpriteName = table.Column<string>(nullable: true),
                    EvolvedStage = table.Column<int>(nullable: false),
                    EvolutionTrigger = table.Column<string>(nullable: true),
                    IsReversible = table.Column<bool>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    PokemonReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolutionReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvolutionReadModel_PokemonReadModel_PokemonReadModelId",
                        column: x => x.PokemonReadModelId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HuntingConfigurationReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonResourceName = table.Column<string>(nullable: true),
                    PokemonName = table.Column<string>(nullable: true),
                    Nature = table.Column<string>(nullable: true),
                    NatureEffect = table.Column<string>(nullable: true),
                    Ability = table.Column<string>(nullable: true),
                    PokemonReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntingConfigurationReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HuntingConfigurationReadModel_PokemonReadModel_PokemonReadModelId",
                        column: x => x.PokemonReadModelId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoveReadModel",
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
                    PokemonVarietyAsAvailableMoveId = table.Column<int>(nullable: false),
                    PokemonVarietyAsUnavailableMoveId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveReadModel_PokemonReadModel_PokemonVarietyAsAvailableMoveId",
                        column: x => x.PokemonVarietyAsAvailableMoveId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveReadModel_PokemonReadModel_PokemonVarietyAsUnavailableMoveId",
                        column: x => x.PokemonVarietyAsUnavailableMoveId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpawnReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonResourceName = table.Column<string>(nullable: true),
                    PokemonName = table.Column<string>(nullable: true),
                    LocationName = table.Column<string>(nullable: true),
                    LocationResourceName = table.Column<string>(nullable: true),
                    RegionName = table.Column<string>(nullable: true),
                    IsEvent = table.Column<bool>(nullable: false),
                    EventName = table.Column<string>(nullable: true),
                    EventDateRange = table.Column<string>(nullable: true),
                    SpawnType = table.Column<string>(nullable: true),
                    IsSyncable = table.Column<bool>(nullable: false),
                    IsInfinite = table.Column<bool>(nullable: false),
                    Rarity = table.Column<string>(nullable: true),
                    PokemonReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpawnReadModel_PokemonReadModel_PokemonReadModelId",
                        column: x => x.PokemonReadModelId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttackEffectivityReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(nullable: true),
                    Effectivity = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    BuildReadModelId = table.Column<int>(nullable: true),
                    PokemonReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttackEffectivityReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttackEffectivityReadModel_BuildReadModel_BuildReadModelId",
                        column: x => x.BuildReadModelId,
                        principalTable: "BuildReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttackEffectivityReadModel_PokemonReadModel_PokemonReadModelId",
                        column: x => x.PokemonReadModelId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemOptionReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemResourceName = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    BuildReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOptionReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOptionReadModel_BuildReadModel_BuildReadModelId",
                        column: x => x.BuildReadModelId,
                        principalTable: "BuildReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NatureOptionReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NatureName = table.Column<string>(nullable: true),
                    NatureEffect = table.Column<string>(nullable: true),
                    BuildReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureOptionReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NatureOptionReadModel_BuildReadModel_BuildReadModelId",
                        column: x => x.BuildReadModelId,
                        principalTable: "BuildReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LearnMethodReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnMethodName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    MoveReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnMethodReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnMethodReadModel_MoveReadModel_MoveReadModelId",
                        column: x => x.MoveReadModelId,
                        principalTable: "MoveReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeasonReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SpawnReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonReadModel_SpawnReadModel_SpawnReadModelId",
                        column: x => x.SpawnReadModelId,
                        principalTable: "SpawnReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Times = table.Column<string>(nullable: true),
                    SpawnReadModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeReadModel_SpawnReadModel_SpawnReadModelId",
                        column: x => x.SpawnReadModelId,
                        principalTable: "SpawnReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbilityTurnsIntoReadModel_PokemonVarietyAsHiddenAbilityId",
                table: "AbilityTurnsIntoReadModel",
                column: "PokemonVarietyAsHiddenAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityTurnsIntoReadModel_PokemonVarietyAsPrimaryAbilityId",
                table: "AbilityTurnsIntoReadModel",
                column: "PokemonVarietyAsPrimaryAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityTurnsIntoReadModel_PokemonVarietyAsSecondaryAbilityId",
                table: "AbilityTurnsIntoReadModel",
                column: "PokemonVarietyAsSecondaryAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AttackEffectivityReadModel_BuildReadModelId",
                table: "AttackEffectivityReadModel",
                column: "BuildReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AttackEffectivityReadModel_PokemonReadModelId",
                table: "AttackEffectivityReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildReadModel_PokemonReadModelId",
                table: "BuildReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTypeReadModel_ResourceName",
                table: "EntityTypeReadModel",
                column: "ResourceName",
                unique: true,
                filter: "[ResourceName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EvolutionReadModel_PokemonReadModelId",
                table: "EvolutionReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfigurationReadModel_PokemonReadModelId",
                table: "HuntingConfigurationReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOptionReadModel_BuildReadModelId",
                table: "ItemOptionReadModel",
                column: "BuildReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnMethodReadModel_MoveReadModelId",
                table: "LearnMethodReadModel",
                column: "MoveReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveReadModel_PokemonVarietyAsAvailableMoveId",
                table: "MoveReadModel",
                column: "PokemonVarietyAsAvailableMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveReadModel_PokemonVarietyAsUnavailableMoveId",
                table: "MoveReadModel",
                column: "PokemonVarietyAsUnavailableMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_NatureOptionReadModel_BuildReadModelId",
                table: "NatureOptionReadModel",
                column: "BuildReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonReadModel_Name",
                table: "PokemonReadModel",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonReadModel_SpawnReadModelId",
                table: "SeasonReadModel",
                column: "SpawnReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleLearnableMoveReadModel_MoveName",
                table: "SimpleLearnableMoveReadModel",
                column: "MoveName");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleLearnableMoveReadModel_PokemonName",
                table: "SimpleLearnableMoveReadModel",
                column: "PokemonName");

            migrationBuilder.CreateIndex(
                name: "IX_SpawnReadModel_PokemonReadModelId",
                table: "SpawnReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeReadModel_SpawnReadModelId",
                table: "TimeReadModel",
                column: "SpawnReadModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbilityTurnsIntoReadModel");

            migrationBuilder.DropTable(
                name: "AttackEffectivityReadModel");

            migrationBuilder.DropTable(
                name: "EntityTypeReadModel");

            migrationBuilder.DropTable(
                name: "EvolutionReadModel");

            migrationBuilder.DropTable(
                name: "HuntingConfigurationReadModel");

            migrationBuilder.DropTable(
                name: "ItemOptionReadModel");

            migrationBuilder.DropTable(
                name: "LearnMethodReadModel");

            migrationBuilder.DropTable(
                name: "NatureOptionReadModel");

            migrationBuilder.DropTable(
                name: "SeasonReadModel");

            migrationBuilder.DropTable(
                name: "SimpleLearnableMoveReadModel");

            migrationBuilder.DropTable(
                name: "TimeReadModel");

            migrationBuilder.DropTable(
                name: "MoveReadModel");

            migrationBuilder.DropTable(
                name: "BuildReadModel");

            migrationBuilder.DropTable(
                name: "SpawnReadModel");

            migrationBuilder.DropTable(
                name: "PokemonReadModel");
        }
    }
}
