using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Posts.DataAccess.Migrations
{
    public partial class NewSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreatedAt", "UserEmail" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 3, 30, 14, 1, 47, 95, DateTimeKind.Unspecified).AddTicks(9302), new TimeSpan(0, -6, 0, 0, 0)), "john.werner@revature.net" });

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 3, 30, 14, 1, 47, 98, DateTimeKind.Unspecified).AddTicks(6979), new TimeSpan(0, -6, 0, 0, 0)));

            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "UserFollows",
                columns: new[] { "FollowedEmail", "FollowerEmail" },
                values: new object[,]
                {
                    { "testaccount@gmail.com", "john.werner@revature.net" },
                    { "john.werner@revature.net", "testaccount@gmail.com" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "UserFollows",
                keyColumns: new[] { "FollowedEmail", "FollowerEmail" },
                keyValues: new object[] { "testaccount@gmail.com", "john.werner@revature.net" });

            migrationBuilder.DeleteData(
                schema: "Fakebook",
                table: "UserFollows",
                keyColumns: new[] { "FollowedEmail", "FollowerEmail" },
                keyValues: new object[] { "john.werner@revature.net", "testaccount@gmail.com" });

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 1, 13, 13, 51, 44, 564, DateTimeKind.Unspecified).AddTicks(4627), new TimeSpan(0, -8, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UserEmail" },
                values: new object[] { new DateTimeOffset(new DateTime(2021, 1, 13, 13, 51, 44, 561, DateTimeKind.Unspecified).AddTicks(3560), new TimeSpan(0, -8, 0, 0, 0)), "david.barnes@revature.net" });

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 1, 13, 13, 51, 44, 563, DateTimeKind.Unspecified).AddTicks(4657), new TimeSpan(0, -8, 0, 0, 0)));

            migrationBuilder.InsertData(
                schema: "Fakebook",
                table: "UserFollows",
                columns: new[] { "FollowedEmail", "FollowerEmail" },
                values: new object[,]
                {
                    { "testaccount@gmail.com", "david.barnes@revature.net" },
                    { "david.barnes@revature.net", "testaccount@gmail.com" }
                });
        }
    }
}
