using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImportSheet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpreadsheetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SheetName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SheetHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportSheet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoveLearnMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveLearnMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PokeApiName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttackBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    SpecialAttackBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    DefenseBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    SpecialDefenseBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    SpeedBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    HitPointsBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    BoostConditions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ability_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BagCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BagCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BagCategory_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ElementalType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PokeApiName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementalType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementalType_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MoveDamageClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveDamageClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveDamageClass_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Nature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    SpecialAttack = table.Column<int>(type: "int", nullable: false),
                    SpecialDefense = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nature_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PokemonAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAvailability_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PvpTier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PvpTier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PvpTier_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Season_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpawnType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    IsSyncable = table.Column<bool>(type: "bit", nullable: false),
                    IsInfinite = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpawnType_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimeOfDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOfDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeOfDay_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PokeApiName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokeoneItemId = table.Column<int>(type: "int", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    DoInclude = table.Column<bool>(type: "bit", nullable: false),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BagCategoryId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Item_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ElementalTypeRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    AttackEffectivity = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    AttackingTypeId = table.Column<int>(type: "int", nullable: false),
                    DefendingTypeId = table.Column<int>(type: "int", nullable: false)
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ElementalTypeRelation_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsEventRegion = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Region_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Move",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    ResourceName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PokeApiName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoInclude = table.Column<bool>(type: "bit", nullable: false),
                    AttackPower = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    PowerPoints = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Effect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DamageClassId = table.Column<int>(type: "int", nullable: false),
                    ElementalTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Move", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Move_ElementalType_ElementalTypeId",
                        column: x => x.ElementalTypeId,
                        principalTable: "ElementalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Move_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Move_MoveDamageClass_DamageClassId",
                        column: x => x.DamageClassId,
                        principalTable: "MoveDamageClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeasonTimesOfDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    StartHour = table.Column<int>(type: "int", nullable: false),
                    EndHour = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: true),
                    TimeOfDayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonTimesOfDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonTimesOfDay_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SeasonTimesOfDay_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeasonTimesOfDay_TimeOfDay_TimeOfDayId",
                        column: x => x.TimeOfDayId,
                        principalTable: "TimeOfDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currency_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Currency_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemStatBoost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttackBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    DefenseBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    SpecialAttackBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    SpecialDefenseBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    SpeedBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    HitPointsBoost = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatBoost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemStatBoost_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false)
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
                name: "CurrencyAmount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    IsDiscoverable = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    TutorType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NpcName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlacementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoveLearnMethodId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveLearnMethodLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveLearnMethodLocation_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
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
                name: "MoveTutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlacementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveTutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveTutor_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MoveTutor_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlacedItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    PlacementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenshotName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacedItem_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
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
                name: "MoveLearnMethodLocationPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveLearnMethodLocationId = table.Column<int>(type: "int", nullable: false),
                    CurrencyAmountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveLearnMethodLocationPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveLearnMethodLocationPrice_CurrencyAmount_CurrencyAmountId",
                        column: x => x.CurrencyAmountId,
                        principalTable: "CurrencyAmount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveLearnMethodLocationPrice_MoveLearnMethodLocation_MoveLearnMethodLocationId",
                        column: x => x.MoveLearnMethodLocationId,
                        principalTable: "MoveLearnMethodLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveTutorMove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    MoveId = table.Column<int>(type: "int", nullable: false),
                    MoveTutorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveTutorMove", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveTutorMove_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MoveTutorMove_Move_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Move",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveTutorMove_MoveTutor_MoveTutorId",
                        column: x => x.MoveTutorId,
                        principalTable: "MoveTutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveTutorMovePrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveTutorMoveId = table.Column<int>(type: "int", nullable: false),
                    CurrencyAmountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveTutorMovePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveTutorMovePrice_CurrencyAmount_CurrencyAmountId",
                        column: x => x.CurrencyAmountId,
                        principalTable: "CurrencyAmount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveTutorMovePrice_MoveTutorMove_MoveTutorMoveId",
                        column: x => x.MoveTutorMoveId,
                        principalTable: "MoveTutorMove",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Build",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttackEv = table.Column<int>(type: "int", nullable: false),
                    DefenseEv = table.Column<int>(type: "int", nullable: false),
                    SpecialAttackEv = table.Column<int>(type: "int", nullable: false),
                    SpecialDefenseEv = table.Column<int>(type: "int", nullable: false),
                    SpeedEv = table.Column<int>(type: "int", nullable: false),
                    HitPointsEv = table.Column<int>(type: "int", nullable: false),
                    PokemonVarietyId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Build", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Build_Ability_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Build_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    BuildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOption_Build_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Build",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemOption_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveId = table.Column<int>(type: "int", nullable: false),
                    BuildId = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveOption_Build_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Build",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveOption_Move_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Move",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NatureOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NatureId = table.Column<int>(type: "int", nullable: false),
                    BuildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatureOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NatureOption_Build_BuildId",
                        column: x => x.BuildId,
                        principalTable: "Build",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NatureOption_Nature_NatureId",
                        column: x => x.NatureId,
                        principalTable: "Nature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evolution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    EvolutionTrigger = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseStage = table.Column<int>(type: "int", nullable: false),
                    EvolvedStage = table.Column<int>(type: "int", nullable: false),
                    IsReversible = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    DoInclude = table.Column<bool>(type: "bit", nullable: false),
                    BasePokemonSpeciesId = table.Column<int>(type: "int", nullable: false),
                    BasePokemonVarietyId = table.Column<int>(type: "int", nullable: false),
                    EvolvedPokemonVarietyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolution_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HuntingConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    PokemonVarietyId = table.Column<int>(type: "int", nullable: false),
                    NatureId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuntingConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HuntingConfiguration_Ability_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HuntingConfiguration_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HuntingConfiguration_Nature_NatureId",
                        column: x => x.NatureId,
                        principalTable: "Nature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemStatBoostPokemon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    ItemStatBoostId = table.Column<int>(type: "int", nullable: false),
                    PokemonVarietyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStatBoostPokemon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemStatBoostPokemon_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemStatBoostPokemon_ItemStatBoost_ItemStatBoostId",
                        column: x => x.ItemStatBoostId,
                        principalTable: "ItemStatBoost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearnableMove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoveId = table.Column<int>(type: "int", nullable: false),
                    PokemonVarietyId = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "LearnableMoveLearnMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    LevelLearnedAt = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LearnableMoveId = table.Column<int>(type: "int", nullable: false),
                    MoveLearnMethodId = table.Column<int>(type: "int", nullable: false),
                    RequiredItemId = table.Column<int>(type: "int", nullable: true),
                    MoveTutorMoveId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnableMoveLearnMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnableMoveLearnMethod_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LearnableMoveLearnMethod_Item_RequiredItemId",
                        column: x => x.RequiredItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                        name: "FK_LearnableMoveLearnMethod_MoveTutorMove_MoveTutorMoveId",
                        column: x => x.MoveTutorMoveId,
                        principalTable: "MoveTutorMove",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PokemonForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PokeApiName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    SpriteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonVarietyId = table.Column<int>(type: "int", nullable: false),
                    AvailabilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonForm_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonForm_PokemonAvailability_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "PokemonAvailability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spawn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImportSheetId = table.Column<int>(type: "int", nullable: false),
                    SpawnCommonality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpawnProbability = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    EncounterCount = table.Column<int>(type: "int", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LowestLevel = table.Column<int>(type: "int", nullable: false),
                    HighestLevel = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpawnTypeId = table.Column<int>(type: "int", nullable: false),
                    PokemonFormId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spawn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spawn_ImportSheet_ImportSheetId",
                        column: x => x.ImportSheetId,
                        principalTable: "ImportSheet",
                        principalColumn: "Id");
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
                name: "SpawnOpportunity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonSpawnId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    TimeOfDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpawnOpportunity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpawnOpportunity_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpawnOpportunity_Spawn_PokemonSpawnId",
                        column: x => x.PokemonSpawnId,
                        principalTable: "Spawn",
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
                name: "PokemonSpecies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokedexNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PokeApiName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultVarietyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSpecies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonVariety",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PokeApiName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoInclude = table.Column<bool>(type: "bit", nullable: false),
                    IsMega = table.Column<bool>(type: "bit", nullable: false),
                    IsFullyEvolved = table.Column<bool>(type: "bit", nullable: false),
                    Generation = table.Column<int>(type: "int", nullable: false),
                    CatchRate = table.Column<int>(type: "int", nullable: false),
                    HasGender = table.Column<bool>(type: "bit", nullable: false),
                    MaleRatio = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    EggCycles = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    ExpYield = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    SpecialAttack = table.Column<int>(type: "int", nullable: false),
                    SpecialDefense = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    HitPoints = table.Column<int>(type: "int", nullable: false),
                    AttackEv = table.Column<int>(type: "int", nullable: false),
                    DefenseEv = table.Column<int>(type: "int", nullable: false),
                    SpecialAttackEv = table.Column<int>(type: "int", nullable: false),
                    SpecialDefenseEv = table.Column<int>(type: "int", nullable: false),
                    SpeedEv = table.Column<int>(type: "int", nullable: false),
                    HitPointsEv = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PokemonSpeciesId = table.Column<int>(type: "int", nullable: false),
                    DefaultFormId = table.Column<int>(type: "int", nullable: true),
                    PrimaryTypeId = table.Column<int>(type: "int", nullable: false),
                    SecondaryTypeId = table.Column<int>(type: "int", nullable: true),
                    PrimaryAbilityId = table.Column<int>(type: "int", nullable: false),
                    SecondaryAbilityId = table.Column<int>(type: "int", nullable: true),
                    HiddenAbilityId = table.Column<int>(type: "int", nullable: true),
                    PvpTierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonVariety", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonVariety_Ability_HiddenAbilityId",
                        column: x => x.HiddenAbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonVariety_Ability_PrimaryAbilityId",
                        column: x => x.PrimaryAbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonVariety_Ability_SecondaryAbilityId",
                        column: x => x.SecondaryAbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonVariety_ElementalType_PrimaryTypeId",
                        column: x => x.PrimaryTypeId,
                        principalTable: "ElementalType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonVariety_ElementalType_SecondaryTypeId",
                        column: x => x.SecondaryTypeId,
                        principalTable: "ElementalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PokemonVariety_PokemonForm_DefaultFormId",
                        column: x => x.DefaultFormId,
                        principalTable: "PokemonForm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonVariety_PokemonSpecies_PokemonSpeciesId",
                        column: x => x.PokemonSpeciesId,
                        principalTable: "PokemonSpecies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonVariety_PvpTier_PvpTierId",
                        column: x => x.PvpTierId,
                        principalTable: "PvpTier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonVarietyUrl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VarietyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonVarietyUrl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonVarietyUrl_PokemonVariety_VarietyId",
                        column: x => x.VarietyId,
                        principalTable: "PokemonVariety",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ability_Hash",
                table: "Ability",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ability_IdHash",
                table: "Ability",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ability_ImportSheetId",
                table: "Ability",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Ability_Name",
                table: "Ability",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BagCategory_Hash",
                table: "BagCategory",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BagCategory_IdHash",
                table: "BagCategory",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BagCategory_ImportSheetId",
                table: "BagCategory",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_BagCategory_Name",
                table: "BagCategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Build_AbilityId",
                table: "Build",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_Hash",
                table: "Build",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Build_IdHash",
                table: "Build",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Build_ImportSheetId",
                table: "Build",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Build_PokemonVarietyId",
                table: "Build",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Hash",
                table: "Currency",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_IdHash",
                table: "Currency",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_ImportSheetId",
                table: "Currency",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_ItemId",
                table: "Currency",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmount_CurrencyId",
                table: "CurrencyAmount",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalType_Hash",
                table: "ElementalType",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElementalType_IdHash",
                table: "ElementalType",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElementalType_ImportSheetId",
                table: "ElementalType",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalType_Name",
                table: "ElementalType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeRelation_AttackingTypeId",
                table: "ElementalTypeRelation",
                column: "AttackingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeRelation_DefendingTypeId",
                table: "ElementalTypeRelation",
                column: "DefendingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeRelation_Hash",
                table: "ElementalTypeRelation",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeRelation_IdHash",
                table: "ElementalTypeRelation",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ElementalTypeRelation_ImportSheetId",
                table: "ElementalTypeRelation",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Hash",
                table: "Event",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_IdHash",
                table: "Event",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_ImportSheetId",
                table: "Event",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Name",
                table: "Event",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_BasePokemonSpeciesId",
                table: "Evolution",
                column: "BasePokemonSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_BasePokemonVarietyId",
                table: "Evolution",
                column: "BasePokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_EvolvedPokemonVarietyId",
                table: "Evolution",
                column: "EvolvedPokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_Hash",
                table: "Evolution",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_IdHash",
                table: "Evolution",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evolution_ImportSheetId",
                table: "Evolution",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfiguration_AbilityId",
                table: "HuntingConfiguration",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfiguration_Hash",
                table: "HuntingConfiguration",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfiguration_IdHash",
                table: "HuntingConfiguration",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfiguration_ImportSheetId",
                table: "HuntingConfiguration",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfiguration_NatureId",
                table: "HuntingConfiguration",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_HuntingConfiguration_PokemonVarietyId",
                table: "HuntingConfiguration",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportSheet_SheetName",
                table: "ImportSheet",
                column: "SheetName");

            migrationBuilder.CreateIndex(
                name: "IX_Item_BagCategoryId",
                table: "Item",
                column: "BagCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Hash",
                table: "Item",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_IdHash",
                table: "Item",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ImportSheetId",
                table: "Item",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Name",
                table: "Item",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ResourceName",
                table: "Item",
                column: "ResourceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemOption_BuildId",
                table: "ItemOption",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOption_ItemId",
                table: "ItemOption",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoost_ItemId",
                table: "ItemStatBoost",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoostPokemon_Hash",
                table: "ItemStatBoostPokemon",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoostPokemon_IdHash",
                table: "ItemStatBoostPokemon",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoostPokemon_ImportSheetId",
                table: "ItemStatBoostPokemon",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoostPokemon_ItemStatBoostId",
                table: "ItemStatBoostPokemon",
                column: "ItemStatBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStatBoostPokemon_PokemonVarietyId",
                table: "ItemStatBoostPokemon",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMove_MoveId",
                table: "LearnableMove",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMove_PokemonVarietyId",
                table: "LearnableMove",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_Hash",
                table: "LearnableMoveLearnMethod",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_IdHash",
                table: "LearnableMoveLearnMethod",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_ImportSheetId",
                table: "LearnableMoveLearnMethod",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_LearnableMoveId",
                table: "LearnableMoveLearnMethod",
                column: "LearnableMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_MoveLearnMethodId",
                table: "LearnableMoveLearnMethod",
                column: "MoveLearnMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_MoveTutorMoveId",
                table: "LearnableMoveLearnMethod",
                column: "MoveTutorMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_RequiredItemId",
                table: "LearnableMoveLearnMethod",
                column: "RequiredItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Hash",
                table: "Location",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_IdHash",
                table: "Location",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_ImportSheetId",
                table: "Location",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationGroupId",
                table: "Location",
                column: "LocationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Name",
                table: "Location",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationGroup_Name",
                table: "LocationGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationGroup_RegionId",
                table: "LocationGroup",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationGroup_ResourceName",
                table: "LocationGroup",
                column: "ResourceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Move_DamageClassId",
                table: "Move",
                column: "DamageClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_ElementalTypeId",
                table: "Move",
                column: "ElementalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Move_Hash",
                table: "Move",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Move_IdHash",
                table: "Move",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Move_ImportSheetId",
                table: "Move",
                column: "ImportSheetId");

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
                name: "IX_MoveDamageClass_Hash",
                table: "MoveDamageClass",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveDamageClass_IdHash",
                table: "MoveDamageClass",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveDamageClass_ImportSheetId",
                table: "MoveDamageClass",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveDamageClass_Name",
                table: "MoveDamageClass",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethod_Name",
                table: "MoveLearnMethod",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethodLocation_Hash",
                table: "MoveLearnMethodLocation",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethodLocation_IdHash",
                table: "MoveLearnMethodLocation",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethodLocation_ImportSheetId",
                table: "MoveLearnMethodLocation",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethodLocation_LocationId",
                table: "MoveLearnMethodLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethodLocation_MoveLearnMethodId",
                table: "MoveLearnMethodLocation",
                column: "MoveLearnMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethodLocationPrice_CurrencyAmountId",
                table: "MoveLearnMethodLocationPrice",
                column: "CurrencyAmountId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethodLocationPrice_MoveLearnMethodLocationId",
                table: "MoveLearnMethodLocationPrice",
                column: "MoveLearnMethodLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveOption_BuildId",
                table: "MoveOption",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveOption_MoveId",
                table: "MoveOption",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutor_Hash",
                table: "MoveTutor",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutor_IdHash",
                table: "MoveTutor",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutor_ImportSheetId",
                table: "MoveTutor",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutor_LocationId",
                table: "MoveTutor",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutor_Name",
                table: "MoveTutor",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutorMove_Hash",
                table: "MoveTutorMove",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutorMove_IdHash",
                table: "MoveTutorMove",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutorMove_ImportSheetId",
                table: "MoveTutorMove",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutorMove_MoveId",
                table: "MoveTutorMove",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutorMove_MoveTutorId",
                table: "MoveTutorMove",
                column: "MoveTutorId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutorMovePrice_CurrencyAmountId",
                table: "MoveTutorMovePrice",
                column: "CurrencyAmountId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveTutorMovePrice_MoveTutorMoveId",
                table: "MoveTutorMovePrice",
                column: "MoveTutorMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_Nature_Hash",
                table: "Nature",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nature_IdHash",
                table: "Nature",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nature_ImportSheetId",
                table: "Nature",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Nature_Name",
                table: "Nature",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NatureOption_BuildId",
                table: "NatureOption",
                column: "BuildId");

            migrationBuilder.CreateIndex(
                name: "IX_NatureOption_NatureId",
                table: "NatureOption",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItem_Hash",
                table: "PlacedItem",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItem_IdHash",
                table: "PlacedItem",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItem_ImportSheetId",
                table: "PlacedItem",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItem_ItemId",
                table: "PlacedItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacedItem_LocationId",
                table: "PlacedItem",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAvailability_Hash",
                table: "PokemonAvailability",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAvailability_IdHash",
                table: "PokemonAvailability",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAvailability_ImportSheetId",
                table: "PokemonAvailability",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAvailability_Name",
                table: "PokemonAvailability",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForm_AvailabilityId",
                table: "PokemonForm",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForm_Hash",
                table: "PokemonForm",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForm_IdHash",
                table: "PokemonForm",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForm_ImportSheetId",
                table: "PokemonForm",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForm_Name",
                table: "PokemonForm",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonForm_PokemonVarietyId",
                table: "PokemonForm",
                column: "PokemonVarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSpecies_DefaultVarietyId",
                table: "PokemonSpecies",
                column: "DefaultVarietyId");

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
                name: "IX_PokemonVariety_DefaultFormId",
                table: "PokemonVariety",
                column: "DefaultFormId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_HiddenAbilityId",
                table: "PokemonVariety",
                column: "HiddenAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_Name",
                table: "PokemonVariety",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_PokemonSpeciesId",
                table: "PokemonVariety",
                column: "PokemonSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_PrimaryAbilityId",
                table: "PokemonVariety",
                column: "PrimaryAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_PrimaryTypeId",
                table: "PokemonVariety",
                column: "PrimaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_PvpTierId",
                table: "PokemonVariety",
                column: "PvpTierId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_ResourceName",
                table: "PokemonVariety",
                column: "ResourceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_SecondaryAbilityId",
                table: "PokemonVariety",
                column: "SecondaryAbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVariety_SecondaryTypeId",
                table: "PokemonVariety",
                column: "SecondaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonVarietyUrl_VarietyId",
                table: "PokemonVarietyUrl",
                column: "VarietyId");

            migrationBuilder.CreateIndex(
                name: "IX_PvpTier_Hash",
                table: "PvpTier",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PvpTier_IdHash",
                table: "PvpTier",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PvpTier_ImportSheetId",
                table: "PvpTier",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_PvpTier_Name",
                table: "PvpTier",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_EventId",
                table: "Region",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_Hash",
                table: "Region",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_IdHash",
                table: "Region",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_ImportSheetId",
                table: "Region",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_Name",
                table: "Region",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_Abbreviation",
                table: "Season",
                column: "Abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_Hash",
                table: "Season",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_IdHash",
                table: "Season",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_ImportSheetId",
                table: "Season",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Season_Name",
                table: "Season",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTimesOfDay_Hash",
                table: "SeasonTimesOfDay",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTimesOfDay_IdHash",
                table: "SeasonTimesOfDay",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTimesOfDay_ImportSheetId",
                table: "SeasonTimesOfDay",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTimesOfDay_SeasonId",
                table: "SeasonTimesOfDay",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonTimesOfDay_TimeOfDayId",
                table: "SeasonTimesOfDay",
                column: "TimeOfDayId");

            migrationBuilder.CreateIndex(
                name: "IX_Spawn_Hash",
                table: "Spawn",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spawn_IdHash",
                table: "Spawn",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spawn_ImportSheetId",
                table: "Spawn",
                column: "ImportSheetId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SpawnType_Hash",
                table: "SpawnType",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpawnType_IdHash",
                table: "SpawnType",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpawnType_ImportSheetId",
                table: "SpawnType",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_SpawnType_Name",
                table: "SpawnType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeOfDay_Abbreviation",
                table: "TimeOfDay",
                column: "Abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeOfDay_Hash",
                table: "TimeOfDay",
                column: "Hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeOfDay_IdHash",
                table: "TimeOfDay",
                column: "IdHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeOfDay_ImportSheetId",
                table: "TimeOfDay",
                column: "ImportSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeOfDay_Name",
                table: "TimeOfDay",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Build_PokemonVariety_PokemonVarietyId",
                table: "Build",
                column: "PokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evolution_PokemonSpecies_BasePokemonSpeciesId",
                table: "Evolution",
                column: "BasePokemonSpeciesId",
                principalTable: "PokemonSpecies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evolution_PokemonVariety_BasePokemonVarietyId",
                table: "Evolution",
                column: "BasePokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evolution_PokemonVariety_EvolvedPokemonVarietyId",
                table: "Evolution",
                column: "EvolvedPokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HuntingConfiguration_PokemonVariety_PokemonVarietyId",
                table: "HuntingConfiguration",
                column: "PokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStatBoostPokemon_PokemonVariety_PokemonVarietyId",
                table: "ItemStatBoostPokemon",
                column: "PokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LearnableMove_PokemonVariety_PokemonVarietyId",
                table: "LearnableMove",
                column: "PokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonForm_PokemonVariety_PokemonVarietyId",
                table: "PokemonForm",
                column: "PokemonVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSpecies_PokemonVariety_DefaultVarietyId",
                table: "PokemonSpecies",
                column: "DefaultVarietyId",
                principalTable: "PokemonVariety",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ability_ImportSheet_ImportSheetId",
                table: "Ability");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementalType_ImportSheet_ImportSheetId",
                table: "ElementalType");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAvailability_ImportSheet_ImportSheetId",
                table: "PokemonAvailability");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonForm_ImportSheet_ImportSheetId",
                table: "PokemonForm");

            migrationBuilder.DropForeignKey(
                name: "FK_PvpTier_ImportSheet_ImportSheetId",
                table: "PvpTier");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_Ability_HiddenAbilityId",
                table: "PokemonVariety");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_Ability_PrimaryAbilityId",
                table: "PokemonVariety");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonVariety_Ability_SecondaryAbilityId",
                table: "PokemonVariety");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonForm_PokemonVariety_PokemonVarietyId",
                table: "PokemonForm");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSpecies_PokemonVariety_DefaultVarietyId",
                table: "PokemonSpecies");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ElementalTypeRelation");

            migrationBuilder.DropTable(
                name: "Evolution");

            migrationBuilder.DropTable(
                name: "HuntingConfiguration");

            migrationBuilder.DropTable(
                name: "ItemOption");

            migrationBuilder.DropTable(
                name: "ItemStatBoostPokemon");

            migrationBuilder.DropTable(
                name: "LearnableMoveLearnMethod");

            migrationBuilder.DropTable(
                name: "MoveLearnMethodLocationPrice");

            migrationBuilder.DropTable(
                name: "MoveOption");

            migrationBuilder.DropTable(
                name: "MoveTutorMovePrice");

            migrationBuilder.DropTable(
                name: "NatureOption");

            migrationBuilder.DropTable(
                name: "PlacedItem");

            migrationBuilder.DropTable(
                name: "PokemonVarietyUrl");

            migrationBuilder.DropTable(
                name: "SeasonTimesOfDay");

            migrationBuilder.DropTable(
                name: "SpawnOpportunity");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ItemStatBoost");

            migrationBuilder.DropTable(
                name: "LearnableMove");

            migrationBuilder.DropTable(
                name: "MoveLearnMethodLocation");

            migrationBuilder.DropTable(
                name: "CurrencyAmount");

            migrationBuilder.DropTable(
                name: "MoveTutorMove");

            migrationBuilder.DropTable(
                name: "Build");

            migrationBuilder.DropTable(
                name: "Nature");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "Spawn");

            migrationBuilder.DropTable(
                name: "TimeOfDay");

            migrationBuilder.DropTable(
                name: "MoveLearnMethod");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Move");

            migrationBuilder.DropTable(
                name: "MoveTutor");

            migrationBuilder.DropTable(
                name: "SpawnType");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "MoveDamageClass");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "BagCategory");

            migrationBuilder.DropTable(
                name: "LocationGroup");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "ImportSheet");

            migrationBuilder.DropTable(
                name: "Ability");

            migrationBuilder.DropTable(
                name: "PokemonVariety");

            migrationBuilder.DropTable(
                name: "ElementalType");

            migrationBuilder.DropTable(
                name: "PokemonForm");

            migrationBuilder.DropTable(
                name: "PokemonSpecies");

            migrationBuilder.DropTable(
                name: "PvpTier");

            migrationBuilder.DropTable(
                name: "PokemonAvailability");
        }
    }
}