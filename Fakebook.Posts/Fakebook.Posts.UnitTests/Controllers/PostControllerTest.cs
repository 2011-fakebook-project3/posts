using Fakebook.Posts.Domain;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace Fakebook.Posts.UnitTests.Controllers
{
    public class PostControllerTest
    {

        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that a proper Post object results in Status201Created.
        /// </summary>
        [Fact]
        public async Task PostAsync_ValidPost_Creates()
        {
            // Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var comment = new List<Comment>();
            var date = DateTime.Now;
            var post = new Post("test@email.com", "Goodman")
            {
                Id = 1,
                Comments = comment,
                Picture = "picture",
                CreatedAt = date
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Returns(ValueTask.FromResult(post));

            var controller = new PostsController(mockRepo.Object);

            // Act
            var actionResult = await controller.PostAsync(post);


            // Assert
            var result = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult);
            var model = Assert.IsAssignableFrom<Post>(result.Value);
            Assert.Equal(1, model.Id);
            Assert.Equal("test@email.com", model.UserEmail);
            Assert.Equal(comment, model.Comments);
            Assert.Equal("Goodman", model.Content);
            Assert.Equal("picture", model.Picture);
            Assert.Equal(date, model.CreatedAt);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }

        /// <summary>
        /// Tests the PostsController class' PostAsync method. Ensures that an improper Post object results in Status400BadRequest with an error message in the body.
        /// </summary>
        [Fact]
        public async Task PostAsync_InvalidPost_BadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var post = new Post("", "");

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Returns(ValueTask.FromResult(post));

            var controller = new PostsController(mockRepo.Object);

            // Act
            var actionResult = await controller.PostAsync(post);

            // Assert
            var result = Assert.IsAssignableFrom<BadRequestErrorMessageResult>(actionResult);
        }

    }
}