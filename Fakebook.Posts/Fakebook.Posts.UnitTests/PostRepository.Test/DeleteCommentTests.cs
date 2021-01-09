using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Posts.UnitTests.Repositories {
    public class DeleteCommentTests {

        /// <summary>
        /// Tests the PostsRepository class' UpdateAsync method. Ensures that a proper Post object results in the database being modified.
        /// </summary>
        [Fact]
        public async Task DeleteCommentAsync_ValidId_Removes() {

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

            DataAccess.Models.Comment insertedComment = new DataAccess.Models.Comment() {
                Id = 1,
                PostId = 1,
                UserEmail = "person@domain.net",
                Content = "New Content",
                CreatedAt = DateTime.Now
            };

            using var context = new FakebookPostsContext(options);
            context.Database.EnsureCreated();
            context.Posts.Add(insertedPost);
            context.Comments.Add(insertedComment);
            context.SaveChanges();

            var repo = new PostsRepository(context);

            //Act
            await repo.DeleteCommentAsync(1);
            context.SaveChanges();

            // Assert
            Assert.DoesNotContain(insertedComment, context.Comments);
        }
    }
}