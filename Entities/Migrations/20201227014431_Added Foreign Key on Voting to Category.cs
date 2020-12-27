using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddedForeignKeyonVotingtoCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voting_Categories_CategoryId",
                table: "Voting");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cf9e732d-a2a2-45ac-b36d-02c311f5f660"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Voting",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "RefreshToken", "RefreshTokenExpiry", "Username" },
                values: new object[] { new Guid("50100dc5-c949-4fae-a983-6a0e790c9aa4"), "gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lamboktulus1379" });

            migrationBuilder.AddForeignKey(
                name: "FK_Voting_Categories_CategoryId",
                table: "Voting",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voting_Categories_CategoryId",
                table: "Voting");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("50100dc5-c949-4fae-a983-6a0e790c9aa4"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Voting",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "RefreshToken", "RefreshTokenExpiry", "Username" },
                values: new object[] { new Guid("cf9e732d-a2a2-45ac-b36d-02c311f5f660"), "gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lamboktulus1379" });

            migrationBuilder.AddForeignKey(
                name: "FK_Voting_Categories_CategoryId",
                table: "Voting",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
