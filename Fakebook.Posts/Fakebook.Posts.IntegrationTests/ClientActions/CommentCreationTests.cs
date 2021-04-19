using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Text.Json;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi;
using Fakebook.Posts.RestApi.Dtos;
using Fakebook.Posts.IntegrationTests.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Fakebook.Posts.Domain.Constants;
using Fakebook.Posts.RestApi.Services;
using Fakebook.Posts.RestApi.DTOs;

namespace Fakebook.Posts.IntegrationTests.ClientActions
{
    public class CommentCreationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public CommentCreationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
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
        
        /// <summary>
        /// Comment controller test checking that a valid comment can be successfully added to a post.
        /// </summary>
        [Fact (Skip = "Having trouble mocking NotificationService")]
        public async Task AddCommentAsync_ValidComment_Creates()
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
            HttpResponseMessage message = new();

            mockRepo.Setup(c => c.AddCommentAsync(It.IsAny<Comment>()))
                .ReturnsAsync(comment);
            var client = BuildTestAuthClient(mockRepo);

            

            NewCommentDto newComment = new()
            {
                PostId = post.Id,
                Content = comment.Content
            };
            StringContent stringContent = new(JsonSerializer.Serialize(newComment), Encoding.UTF8, "application/json");
            // Act
            var response = await client.PostAsync(new Uri("api/comments", UriKind.Relative), stringContent);
            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        /// <summary>
        /// Comment Controller tests checking comments that are null, empty, and too long
        /// </summary>
        [Fact]
        public async Task AddCommentAsync_InvalidComment_BadRequest()
        {
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
            Comment comment = new("testMan.user@email.com", "some comment content", post.Id)
            {
                Id = 2,
                CreatedAt = date2
            };

            mockRepo.Setup(c => c.AddCommentAsync(It.IsAny<Comment>()))
                .ReturnsAsync(comment);
            var client = BuildTestAuthClient(mockRepo);

            // build content to be 1 character longer than acceptable comment. 
            StringBuilder longComment = new StringBuilder();
            for (int i = 0; i < Constants.CommentMaxLength + 1; i++)
            {
                longComment.Append('A');
            }

            // Create Comment DTO objects to be sent to the API.
            NewCommentDto invalidCommentTooLong = new() { Content = longComment.ToString() };
            NewCommentDto invalidCommentNoContent = new() { Content = "" };
            NewCommentDto invalidCommentNull = new() { };

            // Serialize request object into Json format.
            StringContent invalidStringTooMuchContent = new(JsonSerializer.Serialize(invalidCommentTooLong), Encoding.UTF8, "application/json");
            StringContent invalidstringNoContent = new(JsonSerializer.Serialize(invalidCommentNoContent), Encoding.UTF8, "application/json");
            StringContent nullContent = new(JsonSerializer.Serialize(invalidCommentNull), Encoding.UTF8, "application/json");

            // Act
            var invalidresponseTooMuchContent = await client.PostAsync(new Uri("api/comments", UriKind.Relative), invalidStringTooMuchContent);
            var invalidResponseNoContent = await client.PostAsync(new Uri("api/comments", UriKind.Relative), invalidstringNoContent);
            var invalidResponseNullContent = await client.PostAsync(new Uri("api/comments", UriKind.Relative), nullContent);

            // Assert
            // Ensures that Comments that are Null, Empty, or too long fail
            Assert.Equal(HttpStatusCode.BadRequest, invalidresponseTooMuchContent.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, invalidResponseNoContent.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, invalidResponseNullContent.StatusCode);
        }
    }
}
