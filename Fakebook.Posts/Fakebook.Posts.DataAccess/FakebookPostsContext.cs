using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Fakebook.Posts.DataAccess.Models;

namespace Fakebook.Posts.DataAccess {
    public class FakebookPostsContext : DbContext {

        public FakebookPostsContext(DbContextOptions<FakebookPostsContext> options) : base(options) { }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<Post>(entity => {

                entity.ToTable("Post", "Fakebook");

                entity.Property(e => e.UserEmail)
                      .IsRequired();

                entity.Property(e => e.Content)
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("datetimeoffset")
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
                      .HasColumnType("datetimeoffset")
                      .HasDefaultValueSql("NOW()")
                      .ValueGeneratedOnAdd();

                entity.HasOne(e => e.Post)
                      .WithMany(e => e.Comments)
                      .IsRequired()
                      .HasForeignKey(e => e.PostId)
                      .HasConstraintName("FK_Comment_Post")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity => {

                entity.ToTable("UserFollows", "Fakebook");

                entity.HasKey(e => new { e.Email, e.FolloweeEmail })
                      .HasName("PK_UserFollows");
            });
        }
    }
}
