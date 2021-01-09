using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;


namespace Fakebook.Posts.UnitTests.PostRepository.Test
{
    public class PostRepository_CreateTest
    {
        [Fact]
        public async Task CreatePost() {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            Domain.Models.Post post = new Domain.Models.Post("person@domain.net", "content")
            {
                Content = "New Content",
                CreatedAt = DateTime.Now
            };

            Domain.Models.Post result;

            // Act
            using (var context = new FakebookPostsContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new PostsRepository(context);
                result = await repo.AddAsync(post);
            }

            // Assert
            Assert.True(result.Content == post.Content);
            Assert.True(result.UserEmail == post.UserEmail);
            Assert.True(result.CreatedAt == post.CreatedAt);
        }

        [Fact]
        public void GetPost_GetId_EqualsSetValue() {
            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            DataAccess.Models.Post insertedPost = new DataAccess.Models.Post() {
                Id = 1,
                UserEmail = "person@domain.net",
                Content = "New Content",
                CreatedAt = DateTime.Now
            };

            using var context = new FakebookPostsContext(options);
            context.Database.EnsureCreated();
            context.Posts.Add(insertedPost);

            var repo = new PostsRepository(context);

            // Act
            var result = repo.AsQueryable().FirstOrDefault(
                p => p.Id == 1);

            // Assert
            Assert.IsAssignableFrom<Domain.Models.Post>(result);
            Assert.Equal("New Content", result.Content);
            Assert.Equal("person@domain.net", result.UserEmail);
        }
    }
}
