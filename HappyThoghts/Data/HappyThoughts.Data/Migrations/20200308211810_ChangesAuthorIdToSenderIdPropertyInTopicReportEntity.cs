using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyThoughts.Data.Migrations
{
    public partial class ChangesAuthorIdToSenderIdPropertyInTopicReportEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicReports_AspNetUsers_AuthorId",
                table: "TopicReports");

            migrationBuilder.DropIndex(
                name: "IX_TopicReports_AuthorId",
                table: "TopicReports");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "TopicReports");

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "TopicReports",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TopicReports_SenderId",
                table: "TopicReports",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicReports_AspNetUsers_SenderId",
                table: "TopicReports",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicReports_AspNetUsers_SenderId",
                table: "TopicReports");

            migrationBuilder.DropIndex(
                name: "IX_TopicReports_SenderId",
                table: "TopicReports");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "TopicReports");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "TopicReports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TopicReports_AuthorId",
                table: "TopicReports",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicReports_AspNetUsers_AuthorId",
                table: "TopicReports",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
