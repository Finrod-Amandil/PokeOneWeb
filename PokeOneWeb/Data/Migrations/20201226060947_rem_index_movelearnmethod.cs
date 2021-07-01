using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations
{
    public partial class rem_index_movelearnmethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MoveLearnMethod_Name",
                table: "MoveLearnMethod");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MoveLearnMethod",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MoveLearnMethod",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_MoveLearnMethod_Name",
                table: "MoveLearnMethod",
                column: "Name",
                unique: true);
        }
    }
}
