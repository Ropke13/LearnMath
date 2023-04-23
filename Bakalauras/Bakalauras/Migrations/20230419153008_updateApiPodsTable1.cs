using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bakalauras.Migrations
{
    public partial class updateApiPodsTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_API_Pods__Task_fk__Task",
                table: "API_Pods");

            migrationBuilder.AlterColumn<Guid>(
                name: "fk__Task",
                table: "API_Pods",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSaved",
                table: "API_Pods",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_API_Pods__Task_fk__Task",
                table: "API_Pods",
                column: "fk__Task",
                principalTable: "_Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_API_Pods__Task_fk__Task",
                table: "API_Pods");

            migrationBuilder.AlterColumn<Guid>(
                name: "fk__Task",
                table: "API_Pods",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsSaved",
                table: "API_Pods",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_API_Pods__Task_fk__Task",
                table: "API_Pods",
                column: "fk__Task",
                principalTable: "_Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
