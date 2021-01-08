using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Fakebook.Posts.DataAccess.Models;

namespace Fakebook.Posts.DataAccess {
    public class FakebookPostsContext : DbContext {

        public FakebookPostsContext(DbContextOptions<FakebookPostsContext> options) : base(options) { }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<PostLike> PostLikes { get; set; }
        public virtual DbSet<CommentLike> CommentLikes { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Post>(entity => {

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

            modelBuilder.Entity<Comment>(entity => {

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

            modelBuilder.Entity<Follow>(entity => {

                entity.ToTable("UserFollows", "Fakebook");

                entity.HasKey(e => new { e.FollowerEmail, e.FollowedEmail })
                      .HasName("PK_UserFollows");
            });

            modelBuilder.Entity<PostLike>(entity => {

                  entity.ToTable("PostLikes", "Fakebook");

                  entity.HasKey(e => new { e.LikerEmail, e.PostId })
                        .HasName("PK_PostLikes");
                  
                  entity.HasOne(e => e.Post)
                        .WithMany(e  => e.PostLikes)
                        .HasForeignKey(e => e.PostId)
                        .HasConstraintName("FK_Like_Post")
                        .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CommentLike>(entity => {

                  entity.ToTable("PostLikes", "Fakebook");

                  entity.HasKey(e => new { e.LikerEmail, e.CommentId })
                        .HasName("PK_PostLikes");
                  
                  entity.HasOne(e => e.Comment)
                        .WithMany(e  => e.CommentLikes)
                        .HasForeignKey(e => e.CommentId)
                        .HasConstraintName("FK_Like_Comment")
                        .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
