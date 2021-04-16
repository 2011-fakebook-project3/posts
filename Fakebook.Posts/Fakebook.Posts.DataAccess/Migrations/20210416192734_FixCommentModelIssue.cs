using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Posts.DataAccess.Migrations
{
    public partial class FixCommentModelIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post",
                schema: "Fakebook",
                table: "Comment");

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 4, 16, 15, 27, 34, 251, DateTimeKind.Unspecified).AddTicks(7877), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 4, 16, 15, 27, 34, 247, DateTimeKind.Unspecified).AddTicks(7124), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 4, 16, 15, 27, 34, 250, DateTimeKind.Unspecified).AddTicks(5678), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                schema: "Fakebook",
                table: "Comment",
                column: "PostId",
                principalSchema: "Fakebook",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                schema: "Fakebook",
                table: "Comment");

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 3, 30, 14, 1, 47, 100, DateTimeKind.Unspecified).AddTicks(5441), new TimeSpan(0, -6, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 3, 30, 14, 1, 47, 95, DateTimeKind.Unspecified).AddTicks(9302), new TimeSpan(0, -6, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 3, 30, 14, 1, 47, 98, DateTimeKind.Unspecified).AddTicks(6979), new TimeSpan(0, -6, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post",
                schema: "Fakebook",
                table: "Comment",
                column: "PostId",
                principalSchema: "Fakebook",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
