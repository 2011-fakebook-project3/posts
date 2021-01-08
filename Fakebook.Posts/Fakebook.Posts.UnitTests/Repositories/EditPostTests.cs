using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Posts.UnitTests.Repositories
{
    public class EditPostTests
    {

        /// <summary>
        /// Tests the PostsRepository class' UpdateAsync method. Ensures that a proper Post object results in the database being modified.
        /// </summary>
        [Fact]
        public async Task UpdateAsync_ValidPost_UpdatesDb()
        {

            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;
            Domain.Models.Post post = new Domain.Models.Post("person@domain.net", "content")
            {
                Id = 1,
                Content = "New Content",
                CreatedAt = DateTime.Now
            };
            DataAccess.Models.Post _post = new DataAccess.Models.Post()
            {
                Id = 1,
                UserEmail = "person@domain.net",
                Content = "New Content",
                CreatedAt = DateTime.Now
            };
            //Act

            using var context = new FakebookPostsContext(options);

            context.Database.EnsureCreated();
            context.Posts.Add(_post);
            context.SaveChanges();
            post.Content = "updateContent";
            var repo = new PostsRepository(context);
            await repo.UpdateAsync(post);
            var result = await context.Posts.FirstAsync(p => p.UserEmail == "person@domain.net");


            Assert.True(result.Content == post.Content);
            Assert.True(result.UserEmail == post.UserEmail);
            Assert.True(result.CreatedAt == post.CreatedAt);

        }
    }
}
