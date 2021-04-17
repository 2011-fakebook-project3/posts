using System;
using System.Threading.Tasks;
using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.DataAccess.Mappers;
using System.Linq;


namespace Fakebook.Posts.IntegrationTests.Repositories
{
    public class AddComentAsync
    {

        /// <summary>
        /// Ensures that the comment belongs to the proper post.
        /// </summary>
        [Fact]
        public async Task AddComentAsync_CommentPostIdEqualsPostId_ReturnsTrue()
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
                CreatedAt = new DateTime(2021, 2, 14)
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


            var databaseComment = (await context.Comments.SingleAsync(i => i.Id == 1)).ToDomain(insertedPost.ToDomain());


            // Assert
            Assert.Equal(insertedPost.Id,databaseComment.PostId );
        }
    }
}
