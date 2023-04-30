using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Bakalauras.Migrations
{
    public partial class TestComplete_Added_Stuf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "fk__Test",
                table: "TestComplete",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestComplete_fk__Test",
                table: "TestComplete",
                column: "fk__Test");

            migrationBuilder.AddForeignKey(
                name: "FK_TestComplete_Test_fk__Test",
                table: "TestComplete",
                column: "fk__Test",
                principalTable: "Test",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestComplete_Test_fk__Test",
                table: "TestComplete");

            migrationBuilder.DropIndex(
                name: "IX_TestComplete_fk__Test",
                table: "TestComplete");

            migrationBuilder.DropColumn(
                name: "fk__Test",
                table: "TestComplete");
        }
    }
}
