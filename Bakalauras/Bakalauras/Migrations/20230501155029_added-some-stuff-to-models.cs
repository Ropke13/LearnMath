using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakalauras.Migrations
{
    public partial class addedsomestufftomodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Explaining",
                table: "_Task");

            migrationBuilder.AddColumn<int>(
                name: "Attempt",
                table: "TestComplete",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fk__User",
                table: "TestComplete",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fk__User",
                table: "Test",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestComplete_fk__User",
                table: "TestComplete",
                column: "fk__User");

            migrationBuilder.CreateIndex(
                name: "IX_Test_fk__User",
                table: "Test",
                column: "fk__User");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_AspNetUsers_fk__User",
                table: "Test",
                column: "fk__User",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestComplete_AspNetUsers_fk__User",
                table: "TestComplete",
                column: "fk__User",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_AspNetUsers_fk__User",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_TestComplete_AspNetUsers_fk__User",
                table: "TestComplete");

            migrationBuilder.DropIndex(
                name: "IX_TestComplete_fk__User",
                table: "TestComplete");

            migrationBuilder.DropIndex(
                name: "IX_Test_fk__User",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Attempt",
                table: "TestComplete");

            migrationBuilder.DropColumn(
                name: "fk__User",
                table: "TestComplete");

            migrationBuilder.DropColumn(
                name: "fk__User",
                table: "Test");

            migrationBuilder.AddColumn<string>(
                name: "Explaining",
                table: "_Task",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
