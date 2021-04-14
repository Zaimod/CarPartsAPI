using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarParts.Migrations
{
    public partial class Logging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03694a8e-de00-4352-8c0a-52fdd2a93e2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f70fbaa0-ef2b-4dc5-acb1-b3588d8b0e4d");

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Type = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Action = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Message = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.LogId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f5943029-d50e-41cd-a0ab-f3a283a09197", "79a4011e-8ce7-44e6-a8c0-2ac07426b2dc", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bbc770fa-001b-4b35-a74b-1350285ed46e", "ab13b053-5c3e-4f9f-b6f2-2a11a0beac04", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbc770fa-001b-4b35-a74b-1350285ed46e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5943029-d50e-41cd-a0ab-f3a283a09197");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "03694a8e-de00-4352-8c0a-52fdd2a93e2c", "ffc5a89d-ac55-4dcb-b5bd-386f7ff74e04", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f70fbaa0-ef2b-4dc5-acb1-b3588d8b0e4d", "26ce02ac-6923-4a6b-9efd-e805f4da40ce", "Administrator", "ADMINISTRATOR" });
        }
    }
}
