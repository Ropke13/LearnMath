using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakalauras.Migrations
{
    public partial class oneMoreFixWithAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSaved",
                table: "API_Pods");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "API_Pods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "API_Pods");

            migrationBuilder.AddColumn<bool>(
                name: "IsSaved",
                table: "API_Pods",
                type: "bit",
                nullable: true);
        }
    }
}
