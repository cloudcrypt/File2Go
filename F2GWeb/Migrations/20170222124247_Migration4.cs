using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace F2GWeb.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_useremail",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "useremail",
                table: "Files",
                newName: "Useremail");

            migrationBuilder.RenameIndex(
                name: "IX_Files_useremail",
                table: "Files",
                newName: "IX_Files_Useremail");

            migrationBuilder.AddColumn<int>(
                name: "responseID",
                table: "Files",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_responseID",
                table: "Files",
                column: "responseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_Useremail",
                table: "Files",
                column: "Useremail",
                principalTable: "Users",
                principalColumn: "email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Responses_responseID",
                table: "Files",
                column: "responseID",
                principalTable: "Responses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_Useremail",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Responses_responseID",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_responseID",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "responseID",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "Useremail",
                table: "Files",
                newName: "useremail");

            migrationBuilder.RenameIndex(
                name: "IX_Files_Useremail",
                table: "Files",
                newName: "IX_Files_useremail");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_useremail",
                table: "Files",
                column: "useremail",
                principalTable: "Users",
                principalColumn: "email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
