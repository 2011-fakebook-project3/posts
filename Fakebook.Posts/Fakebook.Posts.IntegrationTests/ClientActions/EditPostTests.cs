using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi;
using Fakebook.Posts.RestApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Fakebook.Posts.IntegrationTests.Controllers
{
    public class EditPostTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public EditPostTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Tests the PostsController class' PutAsync method. Ensures that a proper Post object results in status 204NoContent.
        /// </summary>
        [Fact]
        public async Task PutAsync_ValidPost_Updates()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Post>()))
                .Returns(new ValueTask());

            HashSet<Post> posts = new()
            {
                new("test.user@email.com", "test content") { Id = 1 }
            };

            mockRepo.Setup(r => r.GetEnumerator())
                .Returns(posts.GetEnumerator());

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddScoped(sp => mockFollowRepo.Object);
                    services.AddTransient(sp => mockBlobService.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");

            Post post = new("test@email.com", "message")
            {
                Id = 1,
                Comments = new HashSet<Comment>(),
                Picture = "picture",
                CreatedAt = new DateTime(2021, 4, 4)
            };

            StringContent stringContent = new(System.Text.Json.JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("api/posts/1", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        /// <summary>
        /// Tests the PostsController class' PutAsync method. Ensures that an improper Post object results in status 400BadRequest with an error message in the body.
        /// </summary>
        [Fact]
        public async Task PutAsync_InvalidPost_BadRequest()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Post>()))
                .Throws(new DbUpdateException());

            HashSet<Post> posts = new()
            {
                new("test.user@email.com", "test content") { Id = 1 }
            };

            mockRepo.Setup(r => r.GetEnumerator())
                .Returns(posts.GetEnumerator());

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddScoped(sp => mockFollowRepo.Object);
                    services.AddTransient(sp => mockBlobService.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");

            Post post = new("test@email.com", "message")
            {
                Id = 1,
                Comments = new HashSet<Comment>(),
                Picture = "picture",
                CreatedAt = new DateTime(2021, 4, 4)
            };

            StringContent stringContent = new(System.Text.Json.JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync("api/posts/1", stringContent);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}