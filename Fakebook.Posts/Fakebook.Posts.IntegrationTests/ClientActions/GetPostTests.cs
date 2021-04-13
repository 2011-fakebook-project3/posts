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
        /// Tests the PostsController class' PutAsync method. Ensures that a proper Post object results in status 204NoContent.
        /// </summary>
        [Fact]
        public async Task GetAsync_ValidId_ReturnsPost()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>()))
                .Returns(ValueTask.FromResult(new Post("test.user@email.com", "test content") { Id = 1 }));

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
            var response = await client.GetAsync("api/posts/1");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
