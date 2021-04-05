using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Services
{
    public interface IHttpClientWrapper : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}