using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Posts.IntegrationTests.PostRepository.Test
{
    public class GetCommentTests
    {
        /// <summary>
        /// Tests the PostsRepository class' GetCommentAsync method. Ensures that a comment can be gotten from a specific ID.
        /// </summary>
        [Fact]
        public async Task DeleteCommentAsync_ValidId_Removes()
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
                CreatedAt = new DateTime(2021, 2, 1)
            };

            DataAccess.Models.Comment insertedComment = new()
            {
                Id = 2,
                PostId = 3,
                UserEmail = "person@domain.net",
                Content = "New Content",
                CreatedAt = new DateTime(2021, 2, 2)
            };

            using FakebookPostsContext context = new(options);
            context.Database.EnsureCreated();
            context.Posts.Add(insertedPost);
            context.Comments.Add(insertedComment);
            context.SaveChanges();

            PostsRepository repo = new(context);

            //Act
            var retrievedComment = await repo.GetCommentAsync(2);

            // Assert
            Assert.Equal(insertedComment.Id, retrievedComment.Id);
            Assert.Equal(insertedComment.PostId, retrievedComment.PostId);
            Assert.Equal(insertedComment.UserEmail, retrievedComment.UserEmail);
            Assert.Equal(insertedComment.Content, retrievedComment.Content);
            Assert.Equal(insertedComment.CreatedAt, retrievedComment.CreatedAt);
        }
    }
}
