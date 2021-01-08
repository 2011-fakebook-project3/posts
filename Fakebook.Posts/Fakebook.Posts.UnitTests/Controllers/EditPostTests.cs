using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace Fakebook.Posts.UnitTests.Controllers {
    public class EditPostTests {

        /// <summary>
        /// Tests the PostsController class' PutAsync method. Ensures that a proper Post object results in status 204NoContent.
        /// </summary>
        [Fact]
        public async Task PutAsync_ValidPost_Updates() {

            // Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var post = new Post("test@email.com", "message") {
                Id = 1,
                Comments = new HashSet<Comment>(),
                Picture = "picture",
                CreatedAt = DateTime.Now
            };

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Post>()))
                .Returns(new ValueTask());

            var controller = new PostsController(mockRepo.Object, new NullLogger<PostsController>());

            // Act
            var actionResult = await controller.PutAsync(1, post);

            // Assert
            var result = Assert.IsAssignableFrom<NoContentResult>(actionResult);
        }

        /// <summary>
        /// Tests the PostsController class' PutAsync method. Ensures that an improper Post object results in status 400BadRequest with an error message in the body.
        /// </summary>
        [Fact]
        public async Task PutAsync_InvalidPost_BadRequest() {

            // Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var post = new Post("a", "b");

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Post>()))
                .Throws(new DbUpdateException());

            var controller = new PostsController(mockRepo.Object, new NullLogger<PostsController>());

            // Act
            var actionResult = await controller.PutAsync(1, post);

            // Assert
            var result = Assert.IsAssignableFrom<BadRequestErrorMessageResult>(actionResult);
        }
    }
}
