using Microsoft.EntityFrameworkCore.Migrations;

namespace Wanaheim.Migrations
{
    public partial class RemovedIsNewFromItemTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Items",
                nullable: false,
                defaultValue: false);
        }
    }
}
