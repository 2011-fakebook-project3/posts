using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Fakebook.Posts.UnitTests.PostRepository.Test
{
    public class PostRepository_CreateTest
    {
        [Fact]
        public async Task<bool> CreateComment()
        {
            //Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

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
            return true;
        }
        [Fact]
        public async Task<bool> CreatePost()
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
        public async Task<bool> GetPost_GetId_EqualsSetValue()
        {
            //Given
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
