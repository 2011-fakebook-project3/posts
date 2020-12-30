using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FakebookPosts.DataModel;
using Fakebook.Posts.Domain;
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

            Domain.Post post = new Domain.Post
            {
                
            };

            bool result;


            //Act
            using (var context = new FakebookPostsContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new PostsRepository(context);
               
            }


        }
    }
}
