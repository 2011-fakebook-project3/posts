﻿// <auto-generated />
using System;
using Fakebook.Posts.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fakebook.Posts.DataAccess.Migrations
{
    [DbContext(typeof(FakebookPostsContext))]
    [Migration("20210330200147_NewSeedData")]
    partial class NewSeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comment", "Fakebook");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Nice",
                            CreatedAt = new DateTimeOffset(new DateTime(2021, 3, 30, 14, 1, 47, 100, DateTimeKind.Unspecified).AddTicks(5441), new TimeSpan(0, -6, 0, 0, 0)),
                            PostId = 1,
                            UserEmail = "testaccount@gmail.com"
                        });
                });

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.CommentLike", b =>
                {
                    b.Property<string>("LikerEmail")
                        .HasColumnType("text");

                    b.Property<int>("CommentId")
                        .HasColumnType("integer");

                    b.HasKey("LikerEmail", "CommentId")
                        .HasName("PK_CommentLikes");

                    b.HasIndex("CommentId");

                    b.ToTable("CommentLikes", "Fakebook");
                });

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.Follow", b =>
                {
                    b.Property<string>("FollowerEmail")
                        .HasColumnType("text");

                    b.Property<string>("FollowedEmail")
                        .HasColumnType("text");

                    b.HasKey("FollowerEmail", "FollowedEmail")
                        .HasName("PK_UserFollows");

                    b.ToTable("UserFollows", "Fakebook");

                    b.HasData(
                        new
                        {
                            FollowerEmail = "john.werner@revature.net",
                            FollowedEmail = "testaccount@gmail.com"
                        },
                        new
                        {
                            FollowerEmail = "testaccount@gmail.com",
                            FollowedEmail = "john.werner@revature.net"
                        });
                });

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Picture")
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Post", "Fakebook");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Just made my first post!",
                            CreatedAt = new DateTimeOffset(new DateTime(2021, 3, 30, 14, 1, 47, 95, DateTimeKind.Unspecified).AddTicks(9302), new TimeSpan(0, -6, 0, 0, 0)),
                            UserEmail = "john.werner@revature.net"
                        },
                        new
                        {
                            Id = 2,
                            Content = "Fakebook is really cool.",
                            CreatedAt = new DateTimeOffset(new DateTime(2021, 3, 30, 14, 1, 47, 98, DateTimeKind.Unspecified).AddTicks(6979), new TimeSpan(0, -6, 0, 0, 0)),
                            UserEmail = "testaccount@gmail.com"
                        });
                });

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.PostLike", b =>
                {
                    b.Property<string>("LikerEmail")
                        .HasColumnType("text");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.HasKey("LikerEmail", "PostId")
                        .HasName("PK_PostLikes");

                    b.HasIndex("PostId");

                    b.ToTable("PostLikes", "Fakebook");

                    b.HasData(
                        new
                        {
                            LikerEmail = "testaccount@gmail.com",
                            PostId = 1
                        });
                });

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.Comment", b =>
                {
                    b.HasOne("Fakebook.Posts.DataAccess.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_Comment_Post")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.CommentLike", b =>
                {
                    b.HasOne("Fakebook.Posts.DataAccess.Models.Comment", "Comment")
                        .WithMany("CommentLikes")
                        .HasForeignKey("CommentId")
                        .HasConstraintName("FK_Like_Comment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.PostLike", b =>
                {
                    b.HasOne("Fakebook.Posts.DataAccess.Models.Post", "Post")
                        .WithMany("PostLikes")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_Like_Post")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.Comment", b =>
                {
                    b.Navigation("CommentLikes");
                });

            modelBuilder.Entity("Fakebook.Posts.DataAccess.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostLikes");
                });
#pragma warning restore 612, 618
        }
    }
}
