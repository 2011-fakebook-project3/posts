using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi;
using Fakebook.Posts.RestApi.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Fakebook.Posts.IntegrationTests.Controllers
{
    public class FollowsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public FollowsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private HttpClient BuildTestAuthClient(Mock<IFollowsRepository> mockRepo)
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(sp => mockRepo.Object);

                    services
                    .AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
            return client;
        }

        [Fact]
        public async void PostAsync_NewEmail_NoContent()
        {
            //Given
            Follow myFollow = new()
            {
                FollowedEmail = It.IsAny<string>(),
                FollowerEmail = It.IsAny<string>(),
            };
            StringContent stringContent = new(
                JsonSerializer.Serialize(myFollow),
                Encoding.UTF8, "application/json");

            Mock<IFollowsRepository> mockRepo = new();
            NullLogger<FollowsController> logger = new();
            mockRepo.Setup(r => r.AddFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(true);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(sp => mockRepo.Object);

                    services
                    .AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            //When
            var response = await client.PostAsync("api/follows", stringContent);

            //Then
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void PostAsync_OldEmail_BadRequest()
        {
            //Given
            Follow myFollow = new()
            {
                FollowedEmail = It.IsAny<string>(),
                FollowerEmail = It.IsAny<string>()
            };
            StringContent stringContent = new(
                JsonSerializer.Serialize(myFollow),
                Encoding.UTF8, "application/json");

            Mock<IFollowsRepository> mockRepo = new();
            NullLogger<FollowsController> logger = new();
            mockRepo.Setup(r => r.AddFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(false);

            var client = BuildTestAuthClient(mockRepo);
            //When
            var response = await client.PostAsync("api/follows", stringContent);

            //Then
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void PutAsync_NewEmail_NoContent()
        {
            //Given
            Mock<IFollowsRepository> mockRepo = new();
            NullLogger<FollowsController> logger = new();
            mockRepo.Setup(r => r.AddFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(true);
            var client = BuildTestAuthClient(mockRepo);

            //When
            var response = await client.PutAsync("api/follows/name@domain.net", new StringContent(""));

            //Then
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void PutAsync_OldEmail_BadRequest()
        {
            //Given
            Mock<IFollowsRepository> mockRepo = new();
            NullLogger<FollowsController> logger = new();
            mockRepo.Setup(r => r.AddFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(false);
            var client = BuildTestAuthClient(mockRepo);

            //When
            var response = await client.PutAsync("api/follows/name@domain.net", new StringContent(""));

            //Then
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void DeleteAsync_OldEmail_NoContent()
        {
            //Given
            Mock<IFollowsRepository> mockRepo = new();
            NullLogger<FollowsController> logger = new();
            mockRepo.Setup(r => r.RemoveFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(true);
            var client = BuildTestAuthClient(mockRepo);

            //When
            var response = await client.DeleteAsync("api/follows/name@domain.net");

            //Then
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void DeleteAsync_NewEmail_BadRequest()
        {
            //Given
            Mock<IFollowsRepository> mockRepo = new();
            NullLogger<FollowsController> logger = new();
            mockRepo.Setup(r => r.RemoveFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(false);
            var client = BuildTestAuthClient(mockRepo);

            //When
            var response = await client.PutAsync("api/follows/name@domain.net", new StringContent(""));

            //Then
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
