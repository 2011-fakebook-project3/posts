using Xunit;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.DataAccess.Repositories;

namespace Fakebook.Posts.UnitTests.PostRepository.Test
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

            var post = new Fakebook.Posts.Domain.Models.Post("person@domain.net", "Post Content");
            post.CreatedAt = DateTime.Now;
                
            

            Fakebook.Posts.Domain.Models.Post result;

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
    }
}
