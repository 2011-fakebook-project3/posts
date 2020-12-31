using FakebookPosts.DataModel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Posts.UnitTests.Repository
{
    public partial class TestSetup
    {
        public SqliteConnection Database_init()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<FakebookPostsContext>().UseSqlite(connection).Options;
            var comment1 = new List<Comment>();
            var date = DateTime.Now;
            var post1 = new Post();

            Post[] posts =
            {
                new Post
                {
                    Id = 1,
                    UserId = 1,
                    Comments = comment1,
                    Content = "Goodman",
                    Picture = "picture",
                    CreatedAt = date
                },
                new Post
                {
                    Id = 2,
                    UserId = 2,
                    Comments = comment1,
                    Content = "Goodman",
                    Picture = "picture",
                    CreatedAt = date
                },
                new Post
                {
                    Id = 3,
                    UserId = 3,
                    Comments = comment1,
                    Content = "Goodman",
                    Picture = "picture",
                    CreatedAt = date
                },
                new Post
                {
                    Id = 4,
                    UserId = 4,
                    Comments = comment1,
                    Content = "Goodman",
                    Picture = "picture",
                    CreatedAt = date
                },
                new Post
                {
                    Id = 5,
                    UserId = 5,
                    Comments = comment1,
                    Content = "Goodman",
                    Picture = "picture",
                    CreatedAt = date
                },

            };

            Comment[] comments =
            {
                new Comment
                {
                    Id = 1,
                    PostId = 1,
                    UserId = 1,
                    Post = post1,
                    Content = "GoodMan",
                    CreatedAt = date
                },
                

            };

            


            var context = new FakebookPostsContext(options);
            context.Database.EnsureCreated();
            foreach (var post in posts)
            {
                context.Posts.Add(post);
            }
            foreach (var comment in comments)
            {
                context.Comments.Add(comment);
            }
            context.SaveChanges();

            return connection;
        }
    }
}

