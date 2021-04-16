using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Constants;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi;
using Fakebook.Posts.RestApi.Dtos;
using Fakebook.Posts.RestApi.Services;
using Fakebook.Posts.IntegrationTests.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using Moq;
using Xunit;
using Fakebook.Posts.RestApi.Controllers;
using Microsoft.Extensions.Logging;





namespace Fakebook.Posts.IntegrationTests.Controllers
{
    public class PostControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public PostControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that a proper Post object results in Status201Created.
        /// </summary>
        [Fact]
        public async Task PostAsync_ValidPost_Creates()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
            List<Comment> comments = new();
            var date = new DateTime(2021, 4, 4);
            Post post = new("test.user@email.com", "test content")
            {
                Id = 1,
                Comments = comments,
                Picture = "picture",
                CreatedAt = date
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
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

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            NewPostDto newPost = new() { Content = "Valid Content" };

            StringContent stringContent = new(JsonSerializer.Serialize(newPost), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/posts", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }


        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that an improper Post object results in Status400BadRequest with an error message in the body.
        /// </summary>
        [Fact]
        public async Task PostAsync_InvalidPost_BadRequest()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
            List<Comment> comments = new();
            var date = new DateTime(2021, 4, 4);
            Post post = new("test.user@email.com", "test content")
            {
                Id = 1,
                Comments = comments,
                Picture = "picture",
                CreatedAt = date
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
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

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Initialize a string, and then use a forloop to add to the string until it is one longer than the allowed post size

            StringBuilder longPost = new StringBuilder();

            for (int i = 0; i < Constants.PostMaxLength + 1; i++)
            {
                longPost.Append("X");
            }

            //Create New Post DTO objects that will be sent to the API.
            NewPostDto invalidPostTooLong = new() { Content = longPost.ToString() };
            NewPostDto invalidPostNoContent = new() { Content = "" };
            NewCommentDto nullPost = new() { };

            //  Serializes request object into json format.
            StringContent invalidStringTooMuchContent = new(JsonSerializer.Serialize(invalidPostTooLong), Encoding.UTF8, "application/json");
            StringContent invalidstringNoContent = new(JsonSerializer.Serialize(invalidPostNoContent), Encoding.UTF8, "application/json");
            StringContent nullContent = new(JsonSerializer.Serialize(nullPost), Encoding.UTF8, "application/json");

            // Act
            var invalidresponseTooMuchContent = await client.PostAsync("api/posts", invalidStringTooMuchContent);
            var invalidResponseNoContent = await client.PostAsync("api/posts", invalidstringNoContent);
            var invalidResponseNullContent = await client.PostAsync("api/posts", nullContent);

            // Assert
            // Ensures that the valid request was created, and that posts that are too long or too short are rejected.
            Assert.Equal(HttpStatusCode.BadRequest, invalidresponseTooMuchContent.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, invalidResponseNoContent.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, invalidResponseNullContent.StatusCode);
        }

        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that a proper Comment object results in Status201Created.
        /// </summary>
        [Fact]
        public async Task PostAsync_ValidComment_Creates()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            var date = new DateTime(2021, 4, 4);
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

            mockRepo.Setup(r => r.AddCommentAsync(It.IsAny<Comment>()))
                .Returns(ValueTask.FromResult(comment));

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            NewCommentDto newComment = new() { Content = "Valid Content" };

            StringContent stringContent = new(JsonSerializer.Serialize(newComment), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/comments", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that an improper Post object results in Status400BadRequest with an error message in the body.
        /// </summary>
        [Fact]
        public async Task PostAsyncComment_InvalidPost_BadRequest()
        {
            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            var date = new DateTime(2021, 4, 4);
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

            mockRepo.Setup(r => r.AddCommentAsync(It.IsAny<Comment>()))
                .Throws(new DbUpdateException());
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            NewCommentDto newComment = new() { Content = "Valid Content" };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            StringContent stringContent = new(JsonSerializer.Serialize(newComment), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/comments", stringContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }



        [Fact]
        public async Task GetNewsFeedAsync_DtoIsNotNull_ReturnsOk()
        {

            // Arrange
            Mock<IPostsRepository> mockRepo = new();
            Mock<IFollowsRepository> mockFollowRepo = new();
            Mock<IBlobService> mockBlobService = new();
           
            // arrange

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


            NewsFeedDto newsFeedDto = new NewsFeedDto();
            newsFeedDto.Emails = new List<string> { "test@domain.com, test2@domain.com" };

            var json = JsonSerializer.Serialize(newsFeedDto);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");
            var response = await client.PostAsync(new Uri("api/posts/newsfeed", UriKind.Relative), httpContent);


            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);



        }
    }
}