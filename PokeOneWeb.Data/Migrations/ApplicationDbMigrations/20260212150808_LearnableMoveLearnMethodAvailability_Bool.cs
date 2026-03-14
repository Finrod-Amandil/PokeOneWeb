using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeOneWeb.Data.Migrations.ApplicationDbMigrations
{
    public partial class LearnableMoveLearnMethodAvailability_Bool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "LearnableMoveLearnMethod");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "LearnableMoveLearnMethodAvailability",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AvailabilityId",
                table: "LearnableMoveLearnMethod",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LearnableMoveLearnMethod_AvailabilityId",
                table: "LearnableMoveLearnMethod",
                column: "AvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_LearnableMoveLearnMethod_LearnableMoveLearnMethodAvailability_AvailabilityId",
                table: "LearnableMoveLearnMethod",
                column: "AvailabilityId",
                principalTable: "LearnableMoveLearnMethodAvailability",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearnableMoveLearnMethod_LearnableMoveLearnMethodAvailability_AvailabilityId",
                table: "LearnableMoveLearnMethod");

            migrationBuilder.DropIndex(
                name: "IX_LearnableMoveLearnMethod_AvailabilityId",
                table: "LearnableMoveLearnMethod");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "LearnableMoveLearnMethodAvailability");

            migrationBuilder.DropColumn(
                name: "AvailabilityId",
                table: "LearnableMoveLearnMethod");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "LearnableMoveLearnMethod",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
