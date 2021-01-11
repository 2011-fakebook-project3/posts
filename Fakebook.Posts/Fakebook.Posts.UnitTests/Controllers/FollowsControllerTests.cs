using Xunit;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Fakebook.Posts.RestApi.Controllers;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.UnitTests.Controllers
{
    public class FollowsControllerTests
    {
        [Fact]
        public async void PostAsync_NewEmail_NoContent()
        {
            //Given
            var myFollow = new Follow
            {
                FollowedEmail = It.IsAny<string>(),
                FollowerEmail = It.IsAny<string>()
            };
            
            var mockRepo = new Mock<IFollowsRepository>();
            var logger = new NullLogger<FollowsController>();
            mockRepo.Setup(r => r.AddFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(true);
            var controller = new FollowsController(mockRepo.Object, logger);
            //When
            var result = await controller.PostAsync(myFollow);
            //Then
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void PostAsync_OldEmail_BadRequest()
        {
            //Given
            var myFollow = new Follow
            {
                FollowedEmail = It.IsAny<string>(),
                FollowerEmail = It.IsAny<string>()
            };
            var mockRepo = new Mock<IFollowsRepository>();
            var logger = new NullLogger<FollowsController>();
            mockRepo.Setup(r => r.AddFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(false);
            var controller = new FollowsController(mockRepo.Object, logger);
            //When
            var result = await controller.PostAsync(myFollow);
            //Then
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void PutAsync_NewEmail_NoContent()
        {
            //Given
            var mockRepo = new Mock<IFollowsRepository>();
            var logger = new NullLogger<FollowsController>();
            mockRepo.Setup(r => r.AddFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(true);
            var controller = new FollowsController(mockRepo.Object, logger);
            //When
            var result = await controller.PutAsync(It.IsAny<string>());
            //Then
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void PutAsync_OldEmail_BadRequest()
        {
            //Given
            var mockRepo = new Mock<IFollowsRepository>();
            var logger = new NullLogger<FollowsController>();
            mockRepo.Setup(r => r.AddFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(false);
            var controller = new FollowsController(mockRepo.Object, logger);
            //When
            var result = await controller.PutAsync(It.IsAny<string>());
            //Then
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void DeleteAsync_OldEmail_NoContent()
        {
            //Given
            var mockRepo = new Mock<IFollowsRepository>();
            var logger = new NullLogger<FollowsController>();
            mockRepo.Setup(r => r.RemoveFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(true);
            var controller = new FollowsController(mockRepo.Object, logger);
            //When
            var result = await controller.DeleteAsync(It.IsAny<string>());
            //Then
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteAsync_NewEmail_BadRequest()
        {
            //Given
            var mockRepo = new Mock<IFollowsRepository>();
            var logger = new NullLogger<FollowsController>();
            mockRepo.Setup(r => r.RemoveFollowAsync(It.IsAny<Follow>()))
                    .ReturnsAsync(false);
            var controller = new FollowsController(mockRepo.Object, logger);
            //When
            var result = await controller.DeleteAsync(It.IsAny<string>());
            //Then
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }   
}