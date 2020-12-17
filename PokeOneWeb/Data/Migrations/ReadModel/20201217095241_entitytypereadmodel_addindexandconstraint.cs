using Microsoft.EntityFrameworkCore.Migrations;

namespace PokeOneWeb.Data.Migrations.ReadModel
{
    public partial class entitytypereadmodel_addindexandconstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResourceName",
                table: "EntityTypeReadModel",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityTypeReadModel_ResourceName",
                table: "EntityTypeReadModel",
                column: "ResourceName",
                unique: true,
                filter: "[ResourceName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EntityTypeReadModel_ResourceName",
                table: "EntityTypeReadModel");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceName",
                table: "EntityTypeReadModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
