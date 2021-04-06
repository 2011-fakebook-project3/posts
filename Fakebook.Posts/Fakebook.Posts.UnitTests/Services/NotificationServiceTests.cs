﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Posts.RestApi.DTOs;
using Fakebook.Posts.RestApi.Services;
using System.Net.Http;
using System.Net;
using Xunit;
using Moq;
using Moq.Protected;
using System.Threading;

namespace Fakebook.Posts.UnitTests.Services
{
    public class NotificationServiceTests
    {
        [Fact]
        public async Task NotificationService_ValidPost_Pass()
        {
            // arrange
            string endpoint = "http://localhost:5000/api/notifications/comments";
            NotificationDTO notification = new NotificationDTO()
            {
                LoggedInUser = "loggedIn@email.com",
                TriggeredUser = "triggered@email.com",
                PostId = 1,
            };

            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!""}, { ""id"": 100, ""title"": ""Some title""}]"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);
            INotificationService notificationService = new NotificationService(httpClient);

            // act
            var newResponse = await notificationService.SendNotificationAsync(endpoint, notification);

            // assert
            Assert.Equal(response.StatusCode, newResponse.StatusCode);
            Assert.Equal(response.Content, newResponse.Content);
        }
    }
}