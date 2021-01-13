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
        public async void CreateComment()
        {
            //Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;
          
            var dataAccessPost = new DataAccess.Models.Post()
            {
                Id = 1,
                UserEmail = "person@domain.net",
                Content = "post content",
                CreatedAt = DateTime.Now
            };

            var domainModelPost = new Domain.Models.Post("person@domain.net", "post content")
            {
                Id = 1,
                CreatedAt = DateTime.Now
            };

            Domain.Models.Comment comment = new Domain.Models.Comment("person@domain.net", "content")
            {
                Content = "New Content",
                CreatedAt = DateTime.Now
            };

            Domain.Models.Comment result;

            //Act
            using (var context = new FakebookPostsContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new PostsRepository(context);
                result = await repo.AddCommentAsync(comment);
            }

            //Assert
            Assert.True(result.Content == comment.Content);
            Assert.True(result.UserEmail == comment.UserEmail);
            Assert.True(result.CreatedAt == comment.CreatedAt);
        }
        [Fact]
        public async void CreatePost()
        {
            //Arrange
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
