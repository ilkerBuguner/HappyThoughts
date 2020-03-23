using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyThoughts.Data.Migrations
{
    public partial class AddsTopicVoteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicVotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    TopicId = table.Column<int>(nullable: false),
                    TopicId1 = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicVotes_Topics_TopicId1",
                        column: x => x.TopicId1,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicVotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicVotes_TopicId1",
                table: "TopicVotes",
                column: "TopicId1");

            migrationBuilder.CreateIndex(
                name: "IX_TopicVotes_UserId",
                table: "TopicVotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicVotes");
        }
    }
}
