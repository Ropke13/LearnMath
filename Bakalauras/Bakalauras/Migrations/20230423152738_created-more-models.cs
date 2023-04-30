using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Bakalauras.Migrations
{
    public partial class createdmoremodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestName = table.Column<string>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Time = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestComplete",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Started = table.Column<DateTime>(nullable: false),
                    Finished = table.Column<DateTime>(nullable: false),
                    TotalTasks = table.Column<int>(nullable: false),
                    TotalCorrectAnswers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestComplete", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    fk__Test = table.Column<Guid>(nullable: true),
                    fk__Task = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTasks__Task_fk__Task",
                        column: x => x.fk__Task,
                        principalTable: "_Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestTasks_Test_fk__Test",
                        column: x => x.fk__Test,
                        principalTable: "Test",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestTasks_fk__Task",
                table: "TestTasks",
                column: "fk__Task");

            migrationBuilder.CreateIndex(
                name: "IX_TestTasks_fk__Test",
                table: "TestTasks",
                column: "fk__Test");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestComplete");

            migrationBuilder.DropTable(
                name: "TestTasks");

            migrationBuilder.DropTable(
                name: "Test");
        }
    }
}
