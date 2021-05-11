using Fakebook.Posts.RestApi.DTOs;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Fakebook.Posts.RestApi.Services
{
    public class NotificationService : INotificationService
    {
        private HttpClient _client;
        private IConfiguration _configuration;

        public NotificationService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> SendNotificationAsync(string endpoint, NotificationDTO notification)
        {
            var notificationUrl = _configuration["NotificationApi:url"];
            var uri = new Uri($"{notificationUrl}/{endpoint}?loggedInUser={notification.LoggedInUser}&triggerUser={notification.TriggeredUser}&postId={notification.PostId}");

            var response = await _client.PostAsync(uri, null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ToString());
            }

            return response;
        }
    }
}