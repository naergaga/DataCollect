using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataCollect.Web.Data.Migrations
{
    public partial class RowUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Row",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Row_UserId",
                table: "Row",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Row_AspNetUsers_UserId",
                table: "Row",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Row_AspNetUsers_UserId",
                table: "Row");

            migrationBuilder.DropIndex(
                name: "IX_Row_UserId",
                table: "Row");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Row");
        }
    }
}
