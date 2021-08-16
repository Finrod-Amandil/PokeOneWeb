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
                name: "ItemStatBoostPokemonReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttackBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DefenseBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SpecialAttackBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SpecialDefenseBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SpeedBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    HitPointsBoost = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    HasRequiredPokemon = table.Column<bool>(type: "bit", nullable: false),
                    RequiredPokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredPokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatBoostPokemonReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoveReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DamageClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    PowerPoints = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    EffectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NatureReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    SpecialAttack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    SpecialDefense = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonVarietyReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    PokedexNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    SpecialAttack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    SpecialDefense = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    HitPoints = table.Column<int>(type: "int", nullable: false),
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
                    CatchRate = table.Column<int>(type: "int", nullable: false),
                    AttackEv = table.Column<int>(type: "int", nullable: false),
                    SpecialAttackEv = table.Column<int>(type: "int", nullable: false),
                    DefenseEv = table.Column<int>(type: "int", nullable: false),
                    SpecialDefenseEv = table.Column<int>(type: "int", nullable: false),
                    SpeedEv = table.Column<int>(type: "int", nullable: false),
                    HitPointsEv = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonVarietyReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleLearnableMoveReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoveName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleLearnableMoveReadModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    PokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AbilityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtkEv = table.Column<int>(type: "int", nullable: false),
                    SpaEv = table.Column<int>(type: "int", nullable: false),
                    DefEv = table.Column<int>(type: "int", nullable: false),
                    SpdEv = table.Column<int>(type: "int", nullable: false),
                    SpeEv = table.Column<int>(type: "int", nullable: false),
                    HpEv = table.Column<int>(type: "int", nullable: false),
                    PokemonVarietyReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildReadModel_PokemonVarietyReadModel_PokemonVarietyReadModelId",
                        column: x => x.PokemonVarietyReadModelId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EvolutionAbilityReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelativeStageIndex = table.Column<int>(type: "int", nullable: false),
                    PokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonSortIndex = table.Column<int>(type: "int", nullable: false),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AbilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonVarietyAsPrimaryAbilityId = table.Column<int>(type: "int", nullable: true),
                    PokemonVarietyAsSecondaryAbilityId = table.Column<int>(type: "int", nullable: true),
                    PokemonVarietyAsHiddenAbilityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolutionAbilityReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvolutionAbilityReadModel_PokemonVarietyReadModel_PokemonVarietyAsHiddenAbilityId",
                        column: x => x.PokemonVarietyAsHiddenAbilityId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EvolutionAbilityReadModel_PokemonVarietyReadModel_PokemonVarietyAsPrimaryAbilityId",
                        column: x => x.PokemonVarietyAsPrimaryAbilityId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EvolutionAbilityReadModel_PokemonVarietyReadModel_PokemonVarietyAsSecondaryAbilityId",
                        column: x => x.PokemonVarietyAsSecondaryAbilityId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EvolutionReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseSpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasePrimaryElementalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseSecondaryElementalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseSortIndex = table.Column<int>(type: "int", nullable: false),
                    BaseStage = table.Column<int>(type: "int", nullable: false),
                    EvolvedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedSpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedPrimaryElementalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedSecondaryElementalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvolvedSortIndex = table.Column<int>(type: "int", nullable: false),
                    EvolvedStage = table.Column<int>(type: "int", nullable: false),
                    EvolutionTrigger = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReversible = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    PokemonVarietyReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvolutionReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvolutionReadModel_PokemonVarietyReadModel_PokemonVarietyReadModelId",
                        column: x => x.PokemonVarietyReadModelId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HuntingConfigurationReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    PokemonResourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NatureEffect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonVarietyReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntingConfigurationReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HuntingConfigurationReadModel_PokemonVarietyReadModel_PokemonVarietyReadModelId",
                        column: x => x.PokemonVarietyReadModelId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LearnableMoveReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    MoveName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DamageClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    PowerPoints = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    EffectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonVarietyReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnableMoveReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnableMoveReadModel_PokemonVarietyReadModel_PokemonVarietyReadModelId",
                        column: x => x.PokemonVarietyReadModelId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PokemonVarietyUrlReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonVarietyReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonVarietyUrlReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonVarietyUrlReadModel_PokemonVarietyReadModel_PokemonVarietyReadModelId",
                        column: x => x.PokemonVarietyReadModelId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpawnReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
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
                    EventStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpawnType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpawnTypeSortIndex = table.Column<int>(type: "int", nullable: false),
                    SpawnTypeColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSyncable = table.Column<bool>(type: "bit", nullable: false),
                    IsInfinite = table.Column<bool>(type: "bit", nullable: false),
                    LowestLevel = table.Column<int>(type: "int", nullable: false),
                    HighestLevel = table.Column<int>(type: "int", nullable: false),
                    RarityString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RarityValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonVarietyReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpawnReadModel_PokemonVarietyReadModel_PokemonVarietyReadModelId",
                        column: x => x.PokemonVarietyReadModelId,
                        principalTable: "PokemonVarietyReadModel",
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
                    PokemonVarietyReadModelId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_AttackEffectivityReadModel_PokemonVarietyReadModel_PokemonVarietyReadModelId",
                        column: x => x.PokemonVarietyReadModelId,
                        principalTable: "PokemonVarietyReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemOptionReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
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
                name: "MoveOptionReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    MoveName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DamageClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    PowerPoints = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    EffectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildReadModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveOptionReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveOptionReadModel_BuildReadModel_BuildReadModelId",
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
                    ApplicationDbId = table.Column<int>(type: "int", nullable: false),
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
                name: "TimeOfDayReadModel",
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
                    table.PrimaryKey("PK_TimeOfDayReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeOfDayReadModel_SpawnReadModel_SpawnReadModelId",
                        column: x => x.SpawnReadModelId,
                        principalTable: "SpawnReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttackEffectivityReadModel_BuildReadModelId",
                table: "AttackEffectivityReadModel",
                column: "BuildReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AttackEffectivityReadModel_PokemonVarietyReadModelId",
                table: "AttackEffectivityReadModel",
                column: "PokemonVarietyReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildReadModel_ApplicationDbId",
                table: "BuildReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuildReadModel_PokemonVarietyReadModelId",
                table: "BuildReadModel",
                column: "PokemonVarietyReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityTypeReadModel_ResourceName",
                table: "EntityTypeReadModel",
                column: "ResourceName",
                unique: true,
                filter: "[ResourceName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EvolutionAbilityReadModel_PokemonVarietyAsHiddenAbilityId",
                table: "EvolutionAbilityReadModel",
                column: "PokemonVarietyAsHiddenAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_EvolutionAbilityReadModel_PokemonVarietyAsPrimaryAbilityId",
                table: "EvolutionAbilityReadModel",
                column: "PokemonVarietyAsPrimaryAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_EvolutionAbilityReadModel_PokemonVarietyAsSecondaryAbilityId",
                table: "EvolutionAbilityReadModel",
                column: "PokemonVarietyAsSecondaryAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_EvolutionReadModel_ApplicationDbId",
                table: "EvolutionReadModel",
                column: "ApplicationDbId");

            migrationBuilder.CreateIndex(
                name: "IX_EvolutionReadModel_PokemonVarietyReadModelId",
                table: "EvolutionReadModel",
                column: "PokemonVarietyReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfigurationReadModel_ApplicationDbId",
                table: "HuntingConfigurationReadModel",
                column: "ApplicationDbId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfigurationReadModel_PokemonVarietyReadModelId",
                table: "HuntingConfigurationReadModel",
                column: "PokemonVarietyReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOptionReadModel_ApplicationDbId",
                table: "ItemOptionReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemOptionReadModel_BuildReadModelId",
                table: "ItemOptionReadModel",
                column: "BuildReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoostPokemonReadModel_ApplicationDbId",
                table: "ItemStatBoostPokemonReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveReadModel_ApplicationDbId",
                table: "LearnableMoveReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveReadModel_PokemonVarietyReadModelId",
                table: "LearnableMoveReadModel",
                column: "PokemonVarietyReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnMethodReadModel_LearnableMoveReadModelId",
                table: "LearnMethodReadModel",
                column: "LearnableMoveReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveOptionReadModel_ApplicationDbId",
                table: "MoveOptionReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveOptionReadModel_BuildReadModelId",
                table: "MoveOptionReadModel",
                column: "BuildReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveReadModel_ApplicationDbId",
                table: "MoveReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NatureOptionReadModel_ApplicationDbId",
                table: "NatureOptionReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NatureOptionReadModel_BuildReadModelId",
                table: "NatureOptionReadModel",
                column: "BuildReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_NatureReadModel_ApplicationDbId",
                table: "NatureReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarietyReadModel_ApplicationDbId",
                table: "PokemonVarietyReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarietyReadModel_Name",
                table: "PokemonVarietyReadModel",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarietyReadModel_ResourceName",
                table: "PokemonVarietyReadModel",
                column: "ResourceName",
                unique: true,
                filter: "[ResourceName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarietyUrlReadModel_ApplicationDbId",
                table: "PokemonVarietyUrlReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarietyUrlReadModel_PokemonVarietyReadModelId",
                table: "PokemonVarietyUrlReadModel",
                column: "PokemonVarietyReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonReadModel_SpawnReadModelId",
                table: "SeasonReadModel",
                column: "SpawnReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleLearnableMoveReadModel_ApplicationDbId",
                table: "SimpleLearnableMoveReadModel",
                column: "ApplicationDbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpawnReadModel_PokemonVarietyReadModelId",
                table: "SpawnReadModel",
                column: "PokemonVarietyReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOfDayReadModel_SpawnReadModelId",
                table: "TimeOfDayReadModel",
                column: "SpawnReadModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttackEffectivityReadModel");

            migrationBuilder.DropTable(
                name: "EntityTypeReadModel");

            migrationBuilder.DropTable(
                name: "EvolutionAbilityReadModel");

            migrationBuilder.DropTable(
                name: "EvolutionReadModel");

            migrationBuilder.DropTable(
                name: "HuntingConfigurationReadModel");

            migrationBuilder.DropTable(
                name: "ItemOptionReadModel");

            migrationBuilder.DropTable(
                name: "ItemStatBoostPokemonReadModel");

            migrationBuilder.DropTable(
                name: "LearnMethodReadModel");

            migrationBuilder.DropTable(
                name: "MoveOptionReadModel");

            migrationBuilder.DropTable(
                name: "MoveReadModel");

            migrationBuilder.DropTable(
                name: "NatureOptionReadModel");

            migrationBuilder.DropTable(
                name: "NatureReadModel");

            migrationBuilder.DropTable(
                name: "PokemonVarietyUrlReadModel");

            migrationBuilder.DropTable(
                name: "SeasonReadModel");

            migrationBuilder.DropTable(
                name: "SimpleLearnableMoveReadModel");

            migrationBuilder.DropTable(
                name: "TimeOfDayReadModel");

            migrationBuilder.DropTable(
                name: "LearnableMoveReadModel");

            migrationBuilder.DropTable(
                name: "BuildReadModel");

            migrationBuilder.DropTable(
                name: "SpawnReadModel");

            migrationBuilder.DropTable(
                name: "PokemonVarietyReadModel");
        }
    }
}
