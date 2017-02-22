using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace F2GWeb.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "contents",
                table: "Files",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ip = table.Column<string>(nullable: false),
                    Useremail = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ip);
                    table.ForeignKey(
                        name: "FK_Clients_Users_Useremail",
                        column: x => x.Useremail,
                        principalTable: "Users",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Useremail",
                table: "Clients",
                column: "Useremail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropColumn(
                name: "contents",
                table: "Files");
        }
    }
}
