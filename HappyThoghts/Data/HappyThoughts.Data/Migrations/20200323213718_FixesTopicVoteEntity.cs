using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyThoughts.Data.Migrations
{
    public partial class FixesTopicVoteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicVotes_Topics_TopicId1",
                table: "TopicVotes");

            migrationBuilder.DropIndex(
                name: "IX_TopicVotes_TopicId1",
                table: "TopicVotes");

            migrationBuilder.DropColumn(
                name: "TopicId1",
                table: "TopicVotes");

            migrationBuilder.AlterColumn<string>(
                name: "TopicId",
                table: "TopicVotes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TopicVotes_TopicId",
                table: "TopicVotes",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicVotes_Topics_TopicId",
                table: "TopicVotes",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicVotes_Topics_TopicId",
                table: "TopicVotes");

            migrationBuilder.DropIndex(
                name: "IX_TopicVotes_TopicId",
                table: "TopicVotes");

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "TopicVotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "TopicId1",
                table: "TopicVotes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicVotes_TopicId1",
                table: "TopicVotes",
                column: "TopicId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicVotes_Topics_TopicId1",
                table: "TopicVotes",
                column: "TopicId1",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
