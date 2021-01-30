using System;
using System.Threading.Tasks;
using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Fakebook.Posts.UnitTests.Repositories
{
    public class DeletePostTests
    {

        /// <summary>
        /// Tests the PostsRepository class' UpdateAsync method. Ensures that a proper Post object results in the database being modified.
        /// </summary>
        [Fact]
        public async Task DeletePostAsync_ValidId_Removes()
        {

            // Arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            DataAccess.Models.Post insertedPost = new DataAccess.Models.Post()
            {
                Id = 3,
                UserEmail = "person@domain.net",
                Content = "New Content",
                CreatedAt = DateTime.Now
            };

            using var context = new FakebookPostsContext(options);
            context.Database.EnsureCreated();
            context.Posts.Add(insertedPost);
            context.SaveChanges();

            var repo = new PostsRepository(context);

            //Act
            await repo.DeletePostAsync(3);
            context.SaveChanges();

            // Assert
            Assert.DoesNotContain(insertedPost, context.Posts);
        }
    }
}