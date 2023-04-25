using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakalauras.Migrations
{
    public partial class missing_adds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestCompletedTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: true),
                    SelectedAnswer = table.Column<string>(nullable: true),
                    IsAnsweredCorrectly = table.Column<bool>(nullable: false),
                    fk__TestComplete = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCompletedTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCompletedTask_TestComplete_fk__TestComplete",
                        column: x => x.fk__TestComplete,
                        principalTable: "TestComplete",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestCompletedTask_fk__TestComplete",
                table: "TestCompletedTask",
                column: "fk__TestComplete");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestCompletedTask");
        }
    }
}
