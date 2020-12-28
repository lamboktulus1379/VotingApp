using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddedBackUsersVotingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVoting");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c674e5b3-afc4-40cd-b24b-cefc1de56aa1"));

            migrationBuilder.CreateTable(
                name: "UsersVotings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VotingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersVotings", x => new { x.UserId, x.VotingId });
                    table.ForeignKey(
                        name: "FK_UsersVotings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersVotings_Voting_VotingId",
                        column: x => x.VotingId,
                        principalTable: "Voting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new Guid("b875c885-4601-464a-b129-f1177400ae8b"), 25L, "lamboktulus1379@gmail.com", "Lambok Tulus", "L", "Simamora", "Gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_UsersVotings_VotingId",
                table: "UsersVotings",
                column: "VotingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersVotings");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b875c885-4601-464a-b129-f1177400ae8b"));

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
                values: new object[] { new Guid("c674e5b3-afc4-40cd-b24b-cefc1de56aa1"), 25L, "lamboktulus1379@gmail.com", "Lambok Tulus", "L", "Simamora", "Gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_UserVoting_VotingsId",
                table: "UserVoting",
                column: "VotingsId");
        }
    }
}
