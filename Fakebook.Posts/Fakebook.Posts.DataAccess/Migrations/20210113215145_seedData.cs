using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Posts.DataAccess.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "Post",
                columns: new[] { "Id", "Content", "CreatedAt", "Picture", "UserEmail" },
                values: new object[,]
                {
                    { 1, "Just made my first post!", new DateTimeOffset(new DateTime(2021, 1, 13, 13, 51, 44, 561, DateTimeKind.Unspecified).AddTicks(3560), new TimeSpan(0, -8, 0, 0, 0)), null, "david.barnes@revature.net" },
                    { 2, "Fakebook is really cool.", new DateTimeOffset(new DateTime(2021, 1, 13, 13, 51, 44, 563, DateTimeKind.Unspecified).AddTicks(4657), new TimeSpan(0, -8, 0, 0, 0)), null, "testaccount@gmail.com" }
                });

            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "UserFollows",
                columns: new[] { "FollowedEmail", "FollowerEmail" },
                values: new object[,]
                {
                    { "testaccount@gmail.com", "david.barnes@revature.net" },
                    { "david.barnes@revature.net", "testaccount@gmail.com" }
                });

            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "Comment",
                columns: new[] { "Id", "Content", "CreatedAt", "PostId", "UserEmail" },
                values: new object[] { 1, "Nice", new DateTimeOffset(new DateTime(2021, 1, 13, 13, 51, 44, 564, DateTimeKind.Unspecified).AddTicks(4627), new TimeSpan(0, -8, 0, 0, 0)), 1, "testaccount@gmail.com" });

            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "PostLikes",
                columns: new[] { "LikerEmail", "PostId" },
                values: new object[] { "testaccount@gmail.com", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "PostLikes",
                keyColumns: new[] { "LikerEmail", "PostId" },
                keyValues: new object[] { "testaccount@gmail.com", 1 });

            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "UserFollows",
                keyColumns: new[] { "FollowedEmail", "FollowerEmail" },
                keyValues: new object[] { "testaccount@gmail.com", "david.barnes@revature.net" });

            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "UserFollows",
                keyColumns: new[] { "FollowedEmail", "FollowerEmail" },
                keyValues: new object[] { "david.barnes@revature.net", "testaccount@gmail.com" });

            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
