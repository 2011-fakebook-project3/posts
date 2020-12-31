using Fakebook.Posts.Domain;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Posts.UnitTests.Controllers
{
    public class PostControllerTest
    {
        readonly PostsController postController = new PostsController();

        [Fact]
        public async Task PostController_PostAsync()
        {
            //Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var postList = new List<Post>();
            var comment = new List<Comment>();
            var date = DateTime.Now;
            var post = new Post()
            {
                Id = 1,
                UserId = 1,
                Comments = comment,
                Content = "Goodman",
                Picture = "picture",
                CreatedAt = date
            };
            postList.Add(post);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Returns(ValueTask.FromResult(new Post()));

            //Act
            var actionResult = await postController.PostAsync(post);


            //Assert
            var result = Assert.IsAssignableFrom<CreatedAtActionResult>(actionResult);
            Assert.Equal(1, post.Id);
            Assert.Equal(1, post.UserId);
            Assert.Equal(comment, post.Comments);
            Assert.Equal("Goodman", post.Content);
            Assert.Equal("picture", post.Picture);
            Assert.Equal(date, post.CreatedAt);
            Assert.Equal(result.StatusCode, StatusCodes.Status201Created);
        }
       
    }
}
