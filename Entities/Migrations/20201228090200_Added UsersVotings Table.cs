using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddedUsersVotingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c41e73b7-b550-4e9c-8778-eabc3a04017d"));

            migrationBuilder.CreateTable(
                name: "UserVoting",
                columns: table => new
                {
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VotingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVoting", x => new { x.UsersId, x.VotingsId });
                    table.ForeignKey(
                        name: "FK_UserVoting_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVoting_Voting_VotingsId",
                        column: x => x.VotingsId,
                        principalTable: "Voting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new Guid("a510dcfc-0dd7-4526-816c-44e17fbe52aa"), 25L, "lamboktulus1379@gmail.com", "Lambok Tulus", "L", "Simamora", "Gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_UserVoting_VotingsId",
                table: "UserVoting",
                column: "VotingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVoting");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a510dcfc-0dd7-4526-816c-44e17fbe52aa"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new Guid("c41e73b7-b550-4e9c-8778-eabc3a04017d"), 25L, "lamboktulus1379@gmail.com", "Lambok Tulus", "L", "Simamora", "Gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
