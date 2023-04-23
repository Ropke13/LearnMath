using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakalauras.Migrations
{
    public partial class moreTaskInputs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "API_Pods",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "_Task",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "_Task",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "_Task");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "_Task");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "API_Pods",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
