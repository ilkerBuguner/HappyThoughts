using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyThoughts.Data.Migrations
{
    public partial class AddsRelationsToUserReportEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReports_Topics_ReportedUserId",
                table: "UserReports");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReports_AspNetUsers_ReportedUserId",
                table: "UserReports",
                column: "ReportedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReports_AspNetUsers_ReportedUserId",
                table: "UserReports");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReports_Topics_ReportedUserId",
                table: "UserReports",
                column: "ReportedUserId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
