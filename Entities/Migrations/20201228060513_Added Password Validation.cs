using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddedPasswordValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b799b717-a25e-4256-b923-c6e6a7b2820a"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new Guid("c41e73b7-b550-4e9c-8778-eabc3a04017d"), 25L, "lamboktulus1379@gmail.com", "Lambok Tulus", "L", "Simamora", "Gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c41e73b7-b550-4e9c-8778-eabc3a04017d"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new Guid("b799b717-a25e-4256-b923-c6e6a7b2820a"), 25L, "lamboktulus1379@gmail.com", "Lambok Tulus", "L", "Simamora", "Gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
