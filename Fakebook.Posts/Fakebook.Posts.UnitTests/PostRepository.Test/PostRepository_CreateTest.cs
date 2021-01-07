using Xunit;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

using FakebookPosts.DataModel;
using Fakebook.Posts.Domain;


namespace Fakebook.Posts.UnitTests.PostRepository_Test
{
    public class PostRepository_CreateTest
    {
        [Fact]
        public async Task<bool> CreatePost()
        {
            //Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            Domain.Models.Post post = new Domain.Models.Post
            {
                Content = "New Content",
                CreatedAt = DateTime.Now,
                UserEmail = "person@domain.net"
            };

            Domain.Models.Post result;

            //Act
            using (var context = new FakebookPostsContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new PostsRepository(context);
                result = await repo.AddAsync(post);
            }

            //Assert
            Assert.True(result.Content == post.Content);
            Assert.True(result.UserEmail == post.UserEmail);
            Assert.True(result.CreatedAt == post.CreatedAt);
            return true;
        }
        [Fact]
        public async Task<bool> GetPost()
        {
            //Given
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            Domain.Models.Post post = new Domain.Models.Post
            {
                Content = "New Content",
                CreatedAt = DateTime.Now,
                UserEmail = "person@domain.net"
            };

            Domain.Models.Post result;
        
            //When
            using (var context = new FakebookPostsContext(options))
            {
                context.Database.EnsureCreated();
                context.Add(post);
                var repo = new PostsRepository(context);
                result = await repo.AsQueryable().FirstOrDefaultAsync(
                    p => p.UserEmail == "person@domain.net");
            }
        
            //Then
            Assert.True(result.Content == post.Content);
            Assert.True(result.UserEmail == post.UserEmail);
            Assert.True(result.CreatedAt == post.CreatedAt);
            return true;
        }
    }
}
