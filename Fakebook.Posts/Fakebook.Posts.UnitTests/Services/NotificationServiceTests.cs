using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Posts.RestApi.DTOs;
using Fakebook.Posts.RestApi.Services;
using System.Net.Http;
using System.Net;
using Xunit;
using Moq;

namespace Fakebook.Posts.UnitTests.Services
{
    public class NotificationServiceTests
    {
        [Fact]
        public async Task NotificationService_ValidPost_Pass()
        {
            // arrange
            NotificationDTO notification = new NotificationDTO()
            {
                LoggedInUser = "loggedIn@email.com",
                TriggeredUser = "triggered@email.com",
                PostId = 1,
            };

            Mock<IHttpClientWrapper> httpClient = new();

            httpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            INotificationService notificationService = new NotificationService(httpClient.Object);

            // act
            var response = await notificationService.SendNotificationAsync("comments", notification);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}