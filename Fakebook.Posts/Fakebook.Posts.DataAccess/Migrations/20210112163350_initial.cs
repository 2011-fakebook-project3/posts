using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fakebook.Posts.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Fakebook");

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "Fakebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Picture = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFollows",
                schema: "Fakebook",
                columns: table => new
                {
                    FollowerEmail = table.Column<string>(type: "text", nullable: false),
                    FollowedEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollows", x => new { x.FollowerEmail, x.FollowedEmail });
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "Fakebook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserEmail = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Post",
                        column: x => x.PostId,
                        principalSchema: "Fakebook",
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostLikes",
                schema: "Fakebook",
                columns: table => new
                {
                    LikerEmail = table.Column<string>(type: "text", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikes", x => new { x.LikerEmail, x.PostId });
                    table.ForeignKey(
                        name: "FK_Like_Post",
                        column: x => x.PostId,
                        principalSchema: "Fakebook",
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentLikes",
                schema: "Fakebook",
                columns: table => new
                {
                    LikerEmail = table.Column<string>(type: "text", nullable: false),
                    CommentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentLikes", x => new { x.LikerEmail, x.CommentId });
                    table.ForeignKey(
                        name: "FK_Like_Comment",
                        column: x => x.CommentId,
                        principalSchema: "Fakebook",
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                schema: "Fakebook",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_CommentId",
                schema: "Fakebook",
                table: "CommentLikes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostId",
                schema: "Fakebook",
                table: "PostLikes",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentLikes",
                schema: "Fakebook");

            migrationBuilder.DropTable(
                name: "PostLikes",
                schema: "Fakebook");

            migrationBuilder.DropTable(
                name: "UserFollows",
                schema: "Fakebook");

            migrationBuilder.DropTable(
                name: "Comment",
                schema: "Fakebook");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "Fakebook");
        }
    }
}
