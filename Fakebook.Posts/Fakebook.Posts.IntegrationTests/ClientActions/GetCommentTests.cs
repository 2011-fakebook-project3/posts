using Xunit;
using Moq;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.RestApi;
using Fakebook.Posts.IntegrationTests.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Headers;


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

        private HttpClient BuildTestAuthClient(Moq.IMock<IPostsRepository> mockRepo)
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
