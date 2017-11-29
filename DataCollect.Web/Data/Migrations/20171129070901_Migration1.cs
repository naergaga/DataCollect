using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataCollect.Web.Data.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventBook",
                table: "EventBook");

            migrationBuilder.DropIndex(
                name: "IX_EventBook_EventId",
                table: "EventBook");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EventBook");

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Event",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventBook",
                table: "EventBook",
                columns: new[] { "EventId", "BookId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventBook",
                table: "EventBook");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Event");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EventBook",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventBook",
                table: "EventBook",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EventBook_EventId",
                table: "EventBook",
                column: "EventId");
        }
    }
}
