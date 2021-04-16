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
using Microsoft.EntityFrameworkCore;
using Fakebook.Posts.DataAccess.Repositories;

namespace Fakebook.Posts.IntegrationTests.ClientActions
{
    public class GetCommentTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly IPostsRepository _postRepository;
        public GetCommentTests(WebApplicationFactory<Startup> factory, IPostsRepository postRepository)
        {
            _factory = factory;
            _postRepository = postRepository;
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
        // for if/when a getComment is needed--- potentially to be sent to Notifications
    }
}
