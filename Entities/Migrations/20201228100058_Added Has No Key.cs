using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddedHasNoKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a510dcfc-0dd7-4526-816c-44e17fbe52aa"));

            migrationBuilder.CreateTable(
                name: "UsersVotings",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VotingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_UsersVotings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersVotings_Voting_VotingId",
                        column: x => x.VotingId,
                        principalTable: "Voting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new Guid("5780868e-378d-4578-8bc7-fadc51b1bf8b"), 25L, "lamboktulus1379@gmail.com", "Lambok Tulus", "L", "Simamora", "Gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_UsersVotings_UserId",
                table: "UsersVotings",
                column: "UserId");

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
                keyValue: new Guid("5780868e-378d-4578-8bc7-fadc51b1bf8b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "Email", "FirstName", "Gender", "LastName", "Password", "RefreshToken", "RefreshTokenExpiry" },
                values: new object[] { new Guid("a510dcfc-0dd7-4526-816c-44e17fbe52aa"), 25L, "lamboktulus1379@gmail.com", "Lambok Tulus", "L", "Simamora", "Gra0307", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
