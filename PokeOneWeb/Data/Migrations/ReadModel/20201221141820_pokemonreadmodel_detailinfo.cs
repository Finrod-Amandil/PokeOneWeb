using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class pokemonreadmodel_detailinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AtkEv",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CatchRate",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefEv",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HpEv",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpaEv",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpdEv",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpeEv",
                table: "PokemonReadModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EffectDescription",
                table: "MoveReadModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PokemonReadModelId",
                table: "MoveReadModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PokemonReadModelId1",
                table: "MoveReadModel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AbilityTurnsIntoReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonResourceName = table.Column<string>(nullable: true),
                    PokemonName = table.Column<string>(nullable: true),
                    AbilityName = table.Column<string>(nullable: true),
                    PokemonReadModelId = table.Column<int>(nullable: true),
                    PokemonReadModelId1 = table.Column<int>(nullable: true),
                    PokemonReadModelId2 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityTurnsIntoReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbilityTurnsIntoReadModel_PokemonReadModel_PokemonReadModelId",
                        column: x => x.PokemonReadModelId,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbilityTurnsIntoReadModel_PokemonReadModel_PokemonReadModelId1",
                        column: x => x.PokemonReadModelId1,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbilityTurnsIntoReadModel_PokemonReadModel_PokemonReadModelId2",
                        column: x => x.PokemonReadModelId2,
                        principalTable: "PokemonReadModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "SpawnReadModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PokemonResourceName = table.Column<string>(nullable: true),
                    PokemonName = table.Column<string>(nullable: true),
                    LocationName = table.Column<string>(nullable: true),
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
                    Effectivity = table.Column<int>(nullable: false),
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
                name: "IX_MoveReadModel_PokemonReadModelId",
                table: "MoveReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveReadModel_PokemonReadModelId1",
                table: "MoveReadModel",
                column: "PokemonReadModelId1");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityTurnsIntoReadModel_PokemonReadModelId",
                table: "AbilityTurnsIntoReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityTurnsIntoReadModel_PokemonReadModelId1",
                table: "AbilityTurnsIntoReadModel",
                column: "PokemonReadModelId1");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityTurnsIntoReadModel_PokemonReadModelId2",
                table: "AbilityTurnsIntoReadModel",
                column: "PokemonReadModelId2");

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
                name: "IX_NatureOptionReadModel_BuildReadModelId",
                table: "NatureOptionReadModel",
                column: "BuildReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonReadModel_SpawnReadModelId",
                table: "SeasonReadModel",
                column: "SpawnReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SpawnReadModel_PokemonReadModelId",
                table: "SpawnReadModel",
                column: "PokemonReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeReadModel_SpawnReadModelId",
                table: "TimeReadModel",
                column: "SpawnReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveReadModel_PokemonReadModel_PokemonReadModelId",
                table: "MoveReadModel",
                column: "PokemonReadModelId",
                principalTable: "PokemonReadModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveReadModel_PokemonReadModel_PokemonReadModelId1",
                table: "MoveReadModel",
                column: "PokemonReadModelId1",
                principalTable: "PokemonReadModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveReadModel_PokemonReadModel_PokemonReadModelId",
                table: "MoveReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_MoveReadModel_PokemonReadModel_PokemonReadModelId1",
                table: "MoveReadModel");

            migrationBuilder.DropTable(
                name: "AbilityTurnsIntoReadModel");

            migrationBuilder.DropTable(
                name: "AttackEffectivityReadModel");

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
                name: "TimeReadModel");

            migrationBuilder.DropTable(
                name: "BuildReadModel");

            migrationBuilder.DropTable(
                name: "SpawnReadModel");

            migrationBuilder.DropIndex(
                name: "IX_MoveReadModel_PokemonReadModelId",
                table: "MoveReadModel");

            migrationBuilder.DropIndex(
                name: "IX_MoveReadModel_PokemonReadModelId1",
                table: "MoveReadModel");

            migrationBuilder.DropColumn(
                name: "AtkEv",
                table: "PokemonReadModel");

            migrationBuilder.DropColumn(
                name: "CatchRate",
                table: "PokemonReadModel");

            migrationBuilder.DropColumn(
                name: "DefEv",
                table: "PokemonReadModel");

            migrationBuilder.DropColumn(
                name: "HpEv",
                table: "PokemonReadModel");

            migrationBuilder.DropColumn(
                name: "SpaEv",
                table: "PokemonReadModel");

            migrationBuilder.DropColumn(
                name: "SpdEv",
                table: "PokemonReadModel");

            migrationBuilder.DropColumn(
                name: "SpeEv",
                table: "PokemonReadModel");

            migrationBuilder.DropColumn(
                name: "EffectDescription",
                table: "MoveReadModel");

            migrationBuilder.DropColumn(
                name: "PokemonReadModelId",
                table: "MoveReadModel");

            migrationBuilder.DropColumn(
                name: "PokemonReadModelId1",
                table: "MoveReadModel");
        }
    }
}
