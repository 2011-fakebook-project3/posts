using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fakebook.Posts.DataAccess.Migrations
{
    public partial class ChangedNowtoCurrentTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "Fakebook",
                table: "Post",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "Fakebook",
                table: "Comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 4, 18, 23, 10, 17, 355, DateTimeKind.Unspecified).AddTicks(5855), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 4, 18, 23, 10, 17, 352, DateTimeKind.Unspecified).AddTicks(3923), new TimeSpan(0, -4, 0, 0, 0)));

            migrationBuilder.UpdateData(
                schema: "Fakebook",
                table: "Post",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2021, 4, 18, 23, 10, 17, 354, DateTimeKind.Unspecified).AddTicks(4041), new TimeSpan(0, -4, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "Fakebook",
                table: "Post",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "Fakebook",
                table: "Comment",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

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
        }
    }
}
