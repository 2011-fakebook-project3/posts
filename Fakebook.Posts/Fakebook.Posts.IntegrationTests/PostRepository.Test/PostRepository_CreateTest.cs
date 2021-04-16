using System.Linq;
using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Posts.IntegrationTests.PostRepository.Test
{
    public class PostRepository_CreateTest
    {
        [Fact]
        public async Task CreateComment()
        {
            //Arrange
            using SqliteConnection connection = new("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            DataAccess.Models.Post dataAccessPost = new()
            {
                Id = 3,
                UserEmail = "person@domain.net",
                Content = "post content",
                CreatedAt = DateTime.Now
            };

            Domain.Models.Post domainModelPost = new("person@domain.net", "post content")
            {
                Id = 3,
                CreatedAt = DateTime.Now
            };

            Domain.Models.Comment comment = new("person@domain.net", "content", 3)
            {
                Id = 2,
                Post = domainModelPost,
                CreatedAt = DateTime.Now
            };

            Domain.Models.Comment result;

            //Act
            using (FakebookPostsContext context = new(options))
            {
                context.Database.EnsureCreated();
                PostsRepository repo = new(context);
                context.Posts.Add(dataAccessPost);
                context.SaveChanges();
                result = await repo.AddCommentAsync(comment);
            }

            //Assert
            Assert.True(result.Content == comment.Content);
            Assert.True(result.UserEmail == comment.UserEmail);
            Assert.True(result.CreatedAt == comment.CreatedAt);
        }

        [Fact]
        public async Task CreatePost()
        {
            //Arrange
            using SqliteConnection connection = new("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            Domain.Models.Post post = new("person@domain.net", "content")
            {
                Content = "New Content",
                CreatedAt = DateTime.Now
            };

            Domain.Models.Post result;

            // Act
            using (FakebookPostsContext context = new(options))
            {
                context.Database.EnsureCreated();
                PostsRepository repo = new(context);
                result = await repo.AddAsync(post);
            }

            // Assert
            Assert.True(result.Content == post.Content);
            Assert.True(result.UserEmail == post.UserEmail);
            Assert.True(result.CreatedAt == post.CreatedAt);
        }

        [Fact]
        public async Task GetPost_GetId_EqualsSetValue()
        {
            // Arrange
            int postId = 3;
            using SqliteConnection connection = new("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            DataAccess.Models.Post insertedPost = new()
            {
                Id = postId,
                UserEmail = "person@domain.net",
                Content = "New Content",
                CreatedAt = DateTime.Now
            };

            using FakebookPostsContext context = new(options);
            context.Database.EnsureCreated();
            context.Posts.Add(insertedPost);
            context.SaveChanges();

            PostsRepository repo = new(context);

            // Act
            var result = await repo.GetAsync(postId);

            // Assert
            Assert.IsAssignableFrom<Domain.Models.Post>(result);
            Assert.Equal("New Content", result.Content);
            Assert.Equal("person@domain.net", result.UserEmail);
        }
    }
}
