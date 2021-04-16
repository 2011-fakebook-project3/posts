using System;
using System.Linq;
using Fakebook.Posts.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fakebook.Posts.DataAccess
{
    public class FakebookPostsContext : DbContext
    {

        public FakebookPostsContext(DbContextOptions<FakebookPostsContext> options) : base(options) { }

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
                        CreatedAt = DateTimeOffset.Now
                    },
                    new Post
                    {
                        Id = 2,
                        UserEmail = "testaccount@gmail.com",
                        Content = "Fakebook is really cool.",
                        CreatedAt = DateTimeOffset.Now
                    }
                });

            modelBuilder.Entity<Comment>()
                .HasData(new Comment[] {
                    new Comment {
                        Id = 1,
                        UserEmail = "testaccount@gmail.com",
                        Content = "Nice",
                        CreatedAt = DateTimeOffset.Now,
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

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
                // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
                // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
                // use the DateTimeOffsetToBinaryConverter
                // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
                // This only supports millisecond precision, but should be sufficient for most use cases.
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)
                                                                                || p.PropertyType == typeof(DateTimeOffset?));
                    foreach (var property in properties)
                    {
                        modelBuilder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}
