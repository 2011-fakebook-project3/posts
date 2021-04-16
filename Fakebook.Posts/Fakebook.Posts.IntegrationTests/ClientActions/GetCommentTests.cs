using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Text.Json;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi;
using Fakebook.Posts.RestApi.Controllers;
using Fakebook.Posts.RestApi.Dtos;
using Fakebook.Posts.IntegrationTests.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.IntegrationTests.ClientActions
{
    public class GetCommentTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public GetCommentTests(WebApplicationFactory<Startup> factory)
        {
        _factory = factory;
        }

        private HttpClient BuildTestAuthClient(Mock<IPostsRepository> mockRepo)
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

        public async Task GetCommentAsync_ValidRequest_SendComment()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            List<Comment> comments = new();
            var date1 = new DateTime(2021, 3, 3, 3, 3, 3);
            var date2 = new DateTime(2021, 4, 4, 4, 4, 4);

            Post post = new("test.user@email.com", "test content")
            {
                Id = 1,
                Comments = comments,
                Picture = "picture",
                CreatedAt = date1
            };
            Comment comment = new("testMan.user@email.com", "some comment content", 1)
            {
                Id = 2,
                CreatedAt = date2
            };

            //mockRepo.Setup(c => c.GetAsync(It.IsAny<int>)))
            //    .returnsAsync()
        }
    }
}
