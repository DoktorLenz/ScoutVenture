using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoutVenture.PostgresAdapter.Migrations
{
    /// <inheritdoc />
    public partial class LinkMembersAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMemberLinks",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMemberLinks", x => new { x.UserId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_UserMemberLinks_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMemberLinks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMemberLinks_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberLinks_CreatedById",
                table: "UserMemberLinks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserMemberLinks_MemberId",
                table: "UserMemberLinks",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMemberLinks");
        }
    }
}
