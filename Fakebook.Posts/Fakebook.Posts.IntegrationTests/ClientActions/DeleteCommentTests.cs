using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Fakebook.Posts.IntegrationTests.Controllers
{
    public class DeleteCommentTests : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public DeleteCommentTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Tests the PostsController class' DeleteAsync method. Ensures that a proper id results in status 204NoContent.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_ValidId_Deletes()
        {

            // Arrange
            Mock<IPostsRepository> mockRepo = new();

            mockRepo.Setup(r => r.DeleteCommentAsync(It.IsAny<int>()))
                .Returns(new ValueTask());

            var date = DateTime.Now;
            Post post = new("test.user@email.com", "test content")
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = date
            };
            Comment comment = new("test.user@email.com", "comment content")
            {
                Id = 1,
                Post = post,
                Content = "picture",
                CreatedAt = date,
            };
            post.Comments.Add(comment);

            HashSet<Post> posts = new() { post };

            mockRepo.Setup(r => r.GetEnumerator())
                .Returns(posts.GetEnumerator());

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");

            // Act
            var response = await client.DeleteAsync("api/comments/1");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        /// <summary>
        /// Tests the PostsController class' PutAsync method. Ensures that an improper Post object results in status 400BadRequest with an error message in the body.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_InvalidId_NotFound()
        {

            // Arrange
            Mock<IPostsRepository> mockRepo = new();

            mockRepo.Setup(r => r.DeleteCommentAsync(It.IsAny<int>()))
                .Throws(new ArgumentException());

            var date = DateTime.Now;
            Post post = new("test.user@email.com", "test content")
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = date
            };
            Comment comment = new("test.user@email.com", "comment content")
            {
                Id = 1,
                Post = post,
                Content = "picture",
                CreatedAt = date,
            };
            post.Comments.Add(comment);

            HashSet<Post> posts = new() { post };

            mockRepo.Setup(r => r.GetEnumerator())
                .Returns(posts.GetEnumerator());

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");

            // Act
            var response = await client.DeleteAsync("api/comments/1");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
