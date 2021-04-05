using Fakebook.Posts.RestApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService(IHttpClientWrapper client) => throw new NotImplementedException();

        public async Task<HttpResponseMessage> SendNotificationAsync(string endpoint, NotificationDTO notification) => throw new NotImplementedException();
    }
}