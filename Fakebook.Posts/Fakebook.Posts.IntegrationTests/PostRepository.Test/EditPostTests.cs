using System;
using System.Threading.Tasks;
using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Fakebook.Posts.IntegrationTests.Repositories
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
            using SqliteConnection connection = new("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            DataAccess.Models.Post insertedPost = new()
            {
                Id = 3,
                UserEmail = "person@domain.net",
                Content = "New Content",
                CreatedAt = DateTime.Now
            };

            string updatedContent = "updated content";

            Domain.Models.Post updatedPost = new("person@domain.net", updatedContent)
            {
                Id = 3
            };

            using FakebookPostsContext context = new(options);
            context.Database.EnsureCreated();
            context.Posts.Add(insertedPost);
            context.SaveChanges();

            PostsRepository repo = new(context);

            //Act
            await repo.UpdateAsync(updatedPost);

            // Assert
            var result = await context.Posts.FirstAsync(p => p.UserEmail == "person@domain.net");

            Assert.Equal(updatedContent, result.Content);
        }
    }
}
