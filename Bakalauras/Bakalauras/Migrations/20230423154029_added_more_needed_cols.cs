using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakalauras.Migrations
{
    public partial class added_more_needed_cols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestCode",
                table: "Test",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestCode",
                table: "Test");
        }
    }
}
