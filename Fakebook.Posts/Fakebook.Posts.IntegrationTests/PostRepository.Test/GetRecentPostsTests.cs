﻿using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Fakebook.Posts.DataAccess.Repositories;
using Fakebook.Posts.DataAccess;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.IntegrationTests.PostRepository.Test
{
    public class GetRecentPostsTests
    {
        /// <summary>
        /// Tests GetRecentPostAsync postRepository method for getting a list of recent posts 
        /// in a span of minutes from the time of 'now'.
        /// Test does not add addedPost1 to the list because of wrong userEmail.
        /// Test is supposed to add addedPost2 and addedPost3 because they are posts by the User
        /// and the time is recent.
        /// Test does not add addedPost4 to the list because it is an old post.
        /// Asserts that the returned posts are addedPost2 and addedPost3.
        /// </summary>
        /// <returns>Task of GetRecentPostsAsync test.</returns>
        [Fact]
        public async Task GetRecentPostsAsync_ValidPosts_ReturnsPostsList()
        {
            // Arrange
            using SqliteConnection connection = new("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<FakebookPostsContext>()
                .UseSqlite(connection)
                .Options;

            DateTime now = new DateTime(2020, 2, 2, 2, 2, 2);
            DateTime date1 = now - TimeSpan.FromMinutes(3);
            DateTime date2 = now - TimeSpan.FromMinutes(2);
            DateTime date3 = now - TimeSpan.FromMinutes(5);
            DateTime date4 = now - TimeSpan.FromMinutes(40);

            DataAccess.Models.Post addedPost1 = new()
            {
                Id = 10,
                UserEmail = "testerGuy@yoohoo.net",
                Content = "some tasty content",
                CreatedAt = date1
            };

            DataAccess.Models.Post addedPost2 = new()
            {
                Id = 11,
                UserEmail = "testerWoman@yoohoo.net",
                Content = "this content is yummy",
                CreatedAt = date2
            };
            DataAccess.Models.Post addedPost3 = new()
            {
                Id = 15,
                UserEmail = "testerWoman@yoohoo.net",
                Content = "I am hungry for content",
                CreatedAt = date3
            };

            DataAccess.Models.Post addedPost4 = new()
            {
                Id = 13,
                UserEmail = "testerWoman@yoohoo.net",
                Content = "this is old but still tasty content",
                CreatedAt = date4
            };

            using FakebookPostsContext context = new(options);
            context.Database.EnsureCreated();
            context.Posts.Add(addedPost1);
            context.Posts.Add(addedPost2);
            context.Posts.Add(addedPost4);
            context.Posts.Add(addedPost3);
            context.SaveChanges();
            
            PostsRepository repo = new(context);
            string userEmail = "testerWoman@yoohoo.net";
            int recentPostsInMinutes = 10;

            // Act
            var actualList = await repo.GetRecentPostsAsync(userEmail, recentPostsInMinutes, now);

            // Assert
            Assert.Equal(addedPost2.Content, actualList[0].Content);
            Assert.Equal(addedPost3.Content, actualList[1].Content);
        }
    }
}
