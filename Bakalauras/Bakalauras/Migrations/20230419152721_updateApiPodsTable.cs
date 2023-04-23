using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakalauras.Migrations
{
    public partial class updateApiPodsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imgId",
                table: "API_Pods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgId",
                table: "API_Pods");
        }
    }
}
