using System;
using System.Threading.Tasks;
using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Fakebook.Posts.Domain.Models;


namespace Fakebook.Posts.IntegrationTests.Repositories
{
    public class AddComentAsync
    {

        /// <summary>
        /// Tests the PostsRepository class' UpdateAsync method. Ensures that a proper Post object results in the database being modified.
        /// </summary>
        [Fact]
        public async Task AddComentAsync_ValidId_AddComment()
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


            using FakebookPostsContext context = new(options);
            context.Database.EnsureCreated();
            context.Posts.Add(insertedPost);
            context.SaveChanges();

            PostsRepository repo = new(context);

            //Act

            Comment comment = new("person@domain.net", "New Content", insertedPost.Id);
            await repo.AddCommentAsync(comment);
            context.SaveChanges();

            // Assert
            Assert.True(context.Comments.SingleAsync() != null );
        }
    }
}
