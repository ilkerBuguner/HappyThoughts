using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HappyThoughts.Data.Migrations
{
    public partial class FixesSomePropertyNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Topics_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubComments_Topics_PostId",
                table: "SubComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicsCategories_Topics_PostId",
                table: "TopicsCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopicsCategories",
                table: "TopicsCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubComments_PostId",
                table: "SubComments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "TopicsCategories");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "TopicId",
                table: "TopicsCategories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Topics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopicId",
                table: "SubComments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TopicId",
                table: "Comments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopicsCategories",
                table: "TopicsCategories",
                columns: new[] { "TopicId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubComments_TopicId",
                table: "SubComments",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TopicId",
                table: "Comments",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Topics_TopicId",
                table: "Comments",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubComments_Topics_TopicId",
                table: "SubComments",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicsCategories_Topics_TopicId",
                table: "TopicsCategories",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Topics_TopicId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubComments_Topics_TopicId",
                table: "SubComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicsCategories_Topics_TopicId",
                table: "TopicsCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopicsCategories",
                table: "TopicsCategories");

            migrationBuilder.DropIndex(
                name: "IX_SubComments_TopicId",
                table: "SubComments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TopicId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "TopicsCategories");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "TopicsCategories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Topics",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "SubComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopicsCategories",
                table: "TopicsCategories",
                columns: new[] { "PostId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubComments_PostId",
                table: "SubComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Topics_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubComments_Topics_PostId",
                table: "SubComments",
                column: "PostId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicsCategories_Topics_PostId",
                table: "TopicsCategories",
                column: "PostId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
