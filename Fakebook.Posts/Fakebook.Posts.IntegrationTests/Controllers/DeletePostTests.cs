using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi;
using Fakebook.Posts.RestApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Fakebook.Posts.IntegrationTests.Controllers
{
    public class DeletePostTests : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly WebApplicationFactory<Startup> _factory;

        public DeletePostTests(WebApplicationFactory<Startup> factory)
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
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
            mockRepo.Setup(r => r.DeletePostAsync(It.IsAny<int>()))
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

            // Act
            var response = await client.DeleteAsync("api/posts/1");

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
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
            mockRepo.Setup(r => r.DeletePostAsync(It.IsAny<int>()))
                .Throws(new ArgumentException());

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

            // Act
            var response = await client.DeleteAsync("api/posts/1");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
