using Fakebook.Posts.RestApi.DTOs;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Fakebook.Posts.RestApi.Services
{
    public class NotificationService : INotificationService
    {
        private HttpClient _client;

        public NotificationService(HttpClient client) => _client = client;

        public async Task<HttpResponseMessage> SendNotificationAsync(string endpoint, NotificationDTO notification)
        {
            string jsonContent = JsonSerializer.Serialize(notification);
            var response = await _client.PostAsync(endpoint, new StringContent(jsonContent));
            return response;
        }
    }
}