using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class learnable_move_learn_method_price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmount_LearnableMoveLearnMethod_LearnableMoveLearnMethodId",
                table: "CurrencyAmount");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyAmount_LearnableMoveLearnMethodId",
                table: "CurrencyAmount");

            migrationBuilder.DropColumn(
                name: "LearnableMoveLearnMethodId",
                table: "CurrencyAmount");

            migrationBuilder.AddColumn<int>(
                name: "BigMushrooms",
                table: "TutorMoves",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeartScales",
                table: "TutorMoves",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LevelLearnedAt",
                table: "LearnableMoveLearnMethod",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "LearnableMoveLearnMethodPrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearnableMoveLearnMethodId = table.Column<int>(nullable: false),
                    PriceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnableMoveLearnMethodPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearnableMoveLearnMethodPrice_LearnableMoveLearnMethod_LearnableMoveLearnMethodId",
                        column: x => x.LearnableMoveLearnMethodId,
                        principalTable: "LearnableMoveLearnMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LearnableMoveLearnMethodPrice_CurrencyAmount_PriceId",
                        column: x => x.PriceId,
                        principalTable: "CurrencyAmount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethodPrice_LearnableMoveLearnMethodId",
                table: "LearnableMoveLearnMethodPrice",
                column: "LearnableMoveLearnMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethodPrice_PriceId",
                table: "LearnableMoveLearnMethodPrice",
                column: "PriceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearnableMoveLearnMethodPrice");

            migrationBuilder.DropColumn(
                name: "BigMushrooms",
                table: "TutorMoves");

            migrationBuilder.DropColumn(
                name: "HeartScales",
                table: "TutorMoves");

            migrationBuilder.AlterColumn<int>(
                name: "LevelLearnedAt",
                table: "LearnableMoveLearnMethod",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LearnableMoveLearnMethodId",
                table: "CurrencyAmount",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmount_LearnableMoveLearnMethodId",
                table: "CurrencyAmount",
                column: "LearnableMoveLearnMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmount_LearnableMoveLearnMethod_LearnableMoveLearnMethodId",
                table: "CurrencyAmount",
                column: "LearnableMoveLearnMethodId",
                principalTable: "LearnableMoveLearnMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
