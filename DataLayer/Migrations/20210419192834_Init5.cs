using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_IsAnswerForId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_IsAnswerForId",
                table: "Messages");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModify",
                table: "Messages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Reciever",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModify",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Reciever",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IsAnswerForId",
                table: "Messages",
                column: "IsAnswerForId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_IsAnswerForId",
                table: "Messages",
                column: "IsAnswerForId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
