using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.IntegrationTests.Services;
using Fakebook.Posts.RestApi;
using Fakebook.Posts.RestApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Posts.IntegrationTests.ClientActions
{
    public class GetPostTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public GetPostTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Tests the PostsController class' GetAsync method. Ensures that a proper post id results in status 200 Ok.
        /// </summary>
        [Fact]
        public async Task GetAsync_ValidId_ReturnsPost()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
            Post post = new Post("test.user@email.com", "test content") { Id = 1 };
            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(post);

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
            var response = await client.GetAsync(new Uri("api/posts/1", UriKind.Relative));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Tests the PostsController class' GetAsync method. Ensures that an invalid id results in status 404 NotFound.
        /// </summary>
        [Fact]
        public async Task GetAsync_InvalidId_ReturnsNotFound()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Post);

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
            var response = await client.GetAsync(new Uri("api/posts/1", UriKind.Relative));

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
