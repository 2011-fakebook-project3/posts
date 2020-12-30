using Fakebook.Posts.Domain;
using FakebookPosts.DataModel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Post = Fakebook.Posts.Domain.Post;

namespace Fakebook.Posts.UnitTests.Repository_Test
{
    public partial class TestSetup
    {
        Post testPost;
        [Fact]
        public async Task AddPost_Database_TestAsync()
        {
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<FakebookPostsContext>().UseSqlite(connection).Options;
            var date = DateTime.Now;
            testPost = new Post()
            {
                Id = 1,
                UserId = 1,
                Picture = "picture",
                Content = "content",
                Comments = null,
                CreatedAt = date
            };


            using (var context = new FakebookPostsContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new PostsRepository(options);

                await repo.AddAsync(testPost);

            }

            using var context2 = new FakebookPostsContext(options);
            FakebookPosts.DataModel.Post postReal = context2.Posts
                .Single(l => l.Id == 1);

            Assert.Equal(testPost.Id, postReal.Id);
            Assert.Equal(testPost.UserId, postReal.UserId);
        }
    }
}
