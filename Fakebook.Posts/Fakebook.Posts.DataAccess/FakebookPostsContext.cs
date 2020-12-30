using Microsoft.EntityFrameworkCore;

namespace FakebookPosts.DataModel
{
    /// <summary>
    /// Code first EF core code intended for Postgres 
    /// uses default picture path of ""
    /// deleting a post delete all comment on it
    /// </summary>
    public class FakebookPostsContext : DbContext 
    {
        public FakebookPostsContext(DbContextOptions<FakebookPostsContext> options) : base(options) {}
        public virtual DbSet<Post> Posts {get; set;}
        public virtual DbSet<Comment> Comments {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity => 
            {
                entity.ToTable("Post");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
                entity.Property(e => e.Picture).HasDefaultValue("").ValueGeneratedOnAddOrUpdate();
            });
            modelBuilder.Entity<Comment>(entity => 
            {
                entity.ToTable("Comment");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
                entity.HasOne(d => d.Post).WithMany(p => p.Comments).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}