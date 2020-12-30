using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FakebookPosts.DataModel;
using Fakebook.Posts.Domain;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Fakebook.Posts.UnitTests.PostRepository_Test
{
    public class PostRepository_CreateTest
    {
        [Fact]
        public void CreatePost()
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
                UserId = 1
            };

            ValueTask<Domain.Models.Post> result;

            //Act
            using (var context = new FakebookPostsContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new PostsRepository(context);
                result = repo.AddAsync(post);
               
            }

            //Assert
            Assert.True(result.Result.Content == post.Content);
            Assert.True(result.Result.UserId == post.UserId);
            Assert.True(result.Result.CreatedAt == post.CreatedAt);

        }
    }
}
