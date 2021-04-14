using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Fakebook.Posts.IntegrationTests.PostRepository.Test
{
    public class NewsFeedAsync
    {
        [Fact]
        public async Task NewsFeedAsync_ValidPost_ReturnsPost()
        {
            //Arrange
            using SqliteConnection connection = new("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            DataAccess.Models.Post dataAccessPost1 = new()
            {
                Id = 3,
                UserEmail = "person@domain.net",
                Content = "post content",
                CreatedAt = DateTime.Now
            };

            DataAccess.Models.Post dataAccessPost2 = new()
            {
                Id = 4,
                UserEmail = "person2@domain.net",
                Content = "post content",
                CreatedAt = DateTime.Now
            };

            DataAccess.Models.Post dataAccessPost3 = new()
            {
                Id = 5,
                UserEmail = "person3@domain.net",
                Content = "post content",
                CreatedAt = DateTime.Now
            };

            //Act

            using (FakebookPostsContext context = new(options))
            {
                context.Database.EnsureCreated();
                PostsRepository repo = new(context);
                context.Posts.Add(dataAccessPost1);
                context.Posts.Add(dataAccessPost2);
                context.Posts.Add(dataAccessPost3);
                context.SaveChanges();
                List<string> useremails = new List<string> { "person@domain.net", "person2@domain.net" };
                List<Fakebook.Posts.Domain.Models.Post> newsfeedpost = new List<Fakebook.Posts.Domain.Models.Post>(await repo.NewsfeedAsync(useremails));
                var count = newsfeedpost.Count;
                //Assert
                Assert.Equal(2, count);
            }
        }
    }
}
