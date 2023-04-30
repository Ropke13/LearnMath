using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakalauras.Migrations
{
    public partial class added_User_totaskmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fk__User",
                table: "_Task",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX__Task_fk__User",
                table: "_Task",
                column: "fk__User");

            migrationBuilder.AddForeignKey(
                name: "FK__Task_AspNetUsers_fk__User",
                table: "_Task",
                column: "fk__User",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Task_AspNetUsers_fk__User",
                table: "_Task");

            migrationBuilder.DropIndex(
                name: "IX__Task_fk__User",
                table: "_Task");

            migrationBuilder.DropColumn(
                name: "fk__User",
                table: "_Task");
        }
    }
}
