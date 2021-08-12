using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.ReadModelDbMigrations
{
    public partial class Initial_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityTypeReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityTypeReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemStatBoostReadModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtkBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DefBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SpaBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SpdBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SpeBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    HpBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    HasRequiredPokemon = table.Column<bool>(type: "bit", nullable: false),
                    RequiredPokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredPokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatBoostReadModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoveReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DamageClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    PowerPoints = table.Column<int>(type: "int", nullable: false),
                    EffectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NatureReadModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Atk = table.Column<int>(type: "int", nullable: false),
                    Spa = table.Column<int>(type: "int", nullable: false),
                    Def = table.Column<int>(type: "int", nullable: false),
                    Spd = table.Column<int>(type: "int", nullable: false),
                    Spe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureReadModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    PokedexNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Atk = table.Column<int>(type: "int", nullable: false),
                    Spa = table.Column<int>(type: "int", nullable: false),
                    Def = table.Column<int>(type: "int", nullable: false),
                    Spd = table.Column<int>(type: "int", nullable: false),
                    Spe = table.Column<int>(type: "int", nullable: false),
                    Hp = table.Column<int>(type: "int", nullable: false),
                    StatTotal = table.Column<int>(type: "int", nullable: false),
                    PrimaryAbility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryAbilityEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryAbility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryAbilityEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HiddenAbility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HiddenAbilityEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Availability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PvpTier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PvpTierSortIndex = table.Column<int>(type: "int", nullable: false),
                    Generation = table.Column<int>(type: "int", nullable: false),
                    IsFullyEvolved = table.Column<bool>(type: "bit", nullable: false),
                    IsMega = table.Column<bool>(type: "bit", nullable: false),
                    SmogonUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BulbapediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokeOneCommunityUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonShowDownUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerebiiUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonDbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatchRate = table.Column<int>(type: "int", nullable: false),
                    AtkEv = table.Column<int>(type: "int", nullable: false),
                    SpaEv = table.Column<int>(type: "int", nullable: false),
                    DefEv = table.Column<int>(type: "int", nullable: false),
                    SpdEv = table.Column<int>(type: "int", nullable: false),
                    SpeEv = table.Column<int>(type: "int", nullable: false),
                    HpEv = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleLearnableMoveReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MoveName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleLearnableMoveReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbilityTurnsIntoReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonSortIndex = table.Column<int>(type: "int", nullable: false),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AbilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonVarietyAsPrimaryAbilityId = table.Column<int>(type: "int", nullable: true),
                    PokemonVarietyAsSecondaryAbilityId = table.Column<int>(type: "int", nullable: true),
                    PokemonVarietyAsHiddenAbilityId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Move1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Move2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Move3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Move4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AbilityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtkEv = table.Column<int>(type: "int", nullable: false),
                    SpaEv = table.Column<int>(type: "int", nullable: false),
                    DefEv = table.Column<int>(type: "int", nullable: false),
                    SpdEv = table.Column<int>(type: "int", nullable: false),
                    SpeEv = table.Column<int>(type: "int", nullable: false),
                    HpEv = table.Column<int>(type: "int", nullable: false),
                    PokemonReadModelId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseSpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseType1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseType2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseSortIndex = table.Column<int>(type: "int", nullable: false),
                    BaseStage = table.Column<int>(type: "int", nullable: false),
                    EvolvedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedSpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedType1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedType2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedSortIndex = table.Column<int>(type: "int", nullable: false),
                    EvolvedStage = table.Column<int>(type: "int", nullable: false),
                    EvolutionTrigger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReversible = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    PokemonReadModelId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NatureEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonReadModelId = table.Column<int>(type: "int", nullable: true)
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
                name: "LearnableMoveReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DamageClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    PowerPoints = table.Column<int>(type: "int", nullable: false),
                    EffectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnableMoveReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnableMoveReadModel_PokemonReadModel_PokemonReadModelId",
                        column: x => x.PokemonReadModelId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpawnReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonFormSortIndex = table.Column<int>(type: "int", nullable: false),
                    LocationSortIndex = table.Column<int>(type: "int", nullable: false),
                    PokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEvent = table.Column<bool>(type: "bit", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDateRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpawnType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpawnTypeSortIndex = table.Column<int>(type: "int", nullable: false),
                    SpawnTypeColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSyncable = table.Column<bool>(type: "bit", nullable: false),
                    IsInfinite = table.Column<bool>(type: "bit", nullable: false),
                    RarityString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RarityValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonReadModelId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Effectivity = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    BuildReadModelId = table.Column<int>(type: "int", nullable: true),
                    PokemonReadModelId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildReadModelId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NatureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NatureEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildReadModelId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    LearnMethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    LearnableMoveReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnMethodReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnMethodReadModel_LearnableMoveReadModel_LearnableMoveReadModelId",
                        column: x => x.LearnableMoveReadModelId,
                        principalTable: "LearnableMoveReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeasonReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpawnReadModelId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Times = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpawnReadModelId = table.Column<int>(type: "int", nullable: true)
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
                name: "IX_LearnableMoveReadModel_PokemonReadModelId",
                table: "LearnableMoveReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnMethodReadModel_LearnableMoveReadModelId",
                table: "LearnMethodReadModel",
                column: "LearnableMoveReadModelId");

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
                name: "IX_PokemonReadModel_ResourceName",
                table: "PokemonReadModel",
                column: "ResourceName",
                unique: true,
                filter: "[ResourceName] IS NOT NULL");

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
                name: "ItemStatBoostReadModels");

            migrationBuilder.DropTable(
                name: "LearnMethodReadModel");

            migrationBuilder.DropTable(
                name: "MoveReadModel");

            migrationBuilder.DropTable(
                name: "NatureOptionReadModel");

            migrationBuilder.DropTable(
                name: "NatureReadModels");

            migrationBuilder.DropTable(
                name: "SeasonReadModel");

            migrationBuilder.DropTable(
                name: "SimpleLearnableMoveReadModel");

            migrationBuilder.DropTable(
                name: "TimeReadModel");

            migrationBuilder.DropTable(
                name: "LearnableMoveReadModel");

            migrationBuilder.DropTable(
                name: "BuildReadModel");

            migrationBuilder.DropTable(
                name: "SpawnReadModel");

            migrationBuilder.DropTable(
                name: "PokemonReadModel");
        }
    }
}
