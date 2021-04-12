using Fakebook.Posts.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fakebook.Posts.DataAccess
{
    public class FakebookPostsContext : DbContext
    {
        public FakebookPostsContext(DbContextOptions<FakebookPostsContext> options) : base(options)
        {
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<PostLike> PostLikes { get; set; }
        public virtual DbSet<CommentLike> CommentLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post", "Fakebook");

                entity.Property(e => e.UserEmail)
                      .IsRequired();

                entity.Property(e => e.Content)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("timestamp with time zone")
                      .HasDefaultValueSql("NOW()")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Picture)
                      .IsRequired(false);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment", "Fakebook");

                entity.Property(e => e.UserEmail)
                      .IsRequired();

                entity.Property(e => e.Content)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("timestamp with time zone")
                      .HasDefaultValueSql("NOW()")
                      .ValueGeneratedOnAdd();

                entity.HasOne(e => e.Post)
                      .WithMany(e => e.Comments)
                      .IsRequired()
                      .HasForeignKey(e => e.PostId)
                      .HasConstraintName("FK_Comment_Post")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.ToTable("UserFollows", "Fakebook");

                entity.HasKey(e => new { e.FollowerEmail, e.FollowedEmail })
                      .HasName("PK_UserFollows");
            });

            modelBuilder.Entity<PostLike>(entity =>
            {
                entity.ToTable("PostLikes", "Fakebook");

                entity.HasKey(e => new { e.LikerEmail, e.PostId })
                      .HasName("PK_PostLikes");

                entity.HasOne(e => e.Post)
                      .WithMany(e => e.PostLikes)
                      .HasForeignKey(e => e.PostId)
                      .HasConstraintName("FK_Like_Post")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CommentLike>(entity =>
            {
                entity.ToTable("CommentLikes", "Fakebook");

                entity.HasKey(e => new { e.LikerEmail, e.CommentId })
                      .HasName("PK_CommentLikes");

                entity.HasOne(e => e.Comment)
                      .WithMany(e => e.CommentLikes)
                      .HasForeignKey(e => e.CommentId)
                      .HasConstraintName("FK_Like_Comment")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Post>()
                .HasData(new Post[]
                {
                    new Post
                    {
                        Id = 1,
                        UserEmail = "john.werner@revature.net",
                        Content = "Just made my first post!",
                        CreatedAt = new DateTimeOffset(new DateTime(2021, 2, 2, 2, 2, 2))
                    },
                    new Post
                    {
                        Id = 2,
                        UserEmail = "testaccount@gmail.com",
                        Content = "Fakebook is really cool.",
                        CreatedAt = new DateTimeOffset(new DateTime(2021, 3, 3, 3, 3, 3))
                    }
                });

            modelBuilder.Entity<Comment>()
                .HasData(new Comment[] {
                    new Comment {
                        Id = 1,
                        UserEmail = "testaccount@gmail.com",
                        Content = "Nice",
                        CreatedAt = new DateTimeOffset(new DateTime(2021, 3, 3, 3, 2, 2)),
                        PostId = 1
                    }
                });

            modelBuilder.Entity<Follow>()
                .HasData(new Follow[]
                {
                    new Follow
                    {
                        FollowerEmail = "john.werner@revature.net",
                        FollowedEmail = "testaccount@gmail.com"
                    },
                    new Follow
                    {
                        FollowerEmail = "testaccount@gmail.com",
                        FollowedEmail = "john.werner@revature.net"
                    }
                });

            modelBuilder.Entity<PostLike>()
                .HasData(new PostLike[]
                {
                    new PostLike
                    {
                        PostId = 1,
                        LikerEmail = "testaccount@gmail.com"
                    }
                });
        }
    }
}
