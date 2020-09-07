using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class move_elementaltypeid_optional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Move_ElementalType_ElementalTypeId",
                table: "Move");

            migrationBuilder.AlterColumn<int>(
                name: "ElementalTypeId",
                table: "Move",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Move_ElementalType_ElementalTypeId",
                table: "Move",
                column: "ElementalTypeId",
                principalTable: "ElementalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Move_ElementalType_ElementalTypeId",
                table: "Move");

            migrationBuilder.AlterColumn<int>(
                name: "ElementalTypeId",
                table: "Move",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Move_ElementalType_ElementalTypeId",
                table: "Move",
                column: "ElementalTypeId",
                principalTable: "ElementalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
