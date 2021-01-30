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
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Posts.UnitTests.Controllers
{
    public class PostControllerTest : IClassFixture<WebApplicationFactory<Startup>> {

        private readonly WebApplicationFactory<Startup> _factory;

        public PostControllerTest(WebApplicationFactory<Startup> factory) {
            _factory = factory;
        }

        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that a proper Post object results in Status201Created.
        /// </summary>
        [Fact]
        public async Task PostAsync_ValidPost_Creates() {

            // Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var mockFollowRepo = new Mock<IFollowsRepository>();
            var mockBlobService = new Mock<IBlobService>();
            var comments = new List<Comment>();
            var date = DateTime.Now;
            var post = new Post("test.user@email.com", "test content")
            {
                Id = 1,
                Comments = comments,
                Picture = "picture",
                CreatedAt = date
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
                .ReturnsAsync(post);

            var client = _factory.WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(services => {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddScoped(sp => mockFollowRepo.Object);
                    services.AddTransient(sp => mockBlobService.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");

            var stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/posts", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that an improper Post object results in Status400BadRequest with an error message in the body.
        /// </summary>
        [Fact]
        public async Task PostAsync_InvalidPost_BadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var mockFollowRepo = new Mock<IFollowsRepository>();
            var mockBlobService = new Mock<IBlobService>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Throws(new DbUpdateException());

            var comments = new List<Comment>();
            var date = DateTime.Now;
            var post = new Post("test.user@email.com", "test content") {
                Id = 1,
                Comments = comments,
                Picture = "picture",
                CreatedAt = date
            };

            var client = _factory.WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(services => {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddScoped(sp => mockFollowRepo.Object);
                    services.AddTransient(sp => mockBlobService.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");

            var stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/posts", stringContent);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that a proper Comment object results in Status201Created.
        /// </summary>
        [Fact]
        public async Task PostAsync_ValidComment_Creates()
        {
            // Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var date = DateTime.Now;
            var post = new Post("test.user@email.com", "test content") {
                Id = 1,
                Picture = "picture",
                CreatedAt = date
            };
            var comment = new Comment("test.user@email.com", "comment content")
            {
                Id = 1,
                Post = post,
                Content = "picture",
                CreatedAt = date,
            };

            mockRepo.Setup(r => r.AddCommentAsync(It.IsAny<Comment>()))
                .Returns(ValueTask.FromResult(comment));

            var client = _factory.WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(services => {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");

            var stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(comment), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("api/comments", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

        }
        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that an improper Post object results in Status400BadRequest with an error message in the body.
        /// </summary>
        [Fact]
        public async Task PostAsyncComment_InvalidPost_BadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var date = DateTime.Now;
            var post = new Post("test.user@email.com", "test content") {
                Id = 1,
                Picture = "picture",
                CreatedAt = date
            };
            var comment = new Comment("test.user@email.com", "comment content") {
                Id = 1,
                Post = post,
                Content = "picture",
                CreatedAt = date,
            };

            mockRepo.Setup(r => r.AddCommentAsync(It.IsAny<Comment>()))
                .Throws(new DbUpdateException());
            var client = _factory.WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(services => {
                    services.AddScoped(sp => mockRepo.Object);
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Test");

            var stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(comment), Encoding.UTF8, "application/json");
            
            // Act
            var response = await client.PostAsync("api/comments", stringContent);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}