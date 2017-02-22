using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace F2GWeb.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Requests_requestID",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_requestID",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "requestID",
                table: "Responses");

            migrationBuilder.AddColumn<string>(
                name: "Useremail",
                table: "Responses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "clientip",
                table: "Responses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_Useremail",
                table: "Responses",
                column: "Useremail");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_clientip",
                table: "Responses",
                column: "clientip");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Users_Useremail",
                table: "Responses",
                column: "Useremail",
                principalTable: "Users",
                principalColumn: "email",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Clients_clientip",
                table: "Responses",
                column: "clientip",
                principalTable: "Clients",
                principalColumn: "ip",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Users_Useremail",
                table: "Responses");

            migrationBuilder.DropForeignKey(
                name: "FK_Responses_Clients_clientip",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_Useremail",
                table: "Responses");

            migrationBuilder.DropIndex(
                name: "IX_Responses_clientip",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "Useremail",
                table: "Responses");

            migrationBuilder.DropColumn(
                name: "clientip",
                table: "Responses");

            migrationBuilder.AddColumn<int>(
                name: "requestID",
                table: "Responses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responses_requestID",
                table: "Responses",
                column: "requestID");

            migrationBuilder.AddForeignKey(
                name: "FK_Responses_Requests_requestID",
                table: "Responses",
                column: "requestID",
                principalTable: "Requests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
